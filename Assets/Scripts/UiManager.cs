using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject endDemoPanel;
    [SerializeField] private TextMeshProUGUI cownDownText;
    [SerializeField] private TextMeshProUGUI scoreTextUi;
    [SerializeField] private TextMeshProUGUI scoreTextPause;
    [SerializeField] private TextMeshProUGUI scoreTextOver;
    [SerializeField] private TextMeshProUGUI bestScoreTextPause;
    [SerializeField] private TextMeshProUGUI bestScoreTextOver;
    [SerializeField] private TextMeshProUGUI scoreTextEnd;
    [SerializeField] private TextMeshProUGUI bestScoreTextEnd;
    [SerializeField] private Image hpFillImage;
 
    [SerializeField] private TankHealth tankHealth;
    private int _scoreNow = 0;
    private int _bestScore = 0;
 
    private void Start()
    {
        GameManager.Instance.OnGameStart += HandleGameStart;
        GameManager.Instance.OnPause += HandlePause;
        GameManager.Instance.OnResume += HandleResume;
        GameManager.Instance.OnGameOver += HandleGameOver;
        GameManager.Instance.OnScoreChange += HandleScoreChanged;
        GameManager.Instance.OnBestScoreChanged += HandleBestScoreChanged;
        GameManager.Instance.OnCountdownStartChanged += HandleCountDown;
        GameManager.Instance.OnDemoEnd += HandleEndDemo;
        tankHealth.OnChangeHp += HandleHpChanged;
        ShowOnly(waitPanel);
    }
    
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= HandleGameStart;
            GameManager.Instance.OnPause -= HandlePause;
            GameManager.Instance.OnResume -= HandleResume;
            GameManager.Instance.OnGameOver -= HandleGameOver;
            GameManager.Instance.OnScoreChange -= HandleScoreChanged;
            GameManager.Instance.OnBestScoreChanged -= HandleBestScoreChanged;
            GameManager.Instance.OnCountdownStartChanged -= HandleCountDown;
            GameManager.Instance.OnDemoEnd -= HandleEndDemo;
        }
 
        if (tankHealth != null)
        {
            tankHealth.OnChangeHp -= HandleHpChanged;
        }
    }
 
    private void HandleGameStart()
    {
        ShowOnly(uiPanel);
    }
 
    private void HandlePause()
    {
        ShowOnly(pausePanel);
    }
 
    private void HandleResume()
    {
        ShowOnly(uiPanel);
    }
 
    private void HandleGameOver()
    {
        ShowOnly(gameOverPanel);
    }

    private void HandleEndDemo()
    {
        ShowOnly(endDemoPanel);
    }
 
    private void HandleScoreChanged(float score)
    {
        _scoreNow = Mathf.RoundToInt(score);
        scoreTextUi.text = _scoreNow.ToString();
        scoreTextOver.text = _scoreNow.ToString();
        scoreTextPause.text = _scoreNow.ToString();
        scoreTextEnd.text = _scoreNow.ToString();
    }

    private void HandleBestScoreChanged(float bestScore)
    {
        _bestScore = Mathf.RoundToInt(bestScore);
        bestScoreTextOver.text = _bestScore.ToString();
        bestScoreTextPause.text = _bestScore.ToString();
        bestScoreTextEnd.text = _bestScore.ToString();
    }
 
    private void HandleHpChanged(int current, int max)
    {
        if (hpFillImage != null)
        {
            hpFillImage.fillAmount = (float)current / max;
        }
    }

    private void HandleCountDown(int time)
    {
        cownDownText.gameObject.SetActive(true);
        cownDownText.text = time.ToString();
        if (time == 0)
        {
            cownDownText.gameObject.SetActive(false);
        }
    }
    
    private void ShowOnly(GameObject panel)
    {
        waitPanel.SetActive(panel == waitPanel);
        uiPanel.SetActive(panel == uiPanel);
        pausePanel.SetActive(panel == pausePanel);
        gameOverPanel.SetActive(panel == gameOverPanel);
        endDemoPanel.SetActive(panel == endDemoPanel);
    }
}