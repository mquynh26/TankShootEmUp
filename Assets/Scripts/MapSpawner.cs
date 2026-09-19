using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawner : MonoBehaviour
{
    public GameObject pointStart;
    public GameObject pointSpawn;
    public GameObject pointEnd;
 
    [SerializeField] private int bgCount = 4;
    [Serializable]
    private class PlatGroup
    {
        public PoolType platType;
        public int variantCount;
    }
 
    [SerializeField] private PlatGroup[] platGroups;
 
    private class MapPiece
    {
        public GameObject Object;
        public PoolType PoolType;
    }
 
    private MapPiece _pieceA;
    private MapPiece _pieceB;
 
    private List<int>[] _shuffledVariantIndices;
    private int _groupIndex;
    private int _indexInGroup;
 
    private int _lastBgIndex = -1;
 
    private bool _switchToPlat;
 
    private void Start()
    {
        GameManager.Instance.OnGameStart += HandleGameStart;
 
        BuildShuffledGroups();
 
        _pieceA = SpawnBg(pointStart.transform.position);
        _pieceB = SpawnBg(pointSpawn.transform.position);
    }
 
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= HandleGameStart;
        }
    }
 
    private void HandleGameStart()
    {
        _switchToPlat = true;
    }
 
    private void FixedUpdate()
    {
        CheckPiece(ref _pieceA);
        CheckPiece(ref _pieceB);
    }
 
    private void CheckPiece(ref MapPiece piece)
    {
        if (piece == null || piece.Object == null)
        {
            return;
        }
 
        if (piece.Object.transform.position.y >= pointEnd.transform.position.y)
        {
            return;
        }
        
        ObjectPoolManager.Instance.ReturnPool(piece.PoolType, piece.Object);
        
        if (!_switchToPlat)
        {
            piece = SpawnBg(pointSpawn.transform.position);
        }
        else
        {
            piece = SpawnNextPlat(pointSpawn.transform.position);
        }
    }
 
    private MapPiece SpawnBg(Vector2 position)
    {
        if (bgCount <= 0)
        {
            return null;
        }
 
        int randomIndex;
 
        do
        {
            randomIndex = Random.Range(0, bgCount);
        }
        while (randomIndex == _lastBgIndex && bgCount > 1);
 
        _lastBgIndex = randomIndex;
 
        GameObject bg = ObjectPoolManager.Instance.Spawn(PoolType.Bg, randomIndex, position, Quaternion.identity);
 
        if (bg == null)
        {
            return null;
        }
 
        return new MapPiece
        {
            Object = bg,
            PoolType = PoolType.Bg
        };
    }
 
    private MapPiece SpawnNextPlat(Vector2 position)
    {
        while (_groupIndex < platGroups.Length && _indexInGroup >= _shuffledVariantIndices[_groupIndex].Count)
        {
            _groupIndex++;
            _indexInGroup = 0;
        }
 
        if (_groupIndex >= platGroups.Length)
        {
            GameManager.Instance.EndDemo();
            return null;
        }
 
        PoolType platType = platGroups[_groupIndex].platType;
        int variantIndex = _shuffledVariantIndices[_groupIndex][_indexInGroup];
        _indexInGroup++;
 
        GameObject plat = ObjectPoolManager.Instance.Spawn(platType, variantIndex, position, Quaternion.identity);
 
        if (plat == null)
        {
            return null;
        }
 
        return new MapPiece
        {
            Object = plat,
            PoolType = platType
        };
    }
 
    private void BuildShuffledGroups()
    {
        _shuffledVariantIndices = new List<int>[platGroups.Length];
 
        for (int i = 0; i < platGroups.Length; i++)
        {
            List<int> indices = new List<int>();
            for (int v = 0; v < platGroups[i].variantCount; v++)
            {
                indices.Add(v);
            }
 
            Shuffle(indices);
            _shuffledVariantIndices[i] = indices;
        }
    }
 
    private void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}