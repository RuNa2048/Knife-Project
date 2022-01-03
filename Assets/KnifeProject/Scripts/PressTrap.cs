using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTrap : MonoBehaviour
{
	[Header("Timings")]
	[SerializeField] private float _delayTime;

	[Header("Move Parametrs")]
	[SerializeField] private Transform _press;
    [SerializeField] private Vector3 _endPos;
	[SerializeField] private float _movingSpeed = 10f;

    private Vector3 _startPos;

	private bool _isGame = true;

	private void Start()
	{
		_startPos = _press.localPosition;

		StartCoroutine(WaitToMove());
	}

	private IEnumerator WaitToMove()
	{
		while (_isGame)
		{
			yield return new WaitForSeconds(_delayTime);

			yield return StartCoroutine(Move());
		}
	}

	private IEnumerator Move()
	{
		yield return StartCoroutine(MoveToPoint(_endPos));
		yield return StartCoroutine(MoveToPoint(_startPos));
	}

	private IEnumerator MoveToPoint(Vector3 point)
	{
		while (Vector3.Distance(_press.localPosition, point) > float.Epsilon)
		{
			_press.localPosition = Vector3.MoveTowards(_press.localPosition, point, _movingSpeed * Time.deltaTime);

			yield return null;
		}
	}
}
