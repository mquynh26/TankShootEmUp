using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameObject baseTank;
    [SerializeField] private GameObject pointPlayer;
    public float moveSpeed = 7f;
    private bool _isMove = false;
    private Vector2 _moveDirection;
    private float _minDistance = 0.1f;
    private Rigidbody2D _rb;
    private Camera _camera;
    private float _zRotationBaseTank;
    private float _baseMoveSpeed;
    private Coroutine _speedBuffRoutine;
 
    private void Awake()
    {
        _baseMoveSpeed = moveSpeed;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _zRotationBaseTank = baseTank.transform.rotation.eulerAngles.z;
        gameObject.transform.position = pointPlayer.transform.position;
    }
 
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    void FixedUpdate()
    {
        TankMove();
    }
    
    void GetInput()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            _isMove = false;
            _moveDirection = Vector2.zero;
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 tankPos = _rb.position;
            Vector2 moveDirRaw = mousePos - tankPos;
            if (moveDirRaw.magnitude > _minDistance)
            {
                _moveDirection = (mousePos - tankPos).normalized;
                _isMove = true;
            }
            else
            {
                _isMove = false;
                _moveDirection = Vector2.zero;
            }
        }
        else
        {
            _isMove = false;
            _moveDirection = Vector2.zero;
        }
    }
    
    void TankMove()
    {
        if (_isMove)
        {
            _rb.MovePosition(_rb.position + _moveDirection * (moveSpeed*Time.fixedDeltaTime));
        }
        RotateTank();
    }
 
    void RotateTank()
    {
        float angle;
        float targetAngle;
 
        if (_isMove)
        {
            angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
            if (angle > 0)
                targetAngle = angle - 90f;
            else
            {
                targetAngle = angle + 90f;
            }
        }
        else
        {
            targetAngle = _zRotationBaseTank;
        }
 
        float currentAngle = baseTank.transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotateSpeed *  Time.fixedDeltaTime);
 
        baseTank.transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
 
    public void ApplySpeedBuff(float multiplier, float duration)
    {
        if (_speedBuffRoutine != null)
        {
            StopCoroutine(_speedBuffRoutine);
        }
 
        _speedBuffRoutine = StartCoroutine(SpeedBuffRoutine(multiplier, duration));
    }
 
    private IEnumerator SpeedBuffRoutine(float multiplier, float duration)
    {
        moveSpeed = _baseMoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = _baseMoveSpeed;
        _speedBuffRoutine = null;
    }
}