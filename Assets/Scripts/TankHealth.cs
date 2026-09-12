using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private int maxHp;
    public int currentHp;
    public event Action<int, int> OnChangeHp;

    private void OnEnable()
    {
        currentHp = maxHp;
        OnChangeHp?.Invoke(currentHp, maxHp);
    }

    public void TakeDamage(int damage)
    {
        currentHp = currentHp - damage;
        SoundManager.Instance.PlayPlayerHit();
        if (hitFlash != null)
        {
            hitFlash.Flash();
        }
        if (currentHp <= 0)
        {
            currentHp = 0;
            OnChangeHp?.Invoke(currentHp, maxHp);
            GameManager.Instance.GameOver();
            return;
        }
        OnChangeHp?.Invoke(currentHp, maxHp);
    }
    
    public void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp);
        OnChangeHp?.Invoke(currentHp, maxHp);
    }
}
