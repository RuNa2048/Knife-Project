using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
	[Header("Limitation Values")]
	[SerializeField] private float _maxHorizontalSwipe = 1f;
	[SerializeField] private float _stoppingSwipeValue = 0f;

	[Header("References")]
	[SerializeField] private SwipeUI _swipe;

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
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			_swipe.ActivateSwipe(touch, _mainCamera);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					{
						_startSwipePos = _mainCamera.ScreenToViewportPoint(touch.position);


						break;
					}
				case TouchPhase.Ended:
					{
						_endSwipePos = _mainCamera.ScreenToViewportPoint(touch.position);
						Swipe();

						break;
					}
			}
		}
	}

	private void Swipe()
	{
		if (_knife.Flying)
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
		
		if (swipe.y < _stoppingSwipeValue)
		{
			swipe.y = _stoppingSwipeValue;
			swipe.x = _stoppingSwipeValue;
		}

		return swipe;
	}
}
