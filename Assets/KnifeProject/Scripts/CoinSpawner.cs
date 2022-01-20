using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private Coin[] _coins;

	[Header("Coin Counter")]
	[SerializeField] private CoinCounter _coinCounter;

	private void Start()
	{
		foreach (var coin in _coins)
		{
			coin.Initialithation(_coinCounter);
		}
	}
}
