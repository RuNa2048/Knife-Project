using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperKnife : MonoBehaviour
{
	[Header("Physics parametrs")]
	[SerializeField] private float _kickForce = 5f;
	[SerializeField] private float _torqueForce = 20f;

	[Header("Timings")]
	[SerializeField] private float _detectorPauseTime = 0.5f;

	public event Action<FlipperKnife> OnDestructionKnife;

	private Rigidbody _rigidbody;
	private SphereCollider _platformDetector;



	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_platformDetector = GetComponent<SphereCollider>();
	}

	public void ReductionToLastSafePos(Vector3 pos)
	{
		_rigidbody.velocity = Vector3.zero;
		transform.position = pos;
	}

	public void Moving(Vector2 force)
	{
		StartCoroutine(DetectorDelay());

		_rigidbody.isKinematic = false;

		_rigidbody.AddForce(force * _kickForce, ForceMode.Impulse);
		_rigidbody.AddTorque(_torqueForce, 0f, 0f, ForceMode.Impulse);
	}

	private IEnumerator DetectorDelay()
	{
		_platformDetector.enabled = false;

		yield return new WaitForSeconds(_detectorPauseTime);

		_platformDetector.enabled = true;
	}

	private void OnTriggerEnter()
	{
		_rigidbody.isKinematic = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!_rigidbody.isKinematic)
		{
			Destruction();
		}
	}

	private void Destruction()
	{
		OnDestructionKnife?.Invoke(this);
	}
}
