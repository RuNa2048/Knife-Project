using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
	[Header("Refernces")]
	[SerializeField] private FlipperKnife _knife;
	[SerializeField] private PlatformKeeper _platformKeeper;

	[Header("Moving Settings")]
	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _movingThreshold = 0.5f;
	[SerializeField] private float _distanceToMoveForNextPlatforms = 12f;
	[SerializeField] private float _distanceToMoveForKnifeExit = 12f;

	[Header("Distance To Points")]
	[SerializeField] private Vector3 _distanceToPlayer;
	[SerializeField] private Vector3 _distanceToCenterScreen;

	private Camera _camera;
	private Vector3 _preparedXYPosition;

	private bool _isMoving = false;

	private void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	private void Start()
	{
		_knife.OnStikingKnife += MoveToKnife;
		_knife.OnJumpingKnife += RunCameraCheck;

		_distanceToPlayer = _knife.transform.position - transform.position;
		_preparedXYPosition = _knife.transform.position - _distanceToPlayer;
	}

	private void RunCameraCheck()
	{
		StartCoroutine(CheckKnifeForExitingCamera());
	}

	private IEnumerator CheckKnifeForExitingCamera()
	{
		Vector3 point;

		while (_knife.Flying)
		{
			point = _camera.WorldToViewportPoint(_knife.transform.position);

			if (point.x > 1f)
			{
				Vector3 newPos = _preparedXYPosition;
				newPos.z = _platformKeeper.LastSaveCheckpointPos.z - _distanceToPlayer.z;
				newPos.z += _distanceToMoveForKnifeExit;

				StartCoroutine(Moving(newPos));

				yield break;
			}

			yield return null;
		}
	}


	private void FollowToKnife()
	{
		Vector3 point = _camera.WorldToViewportPoint(_knife.transform.position);
		Vector3 position = transform.position;

		position.z = _knife.transform.position.z - _distanceToPlayer.z;

		if (point.x > 1f)
		{
			transform.position = Vector3.Slerp(transform.position, position, _speed * Time.deltaTime);
		}
	}

	private void MoveToKnife()
	{
		if (_isMoving)
		{
			return;
		}

		StopAllCoroutines();

		Vector3 position = _preparedXYPosition;
		position.z = _knife.LastPositionOnPlatform.z - _distanceToPlayer.z;

		StartCoroutine(Moving(position));
	}

	private IEnumerator Moving(Vector3 pos)
	{
		_isMoving = true;

		while (Vector3.Distance(transform.position, pos) > _movingThreshold)
		{
			transform.position = Vector3.Slerp(transform.position, pos, _speed * Time.deltaTime);

			yield return null;
		}

		_isMoving = false;
	}

	public void MoveForwardToDistance()
	{
		StopAllCoroutines();

		Vector3 newPos = _preparedXYPosition;
		newPos.z = _platformKeeper.LastSaveCheckpointPos.z - _distanceToPlayer.z;
		newPos.z += _distanceToMoveForNextPlatforms;

		StartCoroutine(Moving(newPos));
	}

	public void ReturnBackToLastPosition()
	{
		StopAllCoroutines();

		_isMoving = false;

		MoveToKnife();
	}
}
