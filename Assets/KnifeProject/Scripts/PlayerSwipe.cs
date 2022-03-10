using System;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
	[Header("Limitation Values")]
	[SerializeField] private float _maxHorizontalSwipe = 1f;
	[SerializeField] private float _stoppingSwipeValue = 0f;

	private SwipeUI _swipe;
	private Knife _knife;
	private MovingKnife _movingKnife;
	private Camera _mainCamera;

    private Vector2 _startSwipePos;
    private Vector2 _endSwipePos;
    
    private bool _isBlocked = false;
    
    public void Initialize(Knife knife, SwipeUI swipeView)
    {
	    _knife = knife;
	    _swipe = swipeView;
	    
	    _movingKnife = _knife.GetComponent<MovingKnife>();
		
	    _mainCamera = Camera.main;
	    
	    _swipe.AssighnCamera(_mainCamera);
	    
	    _knife.OnJumped += DisableSwipe;
	    _knife.OnStandedToPlatform += ActivateSwipe;
    }

    private void Update()
	{
		CheckInput();
	}

	private void CheckInput()
	{
#if UNITY_EDITOR
		
		Vector3 mousePosition = Input.mousePosition;
		
		if (Input.GetMouseButtonDown(0))
		{
			_swipe.ActivateSwipe(mousePosition);
			
			_startSwipePos = _mainCamera.ScreenToViewportPoint(mousePosition);
		}

		if (Input.GetMouseButton(0))
		{
			_swipe.UpdateCursor(mousePosition);
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			_endSwipePos = _mainCamera.ScreenToViewportPoint(mousePosition);
				
			_swipe.TurnOff();
				
			Swipe();
		}
#elif  UNITY_ANDROID

		if (Input.touchCount == 0)
			return;

		Touch touch = Input.GetTouch(0);

		_swipe.ActivateSwipe(touch.position);

		switch (touch.phase)
		{
			case TouchPhase.Began:
			{
				_startSwipePos = _mainCamera.ScreenToViewportPoint(touch.position);

				break;
			}
			case TouchPhase.Moved:
			{
				_swipe.UpdateCursor(touch.position);

				break;
			}
			case TouchPhase.Ended:
			{
				_endSwipePos = _mainCamera.ScreenToViewportPoint(touch.position);
				
				_swipe.TurnOff();
				
				Swipe();

				break;
			}
		}
#endif
	}

	private void Swipe()
	{
		if (_isBlocked)
			return;

		Vector2 swipeForce = _endSwipePos - _startSwipePos;
		swipeForce = CheckSwipeForValidValues(swipeForce);

		_movingKnife.Moving(swipeForce);
	}

	private Vector2 CheckSwipeForValidValues(Vector2 swipe)
	{
		swipe.x = Mathf.Clamp(swipe.x, -_maxHorizontalSwipe, _maxHorizontalSwipe);

		return swipe;
	}

	private void DisableSwipe()
	{
		_isBlocked = true;
	}
	
	private void ActivateSwipe()
	{
		_isBlocked = false;
	}

	private void OnDestroy()
	{
		_knife.OnJumped -= DisableSwipe;
		_knife.OnStandedToPlatform -= ActivateSwipe;
	}
}
