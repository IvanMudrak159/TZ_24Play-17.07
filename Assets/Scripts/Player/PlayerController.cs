using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _sensivity = 1f;
    [Range(1, 10), SerializeField] private int _speed;

    [SerializeField] private Transform _platformFloor;
    [SerializeField] private Transform _playerPlatformPrefab;
    private float _platformWidthOffset;
    private float _playersCubeWidthOffset;
    private float _playersCubeHeight;

    [SerializeField] private PickupHandler _pickupCollector;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _characterPart;
    [SerializeField] private Transform _cubeHolder;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private ParticleSystem _pickupParticle;
    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private RagdollController _stickmanRagdollController;
    [SerializeField] private GameObject _textPickupPrefab;
    [SerializeField] private float _textLifetime = 1.5f;

    private CubeObjectPool _cubeObjectPool;
    private int _cubeCount = 1;
    private int _jumpHash;

    public UnityEvent OnLose;
    private void Start()
    {
        _jumpHash = Animator.StringToHash("Jump");
        _pickupCollector.OnPickedUp.AddListener(OnPickup);
        _playerInteractor.OnWallHit.AddListener(OnGameLose);
        _stickmanRagdollController.SetRagdoll(false);

        _platformWidthOffset = _platformFloor.localScale.x * 0.5f;
        _playersCubeWidthOffset = _playerPlatformPrefab.localScale.x * 0.5f;
        _playersCubeHeight = _playerPlatformPrefab.localScale.y;

        _cubeObjectPool = _cubeHolder.GetComponent<CubeObjectPool>();
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                float newXPosition = transform.position.x - touchDeltaPosition.x * _sensivity * Time.deltaTime;
                float clampedXPosition = Mathf.Clamp(newXPosition, -_platformWidthOffset + _playersCubeWidthOffset, _platformWidthOffset - _playersCubeWidthOffset);
                transform.position = new Vector3(clampedXPosition, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnPickup()
    {
        _playerAnimator.SetTrigger(_jumpHash);
        _characterPart.position += Vector3.up * (_playersCubeHeight);
        _cubeHolder.position += Vector3.up * (_playersCubeHeight);
        GameObject cube = _cubeObjectPool.GetCubeFromPool();
        cube.transform.localPosition = -Vector3.up * (_playersCubeHeight) * _cubeCount++;

        GameObject text = Instantiate(_textPickupPrefab);
        text.transform.position = new Vector3(_characterPart.position.x, _characterPart.position.y, _characterPart.position.z - _playersCubeWidthOffset);
        Destroy(text, _textLifetime);
        
        _pickupParticle.Play();
    }

    private void OnGameLose()
    {
        OnLose?.Invoke();
        _stickmanRagdollController.SetRagdoll(true);
        _playerAnimator.enabled = false;
        this.enabled = false;
    }
}

