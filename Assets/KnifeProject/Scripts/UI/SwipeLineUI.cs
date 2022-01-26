using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLineUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] _points;
    
    [SerializeField] private float _minStepForPoints = 20f;

    private Vector3 _startPos;

    private int _amountPoints;

    private void Start()
	{
        _amountPoints = _points.Length;
    }

	public void Moving(Vector3 startPoint, Vector3 directionPoint)
    {
        Vector3 distance = directionPoint - startPoint;

        _startPos = startPoint;

        ChangingPointDistances(distance);
    }

    private void ChangingPointDistances(Vector3 distanceOfMainPoints)
    {
        Vector3 step = distanceOfMainPoints / _amountPoints;

        _points[0].localPosition = _startPos + step;

		Vector3 newPos = Vector3.zero;

		for (int i = 1; i < _amountPoints; i++)
		{
            newPos = _points[i - 1].localPosition + step;

            _points[i].localPosition = newPos;
        }
    }
}
