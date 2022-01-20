using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{


	private CoinCounter _coinCounter;

	public void Initialithation(CoinCounter counter)
	{
		_coinCounter = counter;

	}

	private void OnTriggerEnter(Collider other)
	{
		_coinCounter.IncreaseAmountOfCoins();

		Destroy(gameObject);
	}
}
