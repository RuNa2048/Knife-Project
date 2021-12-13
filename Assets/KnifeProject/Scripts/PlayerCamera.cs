using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[Header("Refernces")]
	[SerializeField] private FlipperKnife _player;
	[SerializeField] private Checkpoint _checkpoint;

	[Header("Distance To Points")]
	[SerializeField] private Vector3 _distanceToPlayer;
	[SerializeField] private Vector3 _distanceToCenterScreen;

	[Header("Moving Settings")]
	[SerializeField] private float _speed = 10f;
	[SerializeField] private float movingThreshold = 0.5f;

	private bool _isMoving = false;

	private void Start()
	{
		_player.OnStikingKnife += MoveToNextCheckpoint;
	}

	private void MoveToNextCheckpoint()
	{
		if (_isMoving)
		{
			return;
		}

		Vector3 position = _checkpoint.LastCheckpointPos + _distanceToPlayer;
		position.y = transform.position.y;

		StartCoroutine(Moving(position));
	}

	private IEnumerator Moving(Vector3 pos)
	{
		_isMoving = true;

		while (Vector3.Distance(transform.position, pos) > movingThreshold)
		{
			transform.position = Vector3.Slerp(transform.position, pos, _speed * Time.deltaTime);

			yield return null;
		}

		_isMoving = false;
	}

	//private void LateUpdate()
	//{
	//	Vector3 position = _player.transform.position + _distanceToPlayer;

	//	transform.position = Vector3.Slerp(transform.position, position, _speed * Time.deltaTime);

	//	Vector3 rotOffset = _player.transform.position + _distanceToCenterScreen;

	//	transform.LookAt(rotOffset);
	//}
}
