using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnifePart : MonoBehaviour
{
    protected Rigidbody rigidbody;
    protected FlipperKnife knife;

	private void Awake()
	{
		rigidbody = GetComponentInParent<Rigidbody>();
		knife = GetComponentInParent<FlipperKnife>();
	}
}
