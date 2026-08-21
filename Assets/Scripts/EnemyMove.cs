using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum MoveType
    {
        None,
        Horizontal,
        Vertical
    }

    [SerializeField] private MoveType moveType;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private GameObject enemyBase;
    private Vector3 _startPosition;
    private float _direction = 1f;

    private void OnEnable()
    {
        Rotate();
        _startPosition = transform.localPosition;
        _direction = 1f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (moveType == MoveType.None)
            return;

        Vector3 pos = transform.localPosition;

        if (moveType == MoveType.Horizontal)
        {
            pos.x += _direction * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(pos.x - _startPosition.x) >= moveDistance)
            {
                _direction *= -1f;
            }
        }
        else if (moveType == MoveType.Vertical)
        {
            pos.y += _direction * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(pos.y - _startPosition.y) >= moveDistance)
            {
                _direction *= -1f;
            }
        }

        transform.localPosition = pos;
    }

    private void Rotate()
    {
        if (moveType == MoveType.None)
            return;
        if (moveType == MoveType.Horizontal)
        {
            enemyBase.transform.Rotate(0f, 0f, 90f);
        } else if (moveType == MoveType.Vertical)
        {
            enemyBase.transform.Rotate(0f, 0f, 0);
        }
    }
}
