using System;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Reference To Creators")] 
    [SerializeField] private SwipeCreator _swipeCreator;
    [SerializeField] private KnifeCreator _knifeCreator;
    [SerializeField] private PlatformKeeper _platformKeeper;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private EyeButtonUI _eyeButton;

    public event Action OnLevelStated;
    
    private void Start()
    {
        CreateMainObjects();
    }

    private void CreateMainObjects()
    {
        _platformKeeper.InitializeAllPlatforms();
        
        Knife knife = _knifeCreator.CreateKnife();

        _swipeCreator.CreateSwipe(knife);
        
        _playerCamera.Initialize(knife);
        _eyeButton.InitializeCamera(_playerCamera);
    }
}
