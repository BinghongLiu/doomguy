using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    public AudioSource ammo, medkit, enemyDeath, enemyShot, gunshot, playerHurt;

    private void Awake(){
        instance = this;
    }

    public void PlayAmmo() {
        ammo.Stop();
        ammo.Play();
    }

    public void PlayMedkit() {
        medkit.Stop();
        medkit.Play();
    }

    public void PlayEnemyDeath() {
        enemyDeath.Stop();
        enemyDeath.Play();
    }

    public void PlayEnemyShot() {
        enemyShot.Stop();
        enemyShot.Play();
    }

    public void PlayGunshot() {
        gunshot.Stop();
        gunshot.Play();
    }

    public void PlayPlayerHurt() {
        playerHurt.Stop();
        playerHurt.Play();
    }
}
