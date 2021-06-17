using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float startTime;
    private float elapsedTime;

    public float gameSpeed;
    public float gameSpeedMultiplier;

    public GameObject LevelGameoverAndPause;
    public GameObject mainMenu;
    MainMenu mainMenuScript;

    public GameObject box;
    public GameObject tumbleweed;
    public GameObject snake;
    public GameObject TNT;
    public GameObject agent;
    public GameObject barbedWire;
    public GameObject bull;
    private GameObject Enemy;

    public GameObject gameOverScreen;
    public GameObject level;
    public GameObject pause;
    public GameObject enemies;

    public GameObject retryText;

    public GameObject normalScoreText;
    public GameObject gameOverScoreText;
    private float score;
    public float scoreMultiplier;
    private int scoreRounded;
    public GameObject highscoreText;
    Highscore highscoreScript;

    AudioSource Music;
    Player player;

    public enum GameState
    {
        ALIVE,
        DEAD,
        PAUSE
    }

    public GameState gameState;

    void Start () {
        Music = GetComponent<AudioSource>();
        mainMenuScript = mainMenu.GetComponent<MainMenu>();
        highscoreScript = highscoreText.GetComponent<Highscore>();
        StartLevel();
	}

    public void StartLevel()
    {
        mainMenu.SetActive(false);
        gameState = GameState.ALIVE;
        level.SetActive(true);
        gameOverScreen.SetActive(false);
        enemies.SetActive(true);
        pause.SetActive(false);
        startTime = Time.time;

        ClearEnemiesChildren();

        gameSpeed = 1.1f;

        score = 0;
        scoreRounded = (int) score;
        normalScoreText.GetComponent<UpdateScore>().UpdateScoreFunction(scoreRounded);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ResetPosition();
        player.reloadBullets();

        Music.Play();
        Music.volume = 0.35f;

        SpawnEnemy();
    }

    void DeleteAndSpawnNextEnemy()
    {
        if (Enemy)
        {
            if (Enemy.transform.position.x < -8)
            {
                Destroy(Enemy, 2);
                SpawnEnemy();
                Debug.Log("DeleteAndSpawnNextEnemy werkt");
            }
        }
    }

    public void GameOver()
    {
        gameState = GameState.DEAD;
        level.SetActive(false);
        gameOverScreen.SetActive(true);
        enemies.SetActive(true);
        pause.SetActive(false);
        retryText.transform.position = new Vector3(0,11,-4);
        retryText.transform.rotation = Quaternion.Euler(0, 0, 14);
        gameOverScoreText.GetComponent<UpdateScore>().UpdateScoreFunction(scoreRounded);
        highscoreScript.displayHighscore();
        Music.Stop();
        Destroy(Enemy);
    }

    void RespawnCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartLevel();
        }
    }

    void UpdateScoreAndSpeed()
    {
        score = elapsedTime*scoreMultiplier;
        scoreRounded = (int)score;
        normalScoreText.GetComponent<UpdateScore>().UpdateScoreFunction(scoreRounded);
        gameSpeed += Time.deltaTime * gameSpeedMultiplier;
        if (scoreRounded > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", scoreRounded);
    }

    void SpawnEnemy()
    {
        float enemyAmountFloat = score / 100;
        int enemyAmount = (int)enemyAmountFloat;
        enemyAmount += 1;
        if (enemyAmount < 1)
        {
            enemyAmount = 1;
        }
        if (enemyAmount > 9)
        {
            enemyAmount = 9;
        }
        int welkeEnemy = Random.Range(0, enemyAmount);
        switch (welkeEnemy)
        {
            case 0:
                Enemy = Instantiate(box);
                Enemy.transform.parent = enemies.transform;
                break;
            case 1:
                Enemy = Instantiate(snake);
                Enemy.transform.parent = enemies.transform;
                break;
            case 2:
                Enemy = Instantiate(tumbleweed);
                Enemy.transform.parent = enemies.transform;
                break;
            case 3:
                Enemy = Instantiate(agent);
                Enemy.transform.parent = enemies.transform;
                break;
            case 4:
                Enemy = Instantiate(barbedWire);
                Enemy.transform.parent = enemies.transform;
                break;
            case 5:
                Enemy = Instantiate(agent);
                Enemy.transform.parent = enemies.transform;
                Enemy = Instantiate(box);
                Enemy.transform.parent = enemies.transform;
                break;
            case 6:
                Enemy = Instantiate(TNT);
                Enemy.transform.parent = enemies.transform;
                break;
            case 7:
                Enemy = Instantiate(agent);
                Enemy.transform.parent = enemies.transform;
                Enemy = Instantiate(barbedWire);
                Enemy.transform.parent = enemies.transform;
                break;
            case 8:
                Enemy = Instantiate(bull);
                Enemy.transform.parent = enemies.transform;
                break;
        }
    }

    void pauseCheck() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            level.SetActive(false);
            enemies.SetActive(false);
            pause.SetActive(true);
            gameState = GameState.PAUSE;
            Music.volume = 0.0875f;
        }
    }

    void unPauseCheck() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            level.SetActive(true);
            enemies.SetActive(true);
            pause.SetActive(false);
            gameState = GameState.ALIVE;
            Music.volume = 0.35f;
        }
    }

    void ReturnToMainMenu() {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mainMenu.SetActive(true);
            mainMenuScript.disableGame();
        }
    }

    void ClearEnemiesChildren()
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[enemies.transform.childCount];

        foreach (Transform child in enemies.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    void Update () {
        switch (gameState)
        {
            case GameState.ALIVE:
                elapsedTime = Time.time - startTime;
                DeleteAndSpawnNextEnemy();
                UpdateScoreAndSpeed();
                pauseCheck();
                break;
            case GameState.DEAD:
                RespawnCheck();
                ReturnToMainMenu();
                break;
            case GameState.PAUSE:
                unPauseCheck();
                ReturnToMainMenu();
                break;
        }
	}
}
