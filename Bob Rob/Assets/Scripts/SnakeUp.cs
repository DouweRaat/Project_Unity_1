using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeUp : MonoBehaviour {
    AudioSource Hiss;
    private bool hiss;
    public SpriteRenderer spriteRenderer;
    public Sprite snakeUp;

    void Start () {

        hiss = false;
        Hiss = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (transform.position.x < 8 && hiss == false)
        {
            Hiss.Play();
            hiss = true;
        }
        if (transform.position.x < -1.3)
        {
            spriteRenderer.sprite = snakeUp;
        }
    }
}
