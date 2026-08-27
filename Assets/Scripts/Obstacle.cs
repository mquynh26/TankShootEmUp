using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageable
{
    private enum DestructibleType
    {
        Indestructible,
        Bullet,
        Tank
    }

    [SerializeField] private DestructibleType destructibleType;
    [SerializeField] private int maxHP = 5;
    [SerializeField] private GameObject baseO;
    [SerializeField] private GameObject decalO;
    private int _currentHP;

    private void OnEnable()
    {
        _currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (destructibleType != DestructibleType.Bullet) return;

        _currentHP = _currentHP - damage;

        if (_currentHP <= 0)
        {
            DestroyObstacle();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destructibleType != DestructibleType.Tank) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyObstacle();
        }
    }

    private void DestroyObstacle()
    {
        GetComponent<Collider2D>().enabled = false;
        baseO.SetActive(false);
        decalO.SetActive(true);
    }
}
