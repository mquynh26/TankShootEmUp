using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string BestScoreKey = "BestScore";
    [SerializeField] private int scoreTime = 10;
    public static GameManager Instance { get; private set; }
 
    public enum GameState
    {
        Idle,
        Playing,
        Paused,
        GameOver,
        EndDemo
    }
 
    public GameState CurrentState { get; private set; } = GameState.Idle;
    
    public event Action OnGameStart;
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnGameOver;
    public event Action<float> OnScoreChange;
    public event Action<float> OnBestScoreChanged;
    public event Action<int> OnCountdownStartChanged; 
    public event Action OnDemoEnd;
    public float Score { get; private set; }
    public float BestScore { get; private set; }
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        BestScore = PlayerPrefs.GetFloat(BestScoreKey, 0f);
    }
 
    private void Update()
    {
        if (CurrentState == GameState.Playing)
        {
            AddScore(scoreTime * Time.deltaTime);
        }
    }
 
    public void StartGame()
    {
        if (CurrentState != GameState.Idle)
        {
            return;
        }
        
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        Score = 0;
        OnScoreChange?.Invoke(Score);
        OnGameStart?.Invoke();
    }
    
    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }
        OnBestScoreChanged?.Invoke(BestScore);
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
        {
            return;
        }
        StartCoroutine(CountdownStart(3));
    }

    public void GameOver()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }
        CurrentState = GameState.GameOver;
        Time.timeScale = 0f;
        SoundManager.Instance.PlayGameOver();
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetFloat(BestScoreKey, BestScore);
            PlayerPrefs.Save();
        }
        OnBestScoreChanged?.Invoke(BestScore);
        OnGameOver?.Invoke();
    }

    public void AddScore(float score)
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }
        Score += score;
        OnScoreChange?.Invoke(Score);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    
    public IEnumerator CountdownStart(int countdownTime)
    {
        OnResume?.Invoke();
        Time.timeScale = 0f;
        do
        {
            OnCountdownStartChanged?.Invoke(countdownTime);
            countdownTime -= 1;
            SoundManager.Instance.PlayCountDown();
            yield return new WaitForSecondsRealtime(1f);
        } while (countdownTime >= 0);
        
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }
    
    public void EndDemo()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }
        CurrentState = GameState.EndDemo;
        Time.timeScale = 0f;
        SoundManager.Instance.PlayGameOver();
        OnDemoEnd?.Invoke();
    }
}
