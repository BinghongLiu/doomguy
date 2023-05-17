using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathscreen : MonoBehaviour
{
    public void restartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void mainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
