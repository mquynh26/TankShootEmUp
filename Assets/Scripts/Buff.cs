using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public BuffType buffType;
    public float value;
    public float duration;
    public int turretIndex;
 
    private void Update()
    {
        transform.Translate(Vector2.down * (PlatMap.Speed * Time.deltaTime), Space.World);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
 
        TankBuffs tankBuffs = collision.GetComponent<TankBuffs>();
        if (tankBuffs != null)
        {
            SoundManager.Instance.PlayBuffPickup();
            tankBuffs.ApplyBuff(this);
        }
 
        ObjectPoolManager.Instance.ReturnPool(PoolType.Buff, gameObject);
    }
}
