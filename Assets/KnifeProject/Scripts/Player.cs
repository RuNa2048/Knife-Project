using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private int _coins;
    public int Coins => _coins;



    public void CoinsAccrual(int coinsAmount)
    {
        _coins += coinsAmount;
    }
}
