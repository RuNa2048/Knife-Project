using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBody : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private FlipperKnife _knife;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_knife = GetComponent<FlipperKnife>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!_rigidbody.isKinematic && _knife.CollisionsIsWork)
		{
			Debug.Log(1);

			_knife.Destruction();
		}
	}
}
