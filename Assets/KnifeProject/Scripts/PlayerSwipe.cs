using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
	
	
	private FlipperKnife _knife;

    private Vector2 _startSwipePos;
    private Vector2 _endSwipePos;

	[SerializeField] private Camera _mainCamera;
	
	private void Awake()
	{
		_knife = GetComponent<FlipperKnife>();
	}

	private void Start()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		CheckInput();


	}

	private void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_startSwipePos = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
		}

		if (Input.GetMouseButtonUp(0))
		{
			_endSwipePos = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
			Swipe();
		}
	}

	private void Swipe()
	{
		Vector2 swipeForce = _endSwipePos - _startSwipePos;

		_knife.Discarding(swipeForce);
	}
}
