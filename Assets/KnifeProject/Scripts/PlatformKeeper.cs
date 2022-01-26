using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeeper : MonoBehaviour
{
	[Header("Refrences To Platforms")]
	[SerializeField] private List<Platform> _platforms;

	[Header("Refrences")]
	[SerializeField] private FlipperKnife _knife;
	[SerializeField] private WindowBehaviour _windowBehaviour;

	[SerializeField] private bool _testedMode;

	private Transform _lastSaveCheckpoint;
	public Vector3 LastSaveCheckpointPos => _lastSaveCheckpoint.position;

	private Platform _newPlatform;

	private int _idLastPlatform = -1;

	private void Start()
	{
		int platformID = 0;

		foreach (var platform in _platforms)
		{
			platform.Init(this, platformID);

			platformID++;
		}
	}   

	public void AllowToStand(Platform platform)
	{
		_newPlatform = platform;

		if (_idLastPlatform == _newPlatform.ID)
		{
			return;
		}

		if (_newPlatform is ForbiddenPlatform)
		{
			_knife.Destruction();

			return;
		}

		if (CheckIDPlatform())
		{
			if (_newPlatform is PlatformWithCheckpoint checkpointPlatform)
			{
				StandKnife();
				SavingPosition(checkpointPlatform);
			}
		}
		else
		{
			_knife.Destruction();
		}
	}

	private bool CheckIDPlatform()
	{
		bool futurePlatform = true;

		if (_idLastPlatform > _newPlatform.ID)
		{
			futurePlatform = false;

			return futurePlatform;
		}

		return futurePlatform;
	}

	private void StandKnife()
	{
		_idLastPlatform = _newPlatform.ID;
	}

	private void SavingPosition(PlatformWithCheckpoint checkpoint)
	{
		_lastSaveCheckpoint = checkpoint.CheckpointTransform;

		if (_platforms[_platforms.Count- 1] == checkpoint)
		{
			FinishGame();
		}
	}

	private void FinishGame()
	{
		_windowBehaviour.EnableRestartWindow();
	}
}
