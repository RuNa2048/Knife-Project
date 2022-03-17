using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
	[Header("Refernces")]
	[SerializeField] private Knife _knife;
	[SerializeField] private PlatformKeeper _platformKeeper;

	[Header("Moving Settings")]
	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _movingThreshold = 0.5f;
	[SerializeField] private float _eyeDistance = 12f;
	[SerializeField] private float _distanceToMoveForKnifeExit = 12f;

	[Header("Distance To Points")]
	[SerializeField] private Vector3 _distanceToPlayer;

	private Camera _camera;
	private Vector3 _preparedXYPosition;
	
	private void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	public void Initialize(Knife knife)
	{
		_knife = knife;
		
		_knife.OnStandedToPlatform += MoveToKnife;
		_knife.OnDestructed += MoveToKnife;
		_knife.OnJumped += NotCompletelyFollow;
		
		_preparedXYPosition = _knife.transform.position + _distanceToPlayer;

		transform.position = _preparedXYPosition;
	}

	private void NotCompletelyFollow()
	{
		StartCoroutine(CheckKnifeForExitingCamera());
	}

	private IEnumerator CheckKnifeForExitingCamera()
	{
		while (true)
		{
			Vector3 viewportPoint = _camera.WorldToViewportPoint(_knife.transform.position);

			if (viewportPoint.x > 1f)
			{
				Vector3 positionBeforeMoving = CalculatePositionForMoving(
					_platformKeeper.LastSaveCheckpointPos.z, 
					_distanceToMoveForKnifeExit);

				StartCoroutine(Moving(positionBeforeMoving));

				yield break;
			}

			yield return null;
		}
	}
	
	public void MoveForwardToEyeDistance()
	{
		StopAllCoroutines();

		Vector3 positionBeforeMoving = CalculatePositionForMoving(
			_platformKeeper.LastSaveCheckpointPos.z, 
			_eyeDistance);

		StartCoroutine(Moving(positionBeforeMoving));
	}

	public void MoveToKnife()
	{
		StopAllCoroutines();

		Vector3 position = _preparedXYPosition;
		position.z = _knife.transform.position.z + _distanceToPlayer.z;

		StartCoroutine(Moving(position));
	}

	private IEnumerator Moving(Vector3 pos)
	{
		while (Vector3.Distance(transform.position, pos) > _movingThreshold)
		{
			transform.position = Vector3.Slerp(transform.position, pos, _speed * Time.deltaTime);

			yield return null;
		}
	}
	
	private Vector3 CalculatePositionForMoving(float startPosition, float movingDistance)
	{
		var newPosition = _preparedXYPosition;
		newPosition.z = _knife.transform.position.z + _distanceToPlayer.z;
		newPosition.z += movingDistance;

		return newPosition;
	}
}
