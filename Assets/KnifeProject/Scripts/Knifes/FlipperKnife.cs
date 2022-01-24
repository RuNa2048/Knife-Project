using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlipperKnife : MonoBehaviour
{
	[SerializeField] private bool _testMode = false;

	[Header("Physics Parametrs")]
	[SerializeField] private float _horizontalKickForce = 3f;
	[SerializeField] private float _verticalKickForce = 6f;
	[SerializeField] private float _torqueForce = 20f;
	[SerializeField] private float _minVerticalForce = 0.6f;
	[SerializeField] private float _minHorizontalForce = 0.03f;

	[Header("Timings")]
	[SerializeField] private float _detectorPauseTime = 0.5f;

	[Header("Transform Settings")]
	[SerializeField] private Vector3 _spawnRotating;

	[Header("References")]
	[SerializeField] private KnifePlatformDetector _detector;

	public bool Flying => _inFlight;
	public bool CollisionsIsWork => _collisionsIsWork;
	public bool PlatformDetectorIsWork => _platformDetectorIsWork;
	public bool SaveCheckpoint => _saveCheckpoint;

	public event Action OnStikingKnife;
	public event Action<FlipperKnife> OnDestructionKnife;

	public Vector3 LastPositionOnPlatform => _lastPositionOnPlatform;

	private Rigidbody _rigidbody;
	private SphereCollider _platformDetectorCollider;
	private Vector3 _lastPositionOnPlatform;

	private bool _inFlight = false;
	private bool _collisionsIsWork = true;
	private bool _platformDetectorIsWork = true;
	private bool _saveCheckpoint = true;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_platformDetectorCollider = _detector.GetComponent<SphereCollider>();
	}

	private void Start()
	{
		transform.eulerAngles = _spawnRotating;
	}

	public void ReductionToLastSafePos(Vector3 pos)
	{
		ChangePlatformDetectorSettings(true);

		_collisionsIsWork = true;

		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		_rigidbody.isKinematic = false;

		transform.position = pos;
		transform.eulerAngles = _spawnRotating;
	}

	public void Moving(Vector2 force)
	{
		force.y *= _verticalKickForce;

		if (CheckVerticalForce(force.y))
		{
			_rigidbody.isKinematic = false;
			_inFlight = true;
			_saveCheckpoint = false;

			if (force.y > 0f)
			{
				StartCoroutine(DetectorDelay());
			}
			else
			{
				if (!_testMode)
				{
					ChangePlatformDetectorSettings(false);
				}
			}

			if ((force.x >= 0f && force.x < _minHorizontalForce) || (force.x < 0f && force.x > -_minHorizontalForce))
			{
				force.x = _minHorizontalForce;
			}
			else
			{
				force.x *= _horizontalKickForce;
			}

			_rigidbody.AddTorque(_torqueForce * force.x, 0f, 0f, ForceMode.Impulse);
			_rigidbody.AddForce(0, force.y, force.x, ForceMode.Impulse);
		}
	}

	private bool CheckVerticalForce(float force)
	{
		if ((force < _minVerticalForce && force >= 0f) || (force > -_minVerticalForce && force <= 0f))
		{
			return false;
		}

		return true;
	}

	private IEnumerator DetectorDelay()
	{
		ChangingStateOfColliders(false);

		yield return new WaitForSeconds(_detectorPauseTime);

		ChangingStateOfColliders(true);
	}

	private void ChangingStateOfColliders(bool state)
	{
		_collisionsIsWork = state;
	}

	private void ChangePlatformDetectorSettings(bool state)
	{
		_platformDetectorCollider.isTrigger = state;
		_platformDetectorIsWork = state;
	}

	public void StandToPlatform()
	{

		_rigidbody.isKinematic = true;
		_inFlight = false;
		_saveCheckpoint = true;

		_lastPositionOnPlatform = transform.position;

		OnStikingKnife?.Invoke();
	}

	public void Destruction()
	{
		_saveCheckpoint = false;
		_platformDetectorIsWork = false;

		OnDestructionKnife?.Invoke(this);
	}
}
