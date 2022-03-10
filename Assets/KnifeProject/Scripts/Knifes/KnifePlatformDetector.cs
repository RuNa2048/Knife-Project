using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class KnifePlatformDetector : MonoBehaviour
{
	[Header("Rotation Parameters")] [SerializeField]
	private float _minStandRotation = 0.86f;

	[Header("Timings")] 
	[SerializeField] private float _detectorPauseTime = 0.5f;

	private Knife _knife;
	private SphereCollider _platformDetectorCollider;

	private string _platformTag;
	private bool _platformDetectorIsWork = false;

	private void Awake()
	{
		_knife = GetComponentInParent<Knife>();
		_platformDetectorCollider = GetComponent<SphereCollider>();
	}

	private void Start()
	{
		_platformTag = ConstantsGameTags.PlatformTextTag;

		_knife.OnDestructed += EnablePlatformDetector;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (_knife.IsKinematic || !other.CompareTag(_platformTag) || !_platformDetectorIsWork) 
			return;

		if (CheckAngleOfInclination())
		{
			_knife.Destructed();
			
			return;
		}

		TriggeredWithPlatform();
	}

	private void TriggeredWithPlatform()
	{
		_knife.StandedToPlatform();
	}

	private bool CheckAngleOfInclination()
	{
		var currentXRot = _knife.transform.rotation.x;
		
		Debug.Log(currentXRot);

		return currentXRot > _minStandRotation || currentXRot < -_minStandRotation;
	}

	public void DisableDetectorTemporarily()
	{
		EnableTrigger(true);

		StartCoroutine(DetectorDelay());
	}
	
	public void EnableTrigger(bool state)
	{
		_platformDetectorCollider.isTrigger = state;
		
		ChangePlatformDetectorState(state);
	}

	private IEnumerator DetectorDelay()
	{
		ChangePlatformDetectorState(false);

		yield return new WaitForSeconds(_detectorPauseTime);

		ChangePlatformDetectorState(true);
	}

	private void ChangePlatformDetectorState(bool state)
	{
		_platformDetectorIsWork = state;
	}

	private void EnablePlatformDetector() => ChangePlatformDetectorState(false);
}
