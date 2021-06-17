using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour {
    public CapsuleCollider2D capsuleCollider;
    public Animator animator;

    private Rigidbody2D rigidbody2d;
    public float speed;
    GameController gameController;

    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        if (capsuleCollider.enabled == false)
        {
            animator.SetBool("dead", true);
        }
        rigidbody2d.velocity = Vector2.left * speed * gameController.gameSpeed;
        deleteEnemy();
    }

    void deleteEnemy()
    {
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
        if (col.tag == "Bullet")
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name);
            capsuleCollider.enabled = false;
            Destroy(col);
            animator.SetBool("dead", true);
            speed = 10;
        }
        if (col.tag == "Player")
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name);
            gameController.GameOver();
        }
    }
}
