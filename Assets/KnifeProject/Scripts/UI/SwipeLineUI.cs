using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLineUI : MonoBehaviour
{
    [SerializeField] private RectTransform _startPoint;
    [SerializeField] private RectTransform[] _points;

    private RectTransform _line;
    private Vector3 _targetPos;

    private int _lastNumPoint;
    private int _amountPoints;

    private void Start()
	{
		_line = GetComponent<RectTransform>();

        _amountPoints = _points.Length;
        _lastNumPoint = _points.Length - 1;
    }

	public void Moving(Vector3 directionPoint)
    {
        _targetPos = directionPoint;

        RotationToTargetPoint();
        ChangingPointDistances();
    }

    private void RotationToTargetPoint()
    {
        Vector2 direction = _targetPos - _line.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _line.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void ChangingPointDistances()
    {
        _points[_lastNumPoint].position = _targetPos;

        Vector3 distanceOfMainPoints = _points[_lastNumPoint].localPosition - _startPoint.position;
        Vector3 step = distanceOfMainPoints / _amountPoints;

        Vector3 newPos;

        for (int i = _lastNumPoint - 1; i >= 0; i--)
		{
			newPos = _points[i].localPosition;
			newPos = _points[i + 1].localPosition - step;

			_points[i].localPosition = newPos;
		}
    }
}
