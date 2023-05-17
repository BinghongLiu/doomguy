using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int heal = 25;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {

            AudioController.instance.PlayMedkit();
            PlayerController.instance.healHealth(heal);

            Destroy(gameObject);
        }
    }
}
