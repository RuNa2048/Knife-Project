using System.Collections;
using UnityEngine;

public class MovingKnife : MonoBehaviour
{
    [Header("Physics Parameters")]
    [SerializeField] private float _horizontalKickForce = 3f;
    [SerializeField] private float _verticalKickForce = 6f;
    [SerializeField] private float _torqueForce = 20f;
    [SerializeField] private float _minVerticalForce = 0.6f;
    [SerializeField] private float _minHorizontalForce = 0.03f;
    
    [Header("Time Parameters")]
    [SerializeField] private float _delayBeforeDestruction = 0.4f;

    [Header("Transform Settings")]
    [SerializeField] private Vector3 _spawnRotating;
    
    [Header("Reference On Detector")]
    [SerializeField] private KnifePlatformDetector _platformDetector;
    
    private Knife _knife;
    private Rigidbody _rigidbody;
    private PlatformKeeper _platformKeeper;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _knife = GetComponent<Knife>();
    }

    private void Start()
    {
        _knife.OnDestructed += ResetDisplacement;

        transform.eulerAngles = _spawnRotating;
    }
    
    public void Initialize(PlatformKeeper platformKeeper)
    {
        _platformKeeper = platformKeeper;
        
        transform.position = _platformKeeper.LastSaveCheckpointPos;
    }

    private void ResetDisplacement()
    {
        StartCoroutine(ExpectDelay());
    }
    
    private IEnumerator ExpectDelay()
    {
        yield return new WaitForSeconds(_delayBeforeDestruction);

        PutOnOriginalPosition();
    }

    private void PutOnOriginalPosition()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = _platformKeeper.LastSaveCheckpointPos;
        transform.eulerAngles = _spawnRotating;
        
        _knife.StandedToPlatform();
    }

    public void Moving(Vector2 force)
    {
        float standartTorque = 1f;
        
        force.y *= _verticalKickForce;

        if (CheckMinimalVerticalForce(force.y)) return;
        
        if (force.y < 2f && force.y >= 0f)
        {
            int torqueDirection = Random.value < 0.5f ? -1 : 1;

            standartTorque = torqueDirection * 3f;

            if (force.x < 0.1)
                force.x = 0.15f;
        }
        
        Debug.Log($"{standartTorque} {force.x}");
        
        if (force.y > 0f)
        {
            _platformDetector.DisableDetectorTemporarily();
        }
        else
        {
            _platformDetector.EnableTrigger(false);
        }

        if ((force.x >= 0f && force.x < _minHorizontalForce) || (force.x < 0f && force.x > -_minHorizontalForce))
        {
            force.x = _minHorizontalForce;
        }
        else
        {
            force.x *= _horizontalKickForce;
        }
        
        _knife.Jumped();
            
        _rigidbody.AddTorque(_torqueForce * force.x * standartTorque, 0f, 0f, ForceMode.Impulse);
        _rigidbody.AddForce(0, force.y, force.x * standartTorque, ForceMode.Impulse);
    }

    private bool CheckMinimalVerticalForce(float force)
    {
        return (force < _minVerticalForce && force >= 0f) || (force > -_minVerticalForce && force <= 0f);
    }
}