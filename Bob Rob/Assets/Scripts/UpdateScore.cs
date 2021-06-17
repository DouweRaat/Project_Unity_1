using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour {

    public void UpdateScoreFunction(int score)
    {
        GetComponent<TextMesh>().text = score.ToString();
    }
}
