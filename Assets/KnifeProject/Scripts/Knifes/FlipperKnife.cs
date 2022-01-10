using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperKnife : MonoBehaviour
{
	[Header("Physics Parametrs")]
	[SerializeField] private float _horizontalKickForce = 3f;
	[SerializeField] private float _verticalKickForce = 6f;
	[SerializeField] private float _torqueForce = 20f;

	[Header("Timings")]
	[SerializeField] private float _detectorPauseTime = 0.5f;

	[Header("Transform Settings")]
	[SerializeField] private Vector3 _spawnRotating;
	[SerializeField] private float _minStandRotation = 0.86f;

	public bool Flying => _flying;
	public bool IgnoreCollisions => _ignoreCollisions;

	public event Action OnStikingKnife;
	public event Action<FlipperKnife> OnDestructionKnife;

	private Rigidbody _rigidbody;

	private bool _jumping = false;
	private bool _flying = false;
	private bool _ignoreCollisions = false;
	private string _platformTag;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		transform.eulerAngles = _spawnRotating;

		_platformTag = ConstantsGameTags.PlatformTextTag;
	}

	public void ReductionToLastSafePos(Vector3 pos)
	{
		_ignoreCollisions = false;

		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;

		transform.position = pos;
		transform.eulerAngles = _spawnRotating;
	}

	public void Moving(Vector2 force)
	{
		_flying = true;
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
		_jumping = !state;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(_platformTag) && !_jumping && !_ignoreCollisions)
		{
			if (CheckAngleOfInclination())
			{
				return;
			}

			_rigidbody.isKinematic = true;
			_flying = false;

			OnStikingKnife?.Invoke();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!_rigidbody.isKinematic && !_jumping && !_ignoreCollisions)
		{
			StartCoroutine(DetectorDelay());

			Destruction();
		}
	}

	public void Destruction()
	{
		_ignoreCollisions = true;

		OnDestructionKnife?.Invoke(this);
	}

	private bool CheckAngleOfInclination()
	{
		float currentXRot = transform.rotation.x;

		if (currentXRot > _minStandRotation || currentXRot < -_minStandRotation)
		{
			return false;
		}

		return true;
	}
}
