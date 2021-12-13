using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperKnife : MonoBehaviour
{
	[Header("Physics parametrs")]
	[SerializeField] private float _horizontalKickForce = 3f;
	[SerializeField] private float _verticalKickForce = 6f;
	[SerializeField] private float _torqueForce = 20f;

	[Header("Timings")]
	[SerializeField] private float _detectorPauseTime = 0.5f;

	[Header("Positions settings")]
	[SerializeField] private Vector3 _spawnRotating;

	public bool IsFlying { get; private set; } = false;

	public event Action OnStikingKnife;
	public event Action<FlipperKnife> OnDestructionKnife;

	private Rigidbody _rigidbody;
	private SphereCollider[] _colliders;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_colliders = GetComponents<SphereCollider>();

		transform.eulerAngles = _spawnRotating;
	}

	private void Start()
	{
		
	}

	public void ReductionToLastSafePos(Vector3 pos)
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;

		transform.position = pos;
		transform.eulerAngles = _spawnRotating;
	}

	public void Moving(Vector2 force)
	{
		IsFlying = true;
		_rigidbody.isKinematic = false;

		force.x *= _horizontalKickForce;
		force.y *= _verticalKickForce;

		if (force.y > 0f)
		{
			StartCoroutine(DetectorDelay());

			_rigidbody.AddTorque(_torqueForce * force.x, 0f, 0f, ForceMode.Impulse);
		}

		_rigidbody.AddForce(0, force.y, force.x, ForceMode.Impulse);
	}

	private IEnumerator DetectorDelay()
	{
		ChangingStateOfColliders(false);

		yield return new WaitForSeconds(_detectorPauseTime);

		ChangingStateOfColliders(true);
	}

	private void ChangingStateOfColliders(bool state)
	{
		foreach (Collider collider in _colliders)
		{
			collider.enabled = state;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		_rigidbody.isKinematic = true;
		IsFlying = false;

		OnStikingKnife?.Invoke();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!_rigidbody.isKinematic)
		{
			StartCoroutine(DetectorDelay());

			Destruction();
		}
	}

	private void Destruction()
	{
		OnDestructionKnife?.Invoke(this);
	}
}
