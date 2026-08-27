using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;
    [SerializeField] private GameObject baseE;
    [SerializeField] private GameObject turretE;
    [SerializeField] private GameObject decanDie;
    private int _currentHp;

    private void OnEnable()
    {
        _currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        _currentHp = _currentHp - damage;
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        if (GetComponent<EnemyMove>() != null)
        {
            GetComponent<EnemyMove>().enabled = false;
        }
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        baseE.SetActive(false);
        turretE.SetActive(false);
        decanDie.SetActive(true);
    }
}
