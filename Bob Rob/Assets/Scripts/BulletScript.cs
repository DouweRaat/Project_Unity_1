using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    public float speed;
    GameController gameController;

    void Start () {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	void FixedUpdate () {
        rigidbody2d.velocity = Vector2.right * speed;
        if (transform.position.x > 12) {
            Destroy(gameObject);
        }
        if (gameController.gameState == GameController.GameState.DEAD)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
