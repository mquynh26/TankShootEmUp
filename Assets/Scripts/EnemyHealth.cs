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
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private bool isDron = false;
    private int _currentHp;

    private void OnEnable()
    {
        _currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (transform.position.y <= damagePoint.position.y)
        {
            _currentHp = _currentHp - damage;
            SoundManager.Instance.PlayHit();
            if (hitFlash != null)
            {
                hitFlash.Flash();
            }

            if (_currentHp <= 0)
            {
                _currentHp = 0;
                Die();
            }
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
        SoundManager.Instance.PlayEnemyDeath();

        if (isDron)
        {
            ObjectPoolManager.Instance.Spawn(PoolType.Buff,  transform.position, Quaternion.identity);
        }
    }
}
