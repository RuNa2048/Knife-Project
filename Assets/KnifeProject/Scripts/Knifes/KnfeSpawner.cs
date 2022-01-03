using System.Collections;
using UnityEngine;

public class KnfeSpawner : MonoBehaviour
{
	[Header("References")]
    [SerializeField] FlipperKnife[] _knifes;
	[SerializeField] PlatformKeeper  _platformKeeper;

	[Header("Settings For Return Knife On PlatformKeeper Pos")]
	[SerializeField] private float _delayTime;

	private void Start()
	{
		foreach (FlipperKnife knife in _knifes)
		{
			knife.OnDestructionKnife += SpawnKnife;
		}
	}

	private void SpawnKnife(FlipperKnife knife)
	{
		StartCoroutine(HolpUpTimeAndReturnKnife(knife));
	}

	private IEnumerator HolpUpTimeAndReturnKnife(FlipperKnife knife)
	{
		yield return new WaitForSeconds(_delayTime);

		knife.ReductionToLastSafePos(_platformKeeper.LastCheckpointPos);
	}
}
