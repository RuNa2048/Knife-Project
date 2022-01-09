using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeeper : MonoBehaviour
{
	[Header("Refrences")]
	[SerializeField] private List<Platform> _platforms;
	[SerializeField] private FlipperKnife _knife;

	[SerializeField] private bool _testedMode;

	private Transform _lastSaveCheckpoint;
	public Vector3 LastSaveCheckpointPos() => _lastSaveCheckpoint.position;

	private Platform _newPlatform;
	private PlatformWithCheckpoint _firstCheckpoint;

	private int _idLastPlatform = -1;

	private void Start()
	{
		foreach (var platform in _platforms)
		{
			if (platform is PlatformWithCheckpoint plat)
			{
				_firstCheckpoint = plat;

				break;
			}
		}

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
			if (_newPlatform is PlatformWithoutCheckpoint)
			{
				StandKnife();
			}

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
		_lastSaveCheckpoint = _firstCheckpoint.CheckpointTransform;
		_idLastPlatform = -1;

		_knife.Destruction();
	}
}
