using UnityEngine;


public class SwipeCreator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private SwipeUI _swipeUI;
    [SerializeField] private PlayerSwipe _playerSwipe;

    [Header("Parents")] 
    [SerializeField] private RectTransform _playerInput;

    public PlayerSwipe CreateSwipe(Knife knife)
    {
        SwipeUI swipeUI = Instantiate(_swipeUI, _playerInput);
        PlayerSwipe playerSwipe = Instantiate(_playerSwipe, transform);
        
        playerSwipe.Initialize(knife, swipeUI);

        return playerSwipe;
    }
}

