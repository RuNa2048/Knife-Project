using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCountUI : MonoBehaviour
{
    [Header("Text Field")]
    [SerializeField] private TextMeshProUGUI _cointsText;

    public void ChangeCount(int count)
    {
        _cointsText.text = $"{count}";
    }
}
