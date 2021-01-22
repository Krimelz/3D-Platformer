using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text healthText;
    public Text manaText;
    public GameObject enemyTask;
    public GameObject keyTask;
    public GameObject gameOverPanel;
    public GameObject gamePausePanel;
    public GameObject levelPassedPanel;

    private bool isPaused = false;

    private void Awake()
    {
        LevelManager.levelPassed += EnableLevelPassedPanel;
        LevelManager.enableTasks += EnableTasks;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        PlayerController.death += EnableGameOverPanel;
        PlayerController.healthUpdate += UpdateHealth;
        PlayerController.manaUpdate += UpdateMana;
    }

    private void EnableTasks(bool enemy, bool key)
    {
        enemyTask.SetActive(enemy);
        keyTask.SetActive(key);
    }

    private void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void EnableLevelPassedPanel()
    {
        levelPassedPanel.SetActive(true);
    }

    private void EnableGamePausePanel()
    {
        if (!gameOverPanel.activeSelf || !levelPassedPanel.activeSelf)
        {
            gamePausePanel.SetActive(true);
        }
    }

    private void DisableGamePausePanel()
    {
        gamePausePanel.SetActive(false);
    }

    private void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

    private void UpdateMana(int mana)
    {
        manaText.text = mana.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(gameOverPanel.activeSelf || levelPassedPanel.activeSelf))
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            EnableGamePausePanel();
        }
        else
        {
            Time.timeScale = 1f;
            DisableGamePausePanel();
        }
    }

    public void Resume()
    {
        Pause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        PlayerController.death -= EnableGameOverPanel;
        PlayerController.healthUpdate -= UpdateHealth;
        PlayerController.manaUpdate -= UpdateMana;

        LevelManager.levelPassed -= EnableLevelPassedPanel;
        LevelManager.enableTasks -= EnableTasks;
    }
}
