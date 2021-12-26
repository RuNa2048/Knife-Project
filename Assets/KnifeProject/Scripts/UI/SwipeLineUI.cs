using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLineUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] _points;

    private RectTransform _line;

    private int _lastNumPoint;


    private void Start()
	{
		_line = GetComponent<RectTransform>();

        _lastNumPoint = _points.Length - 1;
    }

	public void Moving(Vector3 directionPoint)
    {
        RotationToTargetPoint(directionPoint);
        ChangingPointDistances(directionPoint);
    }

    private void RotationToTargetPoint(Vector3 targetPos)
    {
        Vector2 direction = targetPos - _line.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _line.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void ChangingPointDistances(Vector3 targetPos)
    {
        _points[_lastNumPoint].position = targetPos;

        Vector3 distanceOfMainPoints = _points[_lastNumPoint].localPosition - _points[0].localPosition;
        Vector3 step = distanceOfMainPoints / _points.Length;

        int midNumPoint = _points.Length / 2;

        for (int i = 1; i < midNumPoint; i++)
		{
			_points[i].localPosition = _points[i - 1].localPosition + step;

            int oppositePointNum = (_points.Length - 1) - i;

            _points[oppositePointNum].localPosition = _points[oppositePointNum + 1].localPosition - step;
		}

        _points[midNumPoint].localPosition = distanceOfMainPoints / 2;
    }
}
