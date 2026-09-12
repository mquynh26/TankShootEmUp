using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private Transform startShoot;
    [SerializeField] private Transform[] pointShoot;
    [SerializeField] private TurretData turretData;
    private Coroutine _fireRoutine;
 
    private float _fireRateMultiplier = 1f;
    private Coroutine _fireRateBuffRoutine;
 
    private void OnEnable()
    {
        _fireRoutine = StartCoroutine(Fire());
    }
 
    private void OnDisable()
    {
        if (_fireRoutine != null)
        {
            StopCoroutine(_fireRoutine);
            _fireRoutine = null;
        }
    }
    private IEnumerator Fire()
    {
        yield return new WaitUntil(() => transform.position.y <= startShoot.position.y);
        yield return new WaitUntil(() => ObjectPoolManager.Instance != null);
        
        while (true)
        {
            if (turretData.fireMode == FireMode.Single)
            {
                Shoot();
                yield return new WaitForSeconds(turretData.fireRate / _fireRateMultiplier);
            }
            else if (turretData.fireMode == FireMode.Burst)
            {
                for (int i = 0; i < turretData.burstCount; i++)
                {
                    Shoot();
 
                    if (i < turretData.burstCount - 1)
                    {
                        yield return new WaitForSeconds(turretData.burstDelay / _fireRateMultiplier);
                    }
                }
                yield return new WaitForSeconds(turretData.burstCooldown / _fireRateMultiplier);
            }
        }
    }
    
    private void Shoot()
    {
        for (int i = 0; i < pointShoot.Length; i++)
        {
            GameObject bullet = ObjectPoolManager.Instance.Spawn(turretData.bulletType, pointShoot[i].position, pointShoot[i].rotation);
            if (bullet.CompareTag("BulletEnemy1"))
            {
                SoundManager.Instance.PlayEnemyShoot();
            }
            else if (bullet.CompareTag("BulletEnemy2"))
            {
                SoundManager.Instance.PlayEnemyShoot1();
            }
            else SoundManager.Instance.PlayPlayerShoot();
            bullet.GetComponent<Bullet>().Init(turretData);
        }
    }
 
    public void ApplyFireRateBuff(float multiplier, float duration)
    {
        if (_fireRateBuffRoutine != null)
        {
            StopCoroutine(_fireRateBuffRoutine);
        }
 
        _fireRateBuffRoutine = StartCoroutine(FireRateBuffRoutine(multiplier, duration));
    }
 
    private IEnumerator FireRateBuffRoutine(float multiplier, float duration)
    {
        _fireRateMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        _fireRateMultiplier = 1f;
        _fireRateBuffRoutine = null;
    }
}