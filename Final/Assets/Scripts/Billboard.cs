using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer sr;

    void start() {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
    }

    void Update() {
        transform.LookAt(PlayerController.instance.transform.position, -Vector3.forward);
    }
}
