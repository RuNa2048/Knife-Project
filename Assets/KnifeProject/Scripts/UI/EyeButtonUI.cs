using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EyeButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[Header("References")]
	[SerializeField] private PlayerCamera _camera;
	[SerializeField] private PlayerSwipe _swipe;

	public void OnPointerDown(PointerEventData eventData)
	{
		_camera.MoveForwardToDistance();

		_swipe.enabled = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_camera.ReturnBackToLastPosition();

		_swipe.enabled = true;
	}
}
