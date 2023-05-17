using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPack : MonoBehaviour
{
    public int ammoPack = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            AudioController.instance.PlayAmmo();
            PlayerController.instance.currentAmmo += ammoPack;
            PlayerController.instance.ammoUpdate();
            Destroy(gameObject);
        }
    }
}
