using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour{

    public float scrollSpeed;
    public Renderer backgroundRenderer;
    GameController gameController;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate() {
        backgroundRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * gameController.gameSpeed / 50, 0f);
    }
}