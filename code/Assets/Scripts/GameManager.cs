using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lastScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lastScoreText;
    public GameObject deathMenu;
    public GameObject pauseMenu;
    public static bool GameIsPaused = false;
    void Start()
    {
        score = 0;
        lastScore = 0;
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        scoreText.text = "Score: " + score;
        lastScoreText.text = "High score: " + lastScore;
        
    }
    
    // Score and death checker
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            DeathMenu();
        }

        if(other.gameObject.tag == "Enemy")
        {
            score+= 10;
            Debug.Log("Score = " + score);
        }
        
    }

    // Button functions


    public void DeathMenu()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene("SumoArena1");
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }
}
