using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EyeButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[Header("References")]
	[SerializeField] private PlayerCamera _camera;
	//[SerializeField] private PlayerSwipe _swipe;

	public void InitializeCamera(PlayerCamera camera) => _camera = camera;

	public void OnPointerDown(PointerEventData eventData)
	{
		_camera.MoveForwardToEyeDistance();

		//_swipe.enabled = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_camera.MoveToKnife();

		//_swipe.enabled = true;
	}
}
