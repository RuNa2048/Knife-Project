using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
	[Header("Limitation Values")]
	[SerializeField] private float _maxHorizontalSwipe = 1f;
	[SerializeField] private float _minVerticalSwipe = 1f;
	
	private FlipperKnife _knife;

    private Vector2 _startSwipePos;
    private Vector2 _endSwipePos;

	private Camera _mainCamera;
	
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
		if (_knife.IsFlying)
		{
			return;
		}

		Vector2 swipeForce = _endSwipePos - _startSwipePos;

		swipeForce = CheckSwipeForValidValues(swipeForce);

		_knife.Moving(swipeForce);
	}

	private Vector2 CheckSwipeForValidValues(Vector2 swipe)
	{
		if (swipe.x > _maxHorizontalSwipe)
		{
			swipe.x = _maxHorizontalSwipe;
		}

		if (swipe.x < - _maxHorizontalSwipe)
		{
			swipe.x = - _maxHorizontalSwipe;
		}
		
		if (swipe.y < _minVerticalSwipe)
		{
			swipe.y = _minVerticalSwipe;
		}

		return swipe;
	}
}
