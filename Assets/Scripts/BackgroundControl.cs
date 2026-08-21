using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public GameObject pointStart;
    public GameObject pointSpawn;
    public GameObject pointEnd;
    private GameObject _bg;
    private GameObject _bg1;
    void Start()
    {
        _bg = ObjectPoolManager.Instance.Spawn(PoolType.Bg, pointStart.transform.position, Quaternion.identity);
        _bg1 = ObjectPoolManager.Instance.Spawn(PoolType.Bg, pointSpawn.transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        CheckBg(ref _bg);
        CheckBg(ref _bg1);
        
    }
    private void CheckBg(ref GameObject bg)
    {
        if (bg.transform.position.y < pointEnd.transform.position.y)
        {
            ObjectPoolManager.Instance.ReturnPool(PoolType.Bg, bg);
            bg = ObjectPoolManager.Instance.Spawn(PoolType.Bg, pointSpawn.transform.position, Quaternion.identity);
        }
    }
}
