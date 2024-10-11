using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public bool IsShieldActive { get; private set; } = false;
    
    [Tooltip("Checkbox in the Inspector for Keyboard control")]
    [SerializeField] private bool isKeyBoardControl = false;

    [Header("Speed Variables")]
    [SerializeField] private float minSpeed = 1.0f;
    [SerializeField] private float normalSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float horizontalSpeed = 2.0f;

    [Header("Sprites")]
    [SerializeField] private Sprite[] carSprites = new Sprite[4];

    [Header("Effects")]
    [SerializeField] private GameObject nitroEffect;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject magnetEffect;

    [Header("Sliders")]
    [SerializeField] private Slider nitroSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Slider magnetSlider;

    [Header("Buttons")]
    [SerializeField] private Button gasButton;
    [SerializeField] private Button brakeButton;

    private float _currentSpeed;
    private float _slowdownTime = 10.0f;
    private float _boosterDuration = 15.0f;
    private float _magnetRange = 3.0f;
    private float _moveHorizontal;

    private bool _canMove = false;
    private bool _isNitroActive = false;
    private bool _isMagnetActive = false;

    private SpriteRenderer _spriteRenderer;
    
    private Coroutine _nitroCoroutine;
    private Coroutine _magnetCoroutine;
    private Coroutine _shieldCoroutine;
    private Coroutine _slowdownCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (_canMove)
        {
            Move();
        }
    }

    public void SetMoveActive()
    {
        _canMove = true;
    }
    
    public void SetMoveInactive()
    {
        _canMove = false;
    }

    public void ChangeDirection(float direction)
    {
        _moveHorizontal = direction;
    }

    public void IncreaseSpeed()
    {
        _currentSpeed = maxSpeed;
    }

    public void DecreaseSpeed()
    {
        _currentSpeed = minSpeed;
    }

    public void ReturnNormalSpeed()
    {
        _currentSpeed = _isNitroActive ? maxSpeed : normalSpeed;
    }

    public void NitroSpeed()
    {
        CheckSlowdownCoroutine();
        
        _isNitroActive = true;
        
        _spriteRenderer.sprite = carSprites[3];
        nitroSlider.gameObject.SetActive(true);
        nitroEffect.gameObject.SetActive(true);
        nitroSlider.value = _boosterDuration;

        if (_nitroCoroutine != null)
        {
            StopCoroutine(_nitroCoroutine);
        }
        
        IncreaseSpeed();
        _nitroCoroutine = StartCoroutine(NitroEffect());
    }

    public void ActivateMagnet()
    {
        _isMagnetActive = true;
        _spriteRenderer.sprite = carSprites[1];
        magnetSlider.value = _boosterDuration;
        magnetSlider.gameObject.SetActive(true);
        magnetEffect.gameObject.SetActive(true);

        if (_magnetCoroutine != null)
        {
            StopCoroutine(_magnetCoroutine);
            _magnetCoroutine = null;
        }
        
        _magnetCoroutine = StartCoroutine(MagnetEffect());
    }

    public void ActivateShield()
    {
        IsShieldActive = true;
        _spriteRenderer.sprite = carSprites[2];
        shieldSlider.value = _boosterDuration;
        shieldSlider.gameObject.SetActive(true);
        shieldEffect.gameObject.SetActive(true);

        if (_shieldCoroutine != null)
        {
            StopCoroutine(_shieldCoroutine);
            _shieldCoroutine = null;
        }
        
        _shieldCoroutine = StartCoroutine(ShieldEffect());
    }

    public void ActivateSlowdown()
    {
        CheckNitroCoroutine();
        
        gasButton.interactable = false;
        brakeButton.interactable = false;

        if (_nitroCoroutine != null)
        {
            StopCoroutine(_nitroCoroutine);
        }
        
        DecreaseSpeed();
        _slowdownCoroutine = StartCoroutine(SlowdownEffect());
    }

    private IEnumerator NitroEffect()
    {
        float elapsed = 0.0f;

        while (elapsed < _boosterDuration)
        {
            elapsed += Time.deltaTime;
            nitroSlider.value = _boosterDuration - elapsed;
            yield return null;
        }
        
        _isNitroActive = false;
        UpdateCarVisual();
    }

    private IEnumerator MagnetEffect()
    {
        float elapsed = 0.0f;

        while (elapsed < _boosterDuration)
        {
            elapsed += Time.deltaTime;
            magnetSlider.value = _boosterDuration - elapsed;

            AttractCoins();

            yield return null;
        }

        _isMagnetActive = false;
        UpdateCarVisual();
    }

    private IEnumerator ShieldEffect()
    {
        float elapsed = 0.0f;

        while (elapsed < _boosterDuration)
        {
            elapsed += Time.deltaTime;
            shieldSlider.value = _boosterDuration - elapsed;
            yield return null;
        }

        IsShieldActive = false;
        UpdateCarVisual();
    }

    private IEnumerator SlowdownEffect()
    {
        yield return new WaitForSeconds(_slowdownTime);
        
        SetButtonsActive();
        ReturnNormalSpeed();
    }

    private void CheckSlowdownCoroutine()
    {
        if (_slowdownCoroutine != null)
        {
            StopCoroutine(_slowdownCoroutine);
            _slowdownCoroutine = null;
            
            SetButtonsActive();
        }
    }

    private void CheckNitroCoroutine()
    {
        if (_nitroCoroutine != null)
        {
            StopCoroutine(_nitroCoroutine);
            _nitroCoroutine = null;

            _isNitroActive = false;
            UpdateCarVisual();
            SetButtonsActive();
            
            nitroSlider.gameObject.SetActive(false);
            nitroEffect.gameObject.SetActive(false);
        }
    }

    private void AttractCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag(Tags.COIN_TAG);

        foreach (GameObject coin in coins)
        {
            float distanceToCar = Vector3.Distance(coin.transform.position, transform.position);

            if (distanceToCar <= _magnetRange)
            {
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, 
                    transform.position, Time.deltaTime * maxSpeed);
            }
        }
    }

    private void UpdateCarVisual()
    {
        if (!_isNitroActive)
        {
            nitroSlider.gameObject.SetActive(false);
            nitroEffect.gameObject.SetActive(false);
        }
        else
        {
            _spriteRenderer.sprite = carSprites[3];
        }

        if (!IsShieldActive)
        {
            shieldSlider.gameObject.SetActive(false);
            shieldEffect.gameObject.SetActive(false);
        }
        else
        {
            _spriteRenderer.sprite = carSprites[2];
        }

        if (!_isMagnetActive)
        {
            magnetSlider.gameObject.SetActive(false);
            magnetEffect.gameObject.SetActive(false);
        }
        else
        {
            _spriteRenderer.sprite = carSprites[1];
        }

        if (!_isNitroActive && !IsShieldActive && !_isMagnetActive)
        {
            _spriteRenderer.sprite = carSprites[0];
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.up * _currentSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * _moveHorizontal * horizontalSpeed * Time.deltaTime);

        if (isKeyBoardControl) // Checkbox in the Inspector for Keyboard control
        {
            _moveHorizontal = Input.GetAxis("Horizontal");
        }
    }

    private void SetButtonsActive()
    {
        gasButton.interactable = true;
        brakeButton.interactable = true;
    }
}