﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;
    private int _highScore;



    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    public GameObject highScore;
    public GameObject score;


    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            if(_lives < 1)
            {
                
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
           
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;


            if (Score == 500)
            {
                SceneManager.LoadScene("Level2");
            }
            else if (highScore.GetComponent<HighScore>().score < _score)
            {
                highScore.GetComponent<HighScore>().score = _score;
            }
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        highScore = GameObject.Find("HighScore");
        score = GameObject.Find("Score");
        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
    }


    private void SceneConfiguration()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                highScoreLabel.enabled = false;
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.NONE;
                break;
            case "Main":
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.ENGINE;
                break;
            case "Level2":
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.ENGINE;
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                activeSoundClip = SoundClip.NONE;
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;
        }

        Lives = 5;
        Score = 0;


        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Event Handlers
    public void OnStartButtonClick()
    {
        DontDestroyOnLoad(highScore);

        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
