using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    
    [Header("Mouse")]
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;

    [Header("view")]
    public Camera viewCam;

    [Header("Shooting")]
    public GameObject bulletImpact;
    public int currentAmmo;
    public int kills = 0;

    [Header("Animator")]
    public Animator gunAnim;
    public Animator anim;

    [Header("Health")]
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject deathScreen;
    public bool dead;

    [Header("UI")]
    public Text healthUI, ammoUI;
    public bool isPaused;


    // Start is called before the first frame update
    void start() {
        currentHealth = 100;
        isPaused = false;
        hpUpdate();
        ammoUpdate();
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (isPaused == false && dead == false) {
            movementUpdate();

            mouseUpdate();

            shootingUpdate();

        }
        
    }

    public void pauseUnpause () {
        if (isPaused) {
            isPaused = false;
        } else {
            isPaused = true;
        }
    }

    void movementUpdate() {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;

        rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        if (moveInput != Vector2.zero) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }
    }

    void mouseUpdate() {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * mouseSensitivity, Input.GetAxisRaw("Mouse Y") * mouseSensitivity);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
    
        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
    }

    void shootingUpdate() {
        if(Input.GetMouseButtonDown(0)) {
            if (currentAmmo > 0) {
                AudioController.instance.PlayGunshot();

                Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) {
                    Debug.Log("I'm looking at " + hit.transform.name);
                    Instantiate(bulletImpact, hit.point, transform.rotation);

                    if(hit.transform.tag == "Enemy") {
                        hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                    }
                } else {
                    //Debug.Log("I'm looking at nothing");
                }
                currentAmmo -= 1;
                gunAnim.SetTrigger("Shoot");
                ammoUpdate();

            }
        }
    }


    public void TakeDamage(int damage) {
        currentHealth -= damage;
        AudioController.instance.PlayPlayerHurt();
        if (currentHealth <= 0) {
            dead = true;
            currentHealth = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        hpUpdate();
    }

    public void healHealth(int heal) {
        currentHealth += heal;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        hpUpdate();
    }

    public void hpUpdate() {
        healthUI.text = currentHealth.ToString() + "%";
    }
    public void ammoUpdate() {
        ammoUI.text = currentAmmo.ToString();
    }

    public void killCount() {
        kills++;
        if (kills >= 5) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
