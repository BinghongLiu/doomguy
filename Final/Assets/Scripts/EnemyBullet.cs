using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public float bulletSpeed = 5f;
    
    public Rigidbody2D rb;

    private Vector3 direction;

    void Start() {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    void Update() {
        rb.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerController.instance.TakeDamage(damage);
            Destroy(gameObject);
        } else if (collision.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
