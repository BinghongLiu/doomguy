using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public GameObject explosion;

    [Header("Movement")]
    public float playerRange = 10f;
    public Rigidbody2D rb;
    public float moveSpeed;

    [Header("Shoot")]
    public bool canShoot;
    public float fireRate = 0.5f;
    private float shotCounter;
    public GameObject bullet;
    public Transform source;

    [Header ("Animator")]
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement();
    }

    public void TakeDamage() {
        anim.SetTrigger("Hit");
        health--;
        if (anim.GetBool("isUnhurt")) {
            anim.SetBool("isUnhurt", false);
            anim.SetBool("isHurt", true);
        } else {
            if (anim.GetBool("isHurt")) {
                anim.SetBool("isHurt", false);
                anim.SetBool("isDying", true);
            }
        }
        if(health <= 0) {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioController.instance.PlayEnemyDeath();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerController.instance.killCount();            
        } else {
            AudioController.instance.PlayEnemyShot();
        }
    }

    void enemyMovement() {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange) {
            //anim.SetBool("Move", true);
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

            rb.velocity = playerDirection.normalized * moveSpeed;

            if (canShoot) {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0) {
                    anim.SetTrigger("Shoot");
                    Instantiate(bullet, source.position, source.rotation);
                    shotCounter = fireRate;
                }
            }
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerController.instance.TakeDamage(10);
        }
    }

    private void OnCollsionExit2D(Collider2D collisioin) {}
}
