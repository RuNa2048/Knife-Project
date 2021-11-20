using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	[Header("Moving parametrs")]
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private float _repulsiveForce = 1f;
	[SerializeField] private Vector3 movingDirection;

    private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Moving();
		}

		Rotation();
	}

	private void Moving()
	{
		Vector3 velocity = Vector3.zero;
		velocity.y = movingDirection.y * _jumpForce;
		velocity.z = movingDirection.z * _repulsiveForce;

		_rigidbody.velocity = velocity;
	}

	private void Rotation()
	{
		if (_rigidbody.velocity == Vector3.zero)
		{
			return;
		}

		
	}

	private void JumpingRotation()
	{
		if (_rigidbody.velocity.y > 0)
		{
			//Quaternion rotate = Quaternion.Euler(0, 0 ,0);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotate, );

			transform.forward
		}
	}
}
