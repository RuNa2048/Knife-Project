using System;
using UnityEngine;

public class KnfeSpawner : MonoBehaviour
{
	[Header("References")]
    [SerializeField] FlipperKnife[] _knifes;
	[SerializeField] CheckpointsPull _pull;

	private void Start()
	{
		foreach (FlipperKnife knife in _knifes)
		{
			knife.OnDestructionKnife += SpawnKnife;
		}
	}

	private void SpawnKnife(FlipperKnife knife)
	{
		knife.ReductionToLastSafePos(_pull.LastCheckpointPos);
	}
}
