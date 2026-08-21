using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyRotation : MonoBehaviour
{
    private GameObject _target;
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
        FollowPlayer();
    }
    
    private void FollowPlayer()
    {
        Vector2 direction = _target.transform.position - transform.position;

        if (direction.sqrMagnitude < 0.001f)
            return;

        float targetAngle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotation =
            Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }
}
