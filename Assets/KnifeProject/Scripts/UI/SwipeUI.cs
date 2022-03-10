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

    [SerializeField] private RectTransform _swipeCanvas;
    private SwipeLineUI _swipeLine;
    private Camera _camera;

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

        _swipeLine.Initialize();

        ChangeSwipeCondition(false);
    }

    public void AssighnCamera(Camera camera)
    {
        _camera = camera;
    }

    public void ActivateSwipe(Vector2 position)
    {
        _startSwipe.gameObject.SetActive(true);

        Vector2 localPos = CalculatePositionInRect(position);

        _startSwipe.localPosition = localPos;
    }

    public void UpdateCursor(Vector2 position)
    {
        ChangeSwipeCondition(true);

        _endSwipe.localPosition = CalculatePositionInRect(position);

        _swipeLine.Moving(_startSwipe.localPosition,  _endSwipe.localPosition);
    }

    public void TurnOff()
    {
        foreach (var rect in _swipeElements)
        {
            rect.localPosition = Vector2.zero;
        }
        
        ChangeSwipeCondition(false);
    }

    private Vector2 CalculatePositionInRect(Vector2 pos)
    {
        Vector2 movePos;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_swipeCanvas, pos, _camera, out movePos);

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
