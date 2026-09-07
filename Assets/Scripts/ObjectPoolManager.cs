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
    private Dictionary<PoolType, List<Queue<GameObject>>> _poolDictionary;
    private Dictionary<GameObject, Queue<GameObject>> _objectToQueue;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
 
        _poolDictionary = new Dictionary<PoolType, List<Queue<GameObject>>>();
        _objectToQueue = new Dictionary<GameObject, Queue<GameObject>>();
 
        foreach (PoolData pool in pools)
        {
            List<Queue<GameObject>> variantQueues = new List<Queue<GameObject>>();
 
            for (int variantIndex = 0; variantIndex < pool.prefab.Length; variantIndex++)
            {
                Queue<GameObject> queue = new Queue<GameObject>();
 
                for (int i = 0; i < pool.poolSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab[variantIndex], transform);
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                    _objectToQueue.Add(obj, queue);
                }
 
                variantQueues.Add(queue);
            }
 
            _poolDictionary.Add(pool.poolType, variantQueues);
        }
    }
    
    public GameObject Spawn(PoolType type, Vector2 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(type, out List<Queue<GameObject>> variantQueues) || variantQueues.Count == 0)
        {
            return null;
        }
 
        int randomIndex = UnityEngine.Random.Range(0, variantQueues.Count);
        return SpawnFromQueue(variantQueues[randomIndex], position, rotation);
    }
    
    public GameObject Spawn(PoolType type, int index, Vector2 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(type, out List<Queue<GameObject>> variantQueues))
        {
            return null;
        }
 
        if (index < 0 || index >= variantQueues.Count)
        {
            return null;
        }
 
        return SpawnFromQueue(variantQueues[index], position, rotation);
    }
 
    private GameObject SpawnFromQueue(Queue<GameObject> queue, Vector2 position, Quaternion rotation)
    {
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
        if (!obj.activeSelf)
        {
            return;
        }
 
        if (!_objectToQueue.TryGetValue(obj, out Queue<GameObject> queue))
        {
            return;
        }
 
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}