using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperKnife : MonoBehaviour
{
	[Header("Physics parametrs")]
	[SerializeField] private float _kickForce = 5f;
	[SerializeField] private float _kickScale = 1000f;
	[SerializeField] private float _torqueForce = 20f;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Discarding(Vector2 force)
	{
		_rigidbody.isKinematic = false;

		_rigidbody.AddForce(force * _kickForce, ForceMode.Impulse);
		_rigidbody.AddTorque(_torqueForce, 0f, 0f, ForceMode.Impulse);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_rigidbody.isKinematic)
		{
			Debug.Log("Trigger");

			_rigidbody.isKinematic = true;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("Lose");
	}
}
