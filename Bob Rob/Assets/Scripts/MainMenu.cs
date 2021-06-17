using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject gameController;
    GameController gameControllerScript;
    public GameObject LevelGameoverAndPause;

    void Start () {
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    public void disableGame() {
        LevelGameoverAndPause.SetActive(false);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelGameoverAndPause.SetActive(true);
            gameControllerScript.StartLevel();
        }
	}
}
