using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    public void start(){
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    
    // Update is called once per frame
    public void resumeGame() {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        PlayerController.instance.pauseUnpause();
    }

    public void mainMenu() {
        Time.timeScale = 1f;
        PlayerController.instance.pauseUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
