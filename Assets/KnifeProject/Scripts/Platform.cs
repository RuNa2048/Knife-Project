using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private Transform _checkpointPos;

	[Header("Treatment Checkpoints")]
	[SerializeField] private Checkpoint _checkpoint;

	public Transform CheckpointPosition{ get { return _checkpointPos; } private set { } }

	private FlipperKnife _player;

	private string _playerTag;

	private void Start()
	{
		_playerTag = ConstantsGameTags.PlayerTextTag;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_player)
		{
			_player = other.GetComponent<FlipperKnife>();
		}

		if (other.tag == _playerTag)
		{
			bool isSavePosition = _checkpoint.SavingPosition(this);

			if (!isSavePosition)
			{
				_player.Destruction();
			}
		}
	}
}
