using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour {

    public void displayHighscore() {
        GetComponent<TextMesh>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
