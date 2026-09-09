using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private int maxHp;
    public int currentHp;

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp = currentHp - damage;
        if (hitFlash != null)
        {
            hitFlash.Flash();
        }
        if (currentHp <= 0)
        {
            currentHp = 0;
            //GameOver();
            Debug.Log("Game Over");
        }
    }
}
