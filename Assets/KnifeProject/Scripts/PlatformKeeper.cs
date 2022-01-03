using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeeper: MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private List<Platform> _platforms;
    [SerializeField] private FlipperKnife _knife;

    [SerializeField] private bool _testedMode;

    private Platform _newPlatform;

    public Vector3 LastCheckpointPos { get { return _lastSaveCheckpoint.position; }  private set { } }

    private List<PlatformWithCheckpoint> _checkpointPlatforms;

    private Transform _lastSaveCheckpoint;

    private int _currentPlatformNumber;
    private int _currentNumCheckpointPlatform;

	private void Start()
	{
        _checkpointPlatforms = new List<PlatformWithCheckpoint>();

		foreach (var platform in _platforms)
		{
            if (platform is PlatformWithCheckpoint plat)
            {
                _checkpointPlatforms.Add(plat);
            }
		}

        _lastSaveCheckpoint = _checkpointPlatforms[_currentPlatformNumber].CheckpointPosition;

		foreach (var platform in _platforms)
		{
            platform.Init(this);
		}

    }   

    public void AllowToStand(Platform platform)
    {
        _newPlatform = platform;

        if (_newPlatform is ForbiddenPlatform)
        {
            _knife.Destruction();

            return;
        }

        if (_newPlatform is PlatformWithCheckpoint && CheckPlatformSequence())
        {
            SavingPosition();
        }
    }

    private bool CheckPlatformSequence()
    {
        if (_platforms[_currentPlatformNumber].transform == _newPlatform.transform)
        {
            return false;
        }

        if (_platforms[_currentPlatformNumber + 1].transform == _newPlatform.transform)
        {
            return true;
        }

        if (_checkpointPlatforms[_currentPlatformNumber + 1].transform == _newPlatform.transform)
        {
            return true;
        }

        _knife.Destruction();

        return false;
    }

    private void SavingPosition()
    {
        _currentPlatformNumber++;
        _currentNumCheckpointPlatform++;

        _lastSaveCheckpoint = _checkpointPlatforms[_currentNumCheckpointPlatform].CheckpointPosition;

        if (_checkpointPlatforms.Count - 1 == _currentNumCheckpointPlatform)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        _currentNumCheckpointPlatform = 0;
        _currentPlatformNumber = 0;
        _lastSaveCheckpoint = _checkpointPlatforms[_currentNumCheckpointPlatform].CheckpointPosition;

        _knife.Destruction();
    }
}
