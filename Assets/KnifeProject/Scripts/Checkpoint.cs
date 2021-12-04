using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	[Header("Player Detector Settings")]
	[SerializeField] private string _playerTag = "Player";

	[Header("Treatment Checkpoints")]
	[SerializeField] private CheckpointsPull _pull;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == _playerTag)
		{
			_pull.SavingPosition(transform.position);
		}
	}
}
