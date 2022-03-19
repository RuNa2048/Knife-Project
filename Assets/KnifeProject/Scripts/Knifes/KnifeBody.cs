using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Knife))]
public class KnifeBody : MonoBehaviour
{
	private Knife _knife;

	private void Awake()
	{
		_knife = GetComponent<Knife>();
	}

	private void OnCollisionEnter(Collision collision)
	{ 
		if (_knife.IsKinematic || !_knife.CollisionsIsWork)
			return;

		_knife.Destructed();
	}
}
