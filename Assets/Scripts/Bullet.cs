using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private TurretData _turretData;
    private Rigidbody2D _rb;
    private Vector2 _posSpawn;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(TurretData turretData)
    {
        _turretData = turretData;
        if (gameObject.CompareTag("BulletEnemy"))
        {
            _rb.velocity = (Vector2)transform.up * turretData.bulletSpeed + Vector2.down * PlatMap.Speed;
        }
        else
        {
            _rb.velocity = (Vector2)transform.up * turretData.bulletSpeed;
        }
    }

    private void OnEnable()
    {
        _posSpawn = transform.position;
    }

    public void ReturnPool()
    {
        if(_turretData == null) return;
        ObjectPoolManager.Instance.ReturnPool(_turretData.bulletType, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Obstacle obstacle = collision.GetComponentInParent<Obstacle>();
            if (obstacle != null && (_turretData.bulletType == PoolType.BulletEnemy1 || _turretData.bulletType == PoolType.BulletEnemy2))
            {
                ReturnPool();
                return;
            }
            damageable.TakeDamage(_turretData.bulletDamage);
        }
        ReturnPool();
    }
}
