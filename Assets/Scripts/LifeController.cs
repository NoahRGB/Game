using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour {

    private TMP_Text healthBarText;
    private HealthBar healthBar;

    public float maxHealth = 20.0f;
    public float health = 20.0f;

    void Start() {
        if (gameObject.name == "Player") {
            healthBarText = GameObject.Find("HealthBarTextUI").GetComponent<TMP_Text>();
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (healthBar != null) {
                healthBar.SetMaxHealth((int)maxHealth);
            }
        }
    }

    public void takeDamage(float damage) {
        health -= damage;

        if (gameObject.name == "Player") {
            healthBarText.text = ((health / maxHealth) * 100) + "%";
            healthBar.SetHealth((int)health);
        }

        if (health <= 0) {
            die();
        }
    }

    private void die() {
        if (gameObject.name != "Player") {
            Destroy(gameObject);
        } else {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOver");
        }
    }
}
