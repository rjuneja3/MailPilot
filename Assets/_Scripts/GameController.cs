using System.Collections;
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
    public GameObject lives;



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
            if (_lives < 1)
            {
                SceneManager.LoadScene("End");
            }

            else
            {
                lives.GetComponent<ScoreBoard>().lives = _lives;
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
            score.GetComponent<ScoreBoard>().score = _score;
            //Checks if the player reaches 500 scores and if the scene is main it moves the player to level2
            if (_score >= 500 && SceneManager.GetActiveScene().name == "Main") 
            {

                SceneManager.LoadScene("Level2");
                _score = 500;
            }
            //Checks if the player reaches a high score, it saves the score to  highScore Variable in scoreboard
            else if (highScore.GetComponent<ScoreBoard>().highScore < _score)
            {
                highScore.GetComponent<ScoreBoard>().highScore = _score;
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
        highScore = GameObject.Find("ScoreBoard");
        lives = GameObject.Find("ScoreBoard");
        score = GameObject.Find("ScoreBoard");
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
                Lives = 5;
                Score = 0;
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.ENGINE;
                break;
            case "Level2":
                Lives = lives.GetComponent<ScoreBoard>().lives;
                Score = score.GetComponent<ScoreBoard>().score;

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
                highScoreLabel.text = "High Score: " + highScore.GetComponent<ScoreBoard>().highScore;
                break;
        }

        


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
        DontDestroyOnLoad(lives);
        DontDestroyOnLoad(score);
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
