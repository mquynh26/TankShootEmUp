using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBuffs : MonoBehaviour
{
    [SerializeField] private TankControl tankControl;
    [SerializeField] private TankHealth tankHealth;
    [SerializeField] private TankShield tankShield;
    [SerializeField] private TurretChange turretChange;
 
    public void ApplyBuff(Buff buff)
    {
        switch (buff.buffType)
        {
            case BuffType.Heal:
                tankHealth.Heal((int)buff.value);
                break;
 
            case BuffType.Speed:
                tankControl.ApplySpeedBuff(buff.value, buff.duration);
                break;
 
            case BuffType.FireRate:
                turretChange.GetActiveWeaponShoot().ApplyFireRateBuff(buff.value, buff.duration);
                break;
 
            case BuffType.Shield:
                tankShield.ActivateShield(buff.duration);
                break;
 
            case BuffType.TurretSwap:
                turretChange.SwitchTo(buff.turretIndex);
                break;
        }
    }
}
