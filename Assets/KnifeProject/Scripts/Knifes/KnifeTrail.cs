using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class KnifeTrail : MonoBehaviour
{


    private TrailRenderer _trail;
    private Knife _knife;
	private Transform _knifeTransform;

	private void Awake()
	{
		_trail = GetComponent<TrailRenderer>();
		_knife = GetComponentInParent<Knife>();
	}

	private void Start()
	{
		_knifeTransform = _knife.transform;

		_knife.OnJumped += PlayTrailEffect;
	}

	private void PlayTrailEffect()
	{
		StartCoroutine(CalculateRotation());
	}

	private IEnumerator CalculateRotation()
	{
		while (true)
		{
			Debug.Log(_knifeTransform.rotation);

			yield return null;
		}
	}
}
