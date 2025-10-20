using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject LegendMenu;
    public GameObject EndOfGameMenu;
    public Transform Camera;
    public Slider Health;
    public Slider HealthNPC;
    public Transform SpawnTheBalls;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI EndOfGameScore;
    public TextMeshProUGUI DistanceTraveled;
    public BallSpawner Spawner;

    public UnityEvent OnBallsSpawned;

    bool gameStarted = false;

    void Start()
    {
        Health.value = 1;
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        LegendMenu.SetActive(false);
        EndOfGameMenu.SetActive(false);
        SpawnTheBalls.gameObject.SetActive(false);
        Camera.transform.position = new Vector3(69, 2, 72);
    }
    
    public void StartGame()
    {
        MainMenu.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        Camera.transform.DOMoveY(-0.75f, 1.33f);

        StartCoroutine(DisplayTheSpawnBallsButton());
    }

    IEnumerator DisplayTheSpawnBallsButton()
    {
        yield return new WaitForSeconds(1.1f);

        SpawnTheBalls.gameObject.SetActive(true);

        while(true)
        {
            SpawnTheBalls.DOScale(1.05f, 0.5f);

            yield return new WaitForSeconds(0.5f);

            SpawnTheBalls.DOScale(1f, 0.5f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SpawnTheBallsClick()
    {
        SpawnTheBalls.gameObject.SetActive(false);
        gameStarted = true;

        Spawner.SpawnTheBalls();

        OnBallsSpawned?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void DisplayLegend()
    {
        LegendMenu.SetActive(true);
    }

    public void HideLegend()
    {
        LegendMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void DisplayScore(int score)
    {
        Score.text = score.ToString().PadLeft(6, '0');
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.F1) && gameStarted)
        {
            DisplayLegend();
            Time.timeScale = 0;
        }
    }

    internal void SetMaxHealth(float health)
    {
        Health.maxValue = health;
        HealthNPC.maxValue = health;

        Health.value = health;
        HealthNPC.value = health;
    }

    internal void DisplayDead(int score)
    {
        EndOfGameMenu.gameObject.SetActive(true);
        EndOfGameScore.text = score.ToString().PadLeft(6, '0');
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    internal void DisplayDistance(float distanceTraveled)
    {
        DistanceTraveled.text = distanceTraveled.ToString("N2") + " m";
    }

    internal void DisplayHealth(float health)
    {
        Health.value = health;
        HealthNPC.value = health;
    }
}
