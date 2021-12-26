using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint: MonoBehaviour
{
    [SerializeField] private List<Platform> _platforms;

    public Vector3 LastCheckpointPos { get { return _lastCheckpoint.position; }  private set { } }

    private Transform _lastCheckpoint;

    private int _currentPlatformNumer;

	private void Start()
	{
        _lastCheckpoint = _platforms[_currentPlatformNumer].CheckpointPosition;
    }

    public bool SavingPosition(Platform newPlatform)
    {
        if (_platforms[_currentPlatformNumer].transform == newPlatform.transform)
        {
            return true;
        }

        if (_platforms[_currentPlatformNumer + 1].transform == newPlatform.transform)
        {
            _currentPlatformNumer++;
            _lastCheckpoint = newPlatform.CheckpointPosition;

            return true;
        }



        return false;
    }
}
