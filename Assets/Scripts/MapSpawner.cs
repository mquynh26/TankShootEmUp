using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject pointStart;
    public GameObject pointSpawn;
    public GameObject pointEnd;
 
    [SerializeField] private int bgCount = 4;
    [SerializeField] private int platCount;
 
    private GameObject _pieceA;
    private GameObject _pieceB;
 
    private int _bgIndex;
    private int _platIndex;
    
    private bool _switchToPlat;
    private bool _inPlatMode;
 
    private void Start()
    {
        GameManager.Instance.OnGameStart += HandleGameStart;
 
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
 
    private void CheckPiece(ref GameObject piece)
    {
        if (piece == null)
        {
            return;
        }
 
        if (piece.transform.position.y >= pointEnd.transform.position.y)
        {
            return;
        }
 
        PoolType finishedType = _inPlatMode ? PoolType.Plv : PoolType.Bg;
        ObjectPoolManager.Instance.ReturnPool(finishedType, piece);
 
        if (!_switchToPlat)
        {
            piece = SpawnBg(pointSpawn.transform.position);
        }
        else
        {
            _inPlatMode = true;
            piece = SpawnNextPlat(pointSpawn.transform.position);
        }
    }
 
    private GameObject SpawnBg(Vector2 position)
    {
        GameObject bg = ObjectPoolManager.Instance.Spawn(PoolType.Bg, _bgIndex, position, Quaternion.identity);
        _bgIndex = (_bgIndex + 1) % bgCount;
        return bg;
    }
 
    private GameObject SpawnNextPlat(Vector2 position)
    {
        if (_platIndex >= platCount)
        {
            return null;
        }
 
        GameObject plat = ObjectPoolManager.Instance.Spawn(PoolType.Plv, _platIndex, position, Quaternion.identity);
        _platIndex++;
        return plat;
    }
}
