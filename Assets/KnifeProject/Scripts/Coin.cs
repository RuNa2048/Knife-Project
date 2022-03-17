using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[Header("Moving Characteristics")]
	[SerializeField] private float _movingSpeed = 15f;
	[SerializeField] private float _endYPosition = 10f;
	[SerializeField] private float _horizontalDistance = 5f;
	[SerializeField] private float _movingThreshold = 0.5f;

	[Header("Scale Characteristics")]
	[ SerializeField] private Vector3 _maxScale = new Vector3(1.3f, 1.3f, 1.3f);
	[SerializeField] private float _resizingSpeed = 1f;

	private CoinCounter _coinCounter;

	private Vector3 _mainScale = Vector3.one;
	private Vector3 _endPosition;

	private bool _increase = false;
	private bool _tacked = false;

	public void Initialithation(CoinCounter counter)
	{
		_coinCounter = counter;

	}

	private void OnTriggerEnter(Collider other)
	{
		if (_tacked) 
			return;

		_tacked = true;
		
		_coinCounter.IncreaseAmountOfCoins();

		MoveToEndpoint();	
	}

	private void MoveToEndpoint()
	{
		_endPosition = transform.position;
		_endPosition.x += _horizontalDistance;
		_endPosition.y = _endYPosition;

		StartCoroutine(MoveAndRescale());
	}

	private IEnumerator MoveAndRescale()
	{
		StartCoroutine(ChangeScale());
		yield return StartCoroutine(Move());
	}

	private IEnumerator ChangeScale()
	{
		yield return StartCoroutine(IncreaseScale());

		yield return new WaitForSeconds(0.2f);

		yield return StartCoroutine(ReduseScale());
	}

	private IEnumerator IncreaseScale()
	{
		_increase = true;

		while (transform.localScale.x < _maxScale.x)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, _maxScale, _resizingSpeed * Time.deltaTime);

			yield return null;
		}

		_increase = false;
	}
	
	private IEnumerator ReduseScale()
	{
		while (transform.localScale.x > _mainScale.x)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, _mainScale, _resizingSpeed * Time.deltaTime);

			yield return null;
		}
	}

	private IEnumerator Move()
	{
		while (Vector3.Distance(transform.position, _endPosition) > _movingThreshold)
		{
			transform.position = Vector3.MoveTowards(transform.position, _endPosition, _movingSpeed * Time.deltaTime);

			yield return null;
		}

		Destroy(gameObject);
	}
}
