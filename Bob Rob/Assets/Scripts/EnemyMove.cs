using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    public float speed;
    GameController gameController;

    void Start () {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	void FixedUpdate () {
        rigidbody2d.velocity = Vector2.left * speed * gameController.gameSpeed;
        deleteEnemy();
	}

    void deleteEnemy() {
        if (transform.position.x < -8)
        {
            Destroy(gameObject, 2);
        }
        if (gameController.gameState == GameController.GameState.DEAD)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name);
            gameController.GameOver();
        }
    }
}
