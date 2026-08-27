using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyRotation : MonoBehaviour
{
    private enum TypeRotation
    {
        Non,
        FollowPlayer,
        Rotation360,
        Rotation180
    }
    
    
    private GameObject _target;
    [SerializeField] private TypeRotation typeR;
    [SerializeField] float rotateSpeed;
    private float _currentAngle;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _currentAngle = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (typeR == TypeRotation.Non)
        {
            return;
        }
        else if (typeR == TypeRotation.FollowPlayer)
        {
            FollowPlayer();
        }
        else if (typeR == TypeRotation.Rotation360)
        {
            Rotation360();
        }
        else if (typeR == TypeRotation.Rotation180)
        {
            Rotation180();
        }
    }
    
    private void FollowPlayer()
    {
        Vector2 direction = _target.transform.position - transform.position;
        if (direction.sqrMagnitude < 0.001f) return;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void Rotation360()
    {
        _currentAngle = _currentAngle + rotateSpeed * Time.deltaTime;

        if (_currentAngle >= 360f)
        {
            _currentAngle -= 360f;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
    }

    private void Rotation180()
    {
        float angle = Mathf.PingPong(Time.time * rotateSpeed, 180f) + 90f;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
