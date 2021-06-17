using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Animator animator;
    private bool dead;
    private bool alreadydead;

    private GameObject bullet;
    public GameObject bulletPrefab;
    private float bulletSpwanpositionX;
    private float bulletSpwanpositionY;
    private bool bulletFired;
    GameObject enemies;
    public AudioSource shootingSound;

    private void Start()
    {
        dead = false;
        alreadydead = false;
        bulletFired = false;
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }

    private void Update()
    {
        FireBullet();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Agent_idle") && dead == true)
        {
            alreadydead = true;
            animator.SetBool("alreadydead", alreadydead);
        }
    }

    private void FireBullet()
    {
        if (dead == false && bulletFired == false && transform.position.x < 1.5f)
        {
            bullet = Instantiate(bulletPrefab);
            bulletSpwanpositionX = transform.position.x - 3.5f;
            bulletSpwanpositionY = transform.position.y + 0.702f;
            bullet.transform.position = new Vector3(bulletSpwanpositionX, bulletSpwanpositionY, bullet.transform.position.z);
            bullet.transform.parent = enemies.transform;
            shootingSound.Play();
            bulletFired = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name);
            boxCollider.enabled = false;
            Destroy(col);
            dead = true;
            animator.SetBool("dead", dead);
        }
    }
}
