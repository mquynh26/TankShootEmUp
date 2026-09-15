using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject baseM;
    [SerializeField] private GameObject decalM;
    [SerializeField] private int damage;
    [SerializeField] private GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (collision.CompareTag("Player"))
            {
                damageable.TakeDamage(damage);
            }
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            if (deathEffect != null)
            {
                deathEffect.SetActive(true);
            }
            SoundManager.Instance.PlayMine();
            baseM.SetActive(false);
            decalM.SetActive(true);
        }
    }
}
