using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour {
    public BoxCollider2D boxCollider;
    public Animator animator;
    public AudioSource explosionSound;
    private bool alreadyexploded;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TNT_default") && boxCollider.enabled == false)
        {
            alreadyexploded = true;
            animator.SetBool("alreadyexploded", alreadyexploded);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name);
            boxCollider.enabled = false;
            Destroy(col);
            animator.SetBool("explosion", true);
            explosionSound.Play();
        }
    }
}
