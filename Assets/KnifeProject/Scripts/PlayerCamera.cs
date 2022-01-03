using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[Header("Refernces")]
	[SerializeField] private FlipperKnife _knife;

	[Header("Moving Settings")]
	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _movingThreshold = 0.5f;
	[SerializeField] private float _distanceToMoveForward = 12f;

	[Header("Distance To Points")]
	[SerializeField] private Vector3 _distanceToPlayer;
	[SerializeField] private Vector3 _distanceToCenterScreen;

	private bool _isMoving = false;

	private void Start()
	{
		_knife.OnStikingKnife += MoveToKnife;
		_distanceToPlayer = _knife.transform.position - transform.position;
	}

	private void MoveToKnife()
	{
		if (_isMoving)
		{
			return;
		}

		Vector3 position = _knife.transform.position - _distanceToPlayer;

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

		Vector3 newPos = transform.position;
		newPos.z += _distanceToMoveForward;

		StartCoroutine(Moving(newPos));
	}

	public void ReturnBackToLastPosition()
	{
		StopAllCoroutines();

		_isMoving = false;

		MoveToKnife();
	}

	//private void LateUpdate()
	//{
	//	Vector3 position = _player.transform.position + _distanceToPlayer;

	//	transform.position = Vector3.Slerp(transform.position, position, _speed * Time.deltaTime);

	//	Vector3 rotOffset = _player.transform.position + _distanceToCenterScreen;

	//	transform.LookAt(rotOffset);
	//}
}
