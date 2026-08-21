using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private Transform startShoot;
    [SerializeField] private Transform[] pointShoot;
    [SerializeField] private TurretData turretData;
    private Coroutine _fireRoutine;
    
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
                yield return new WaitForSeconds(turretData.fireRate);
            }
            else if (turretData.fireMode == FireMode.Burst)
            {
                for (int i = 0; i < turretData.burstCount; i++)
                {
                    Shoot();

                    if (i < turretData.burstCount - 1)
                    {
                        yield return new WaitForSeconds(turretData.burstDelay);
                    }
                }
                yield return new WaitForSeconds(turretData.burstCooldown);
            }
        }
    }
    
    private void Shoot()
    {
        for (int i = 0; i < pointShoot.Length; i++)
        {
            GameObject bullet = ObjectPoolManager.Instance.Spawn(turretData.bulletType, pointShoot[i].position, pointShoot[i].rotation);
            bullet.GetComponent<Bullet>().Init(turretData);
        }
    }
}
