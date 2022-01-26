using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
    [Header("Swipe Objects")]
    [SerializeField] private RectTransform _startSwipe;
    [SerializeField] private RectTransform _endSwipe;
    [SerializeField] private RectTransform _linePosition;

    private List<RectTransform> _swipeElements;

    private RectTransform _swipeCanvas;
    private SwipeLineUI _swipeLine;

	private void Awake()
	{
        _swipeCanvas = GetComponent<RectTransform>();
       _swipeLine = _linePosition.GetComponent<SwipeLineUI>();
	}

	private void Start()
	{
        _swipeElements = new List<RectTransform>();

        _swipeElements.Add(_startSwipe);
        _swipeElements.Add(_endSwipe);
        _swipeElements.Add(_linePosition);

        ChangeSwipeCondition(false);
    }

	public void ActivateSwipe(Touch touch, Camera camera)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                {
                    _startSwipe.gameObject.SetActive(true);

                    Vector2 localPos = CalculatePositionInRect(touch.position, camera);

                    //_linePosition.localPosition = localPos;
                    _startSwipe.localPosition = localPos;

                    break;
                }
            case TouchPhase.Moved:
                {
                    ChangeSwipeCondition(true);

                    _endSwipe.localPosition = CalculatePositionInRect(touch.position, camera);

                    _swipeLine.Moving(_startSwipe.localPosition,  _endSwipe.localPosition);

                    break;
                }
            case TouchPhase.Ended:
                {
                    _startSwipe.localPosition = Vector2.zero;
                    _endSwipe.localPosition = Vector2.zero;
                    _linePosition.localPosition = Vector2.zero;

                    ChangeSwipeCondition(false);

                    break;
                }
        }
    }

    private Vector2 CalculatePositionInRect(Vector2 pos, Camera camera)
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_swipeCanvas, pos, camera, out movePos);

        return movePos;
    }

    private void ChangeSwipeCondition(bool condition)
    {
        foreach (RectTransform rect in _swipeElements)
        {
            rect.gameObject.SetActive(condition);
        }
    }
}
