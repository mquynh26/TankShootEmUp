using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TurretData", menuName = "TurretData")]
public class TurretData : ScriptableObject
{
    public FireMode fireMode;
    public PoolType turretType;
    public PoolType bulletType;
    public float bulletSpeed;
    public int bulletDamage;
    public float fireRate;
    
    public int burstCount;
    public float burstDelay;
    public float burstCooldown;
}
