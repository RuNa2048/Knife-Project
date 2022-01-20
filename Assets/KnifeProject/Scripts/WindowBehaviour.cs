using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour: MonoBehaviour
{
	[Header("Windows")]
	[SerializeField] private GameObject _restartWindow;

	public void EnableRestartWindow()
	{
		_restartWindow.SetActive(true);
	}
}
