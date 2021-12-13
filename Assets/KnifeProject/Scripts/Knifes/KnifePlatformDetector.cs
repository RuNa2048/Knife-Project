using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePlatformDetector : MonoBehaviour
{


	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponentInParent<Rigidbody>();
	}

	private void OnTriggerEnter()
	{
		_rigidbody.isKinematic = true;
	}
}
