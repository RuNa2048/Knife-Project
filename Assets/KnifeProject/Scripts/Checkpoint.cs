using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint: MonoBehaviour
{
    [SerializeField] private Transform _firstPlatform;

    public Vector3 LastCheckpointPos { get; private set; }

    private Transform _lastCheckpoint;
    private Transform _penultimateCheckpoint;

	private void Start()
	{
        _lastCheckpoint = _firstPlatform;
        _penultimateCheckpoint = _firstPlatform;
    }

	public void SavingPosition(Transform platform)
    {
        if (_lastCheckpoint == platform || _penultimateCheckpoint == platform)
            return;

        _penultimateCheckpoint = _lastCheckpoint;
        _lastCheckpoint = platform;
        LastCheckpointPos = platform.position;
    }
}
