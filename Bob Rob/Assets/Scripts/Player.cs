using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    public float jumpVelocity;
    private BoxCollider2D boxCollider2d;
    public Animator animator;
    private bool jumping;
    private GameObject bullet;
    public GameObject bulletPrefab;
    private float bulletSpwanposition;
    private int bulletsInChamber;
    private float reloadStartTime;
    private float reloadElapsedTime;
    public float reloadTime;
    public AudioSource reloadSound;
    public AudioSource shootingSound;
    Chamber chamber;
    GameObject enemies;
    private enum PlayerState
    {
        RUNNING,
        JUMPING
    }

    private PlayerState playerState;

    private void Start() {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        chamber = GameObject.FindGameObjectWithTag("Chamber").GetComponent<Chamber>();
        reloadBullets();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        playerState = PlayerState.RUNNING;
    }

    private void Update() {
        fireCheck();
        switch (playerState)
        {
            case PlayerState.RUNNING:
                animator.SetBool("Jumping", false);
                checkIfGrounded();
                Jump();
                break;
            case PlayerState.JUMPING:
                animator.SetBool("Jumping", true);
                checkIfGrounded();
                break;
        }
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            playerState = PlayerState.JUMPING;
        }
    }

    private void checkIfGrounded()
    {
        if (IsGrounded())
        {
            playerState = PlayerState.RUNNING;
        }
        else
        {
            playerState = PlayerState.JUMPING;
        }
    }

    public void reloadBullets()
    {
        bulletsInChamber = 6;
    }

    private void fireBullet() {
        bullet = Instantiate(bulletPrefab);
        bulletSpwanposition = transform.position.y + 0.6f;
        bullet.transform.position = new Vector3(bullet.transform.position.x, bulletSpwanposition, bullet.transform.position.z);
        bullet.transform.parent = enemies.transform;
    }

    private void fireCheck() {
        if (Input.GetKeyDown(KeyCode.F) && bulletsInChamber >= 1)
        {
            fireBullet();
            shootingSound.Play();
            bulletsInChamber -= 1;
            if (bulletsInChamber <= 0)
            {
                reloadStartTime = Time.time;
                reloadSound.Play();
            }
        }
        else if (bulletsInChamber <= 0)
        {
            reloadElapsedTime = Time.time - reloadStartTime;
            if (reloadElapsedTime >= reloadTime)
            {
                reloadBullets();
            }
        }
        chamber.changeChamber(bulletsInChamber);
    }

    private bool IsGrounded() {
        RaycastHit2D groundedRaycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        Debug.Log(groundedRaycastHit2d.collider);
        return groundedRaycastHit2d.collider != null;
    }

    public void ResetPosition()
    {
        Debug.Log("Player: ResetPosition");
        transform.position = new Vector3(-6.5f, -2, -2);
    }
}