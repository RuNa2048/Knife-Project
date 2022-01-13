using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePlatformDetector : KnifePart
{
	[Header("Rotation Parametrs")]
	[SerializeField] private float _minStandRotation = 0.86f;

	private string _platformTag;

	private void Start()
	{
		_platformTag = ConstantsGameTags.PlatformTextTag;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(_platformTag) && knife.CollisionsIsWork && knife.PlatformDetectorIsWork)
		{
			if (CheckAngleOfInclination())
			{
				return;
			}

			knife.StandToPlatform();
		}
	}

	private bool CheckAngleOfInclination()
	{
		float currentXRot = knife.transform.rotation.x;

		if (currentXRot > _minStandRotation || currentXRot < -_minStandRotation)
		{
			return false;
		}

		return true;
	}
}
