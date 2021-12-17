using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform: MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private Transform _checkpointPos;

	[Header("Treatment Checkpoints")]
	[SerializeField] private Checkpoint _checkpoint;

	private string _playerTag;

	private void Start()
	{
		_playerTag = ConstantsGameTags.PlayerTextTag;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == _playerTag)
		{
			_checkpoint.SavingPosition(_checkpointPos);
		}
	}
}
