using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour {

    public bool isInvincible = false;
    private TMP_Text healthBarText;
    private HealthBar healthBar;

    public float maxHealth = 20.0f;
    public float health = 20.0f;

    void Start() {
        // if the parent is the player, setup the health bar UI componenet
        if (gameObject.name == "Player") {
            healthBarText = GameObject.Find("HealthBarTextUI").GetComponent<TMP_Text>();
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (healthBar != null) {
                healthBar.SetMaxHealth((int)maxHealth);
            }
        }
    }

    public void SetHealth(float newHealth) {
        // sets the health based on the max health
        if (newHealth <= maxHealth) {
            health = newHealth;
        } else {
            health = maxHealth;
        }
        healthBarText.text = ((health / maxHealth) * 100) + "%";
        healthBar.SetHealth((int)health);
    }

    public void TakeDamage(float damage) {
        if (!isInvincible) {
            health -= damage;

            // if the parent is the player, update the health bar UI
            if (gameObject.name == "Player") {
                gameObject.transform.GetComponent<Player>().Hit();
                healthBarText.text = ((health / maxHealth) * 100) + "%";
                healthBar.SetHealth((int)health);
            }

            if (health <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        // calls the relevant functions for the object that has died

        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (gameObject.tag == "Player") {
            // player death
            player.Die();

        } else if (gameObject.tag == "Enemy") {
            // enemy death
            MeleeEnemy enemy = gameObject.GetComponent<MeleeEnemy>();
            enemy.Die();

            player.AddCash(enemy.prize);
            
        
        } else {
            // any other death
            Destroy(gameObject);
        }
    }
}
