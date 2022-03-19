using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeeper : MonoBehaviour
{
	[Header("References To Platforms")]
	[SerializeField] private List<Platform> _platforms;

	[Header("References")]
	[SerializeField] private WindowBehaviour _windowBehaviour;
	
	private Knife _knife;
	
	private Transform _lastSaveCheckpoint;
	public Vector3 LastSaveCheckpointPos => _lastSaveCheckpoint.position;

	private Platform _newPlatform;

	private int _idLastPlatform = -1;

	public void InitializeAllPlatforms()
	{
		int platformID = 0;

		foreach (var platform in _platforms)
		{
			platform.Init(this, platformID);

			platformID++;
		}

		AllowToStand(_platforms[0]);
	}

	public void GoToNextPlatform()
	{
		AllowToStand(_platforms[_idLastPlatform + 1]);
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
			_knife.Destructed();

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
			_knife.Destructed();
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
