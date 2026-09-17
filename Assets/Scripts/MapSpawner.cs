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
    [SerializeField] private int platCount;

    private class MapPiece
    {
        public GameObject Object;
        public PoolType PoolType;
    }

    private MapPiece _pieceA;
    private MapPiece _pieceB;

    private int _platIndex;
    private int _lastBgIndex = -1;

    private bool _switchToPlat;

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

        // Trả đúng PoolType của chính piece
        ObjectPoolManager.Instance.ReturnPool(piece.PoolType, piece.Object);

        // Spawn tiếp
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
        if (_platIndex >= platCount)
        {
            GameManager.Instance.EndDemo();
            return null;
        }

        GameObject plat = ObjectPoolManager.Instance.Spawn(PoolType.Plv, _platIndex, position, Quaternion.identity);

        _platIndex++;

        if (plat == null)
        {
            return null;
        }

        return new MapPiece
        {
            Object = plat,
            PoolType = PoolType.Plv
        };
    }
}