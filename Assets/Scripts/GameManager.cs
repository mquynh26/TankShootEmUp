using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
 
    public enum GameState
    {
        Idle,
        Playing
    }
 
    public GameState CurrentState { get; private set; } = GameState.Idle;
    
    public event Action OnGameStart;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
 
    private void Update()
    {
        if (CurrentState != GameState.Idle)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }
 
    private void StartGame()
    {
        CurrentState = GameState.Playing;
        OnGameStart?.Invoke();
    }
}
