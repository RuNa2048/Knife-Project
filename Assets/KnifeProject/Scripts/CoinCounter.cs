using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Player _player;
	[SerializeField] private CoinCountUI _coinCountUI;

	private int _currentAmountCoins;
	private int _amountPickedUpCoins;

	private void Start()
	{
		_currentAmountCoins = _player.Coins;

		_coinCountUI.ChangeCount(_currentAmountCoins);
	}

	public void IncreaseAmountOfCoins()
	{
		_currentAmountCoins++;
		_amountPickedUpCoins++;

		_coinCountUI.ChangeCount(_currentAmountCoins);
	}
}
