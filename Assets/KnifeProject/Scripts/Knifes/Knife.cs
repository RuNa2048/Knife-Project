using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knife : MonoBehaviour
{
	[Header("Time Parameters")]
	[SerializeField] private float _delayBeforeDestruction = 0.4f;
	[SerializeField] private float _collisionPauseTime = 0.5f;

	public bool CollisionsIsWork => _collisionsIsWork;
	public bool SaveCheckpoint => _saveCheckpoint;
	public bool IsKinematic => _rigidbody.isKinematic;
	
	
	public event Action OnStandedToPlatform;
	public event Action OnJumped;
	public event Action OnDestructed;
	
	private Rigidbody _rigidbody;

	private bool _collisionsIsWork = true;
	private bool _saveCheckpoint = true;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	private void Start()
	{
		StandedToPlatform();
	}
	
	public void StandedToPlatform()
	{
		_rigidbody.isKinematic = true;
		
		_saveCheckpoint = true;
		
		OnStandedToPlatform?.Invoke();
	}

	public void Jumped()
	{
		_rigidbody.isKinematic = false;

		StartCoroutine(DetectorDelay());
		
		OnJumped?.Invoke();
	}

	public void Destructed()
	{
		_saveCheckpoint = false;
		
		ChangeCollisionState(false);
		
		OnDestructed?.Invoke();
	}
	
	private IEnumerator DetectorDelay()
	{
		ChangeCollisionState(false);

		yield return new WaitForSeconds(_collisionPauseTime);

		ChangeCollisionState(true);
	}

	private void ChangeCollisionState(bool state)
	{
		_collisionsIsWork = state;
	}
}
