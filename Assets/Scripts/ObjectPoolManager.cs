using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [SerializeField]
    private List<PoolData> pools = new();

    private Dictionary<PoolType, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        _poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

        foreach (PoolData pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab[UnityEngine.Random.Range(0, pool.prefab.Length)], transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            _poolDictionary.Add(pool.poolType, queue);
        }
    }

    public GameObject Spawn(PoolType type, Vector2 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(type, out Queue<GameObject> queue))
        {
            return null;
        }

        if (queue.Count == 0)
        {
            return null;
        }

        GameObject obj = queue.Dequeue();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void ReturnPool(PoolType type, GameObject obj)
    {
        if (!_poolDictionary.TryGetValue(type, out Queue<GameObject> queue))
        {
            return;
        }
        if (!obj.activeSelf)
        {
            return;
        }

        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}