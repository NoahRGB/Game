using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public AudioClip selectSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public bool inMenu = false;

    private AudioSource audioSource;
    private TMP_Text hudCashText;
    private TMP_Text shopCashText;
    private LifeController lifeController;
    private Inventory inventory;
    private float cash = 200.0f;

    void Start() {
        hudCashText = GameObject.Find("CashText").GetComponent<TMP_Text>();
        shopCashText = GameObject.Find("ShopCashText").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        lifeController = GetComponent<LifeController>();
        inventory = GetComponent<Inventory>();
        UpdateCashUI();
    }

    public void ResetGame() {
        lifeController.SetHealth(lifeController.maxHealth);
        inventory.ResetItems();
    }

    public void ResetPosition() {
        transform.localPosition = new Vector3(55.0f, 3.2f, 11.0f);
    }

    void Update() {

    }

    public void Hit() {
        audioSource.PlayOneShot(hitSound);
    }

    public void Die() {
        audioSource.PlayOneShot(deathSound);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }

    public void SwitchWeapon() {
        audioSource.PlayOneShot(selectSound);
    }

    public void AddCash(float newCash) {
        cash += newCash;
        UpdateCashUI();
    }

    public void RemoveCash(float cashToRemove) {
        cash -= cashToRemove;
        UpdateCashUI();
    }

    public float GetCash() { return cash; }

    void UpdateCashUI() {
        hudCashText.text = shopCashText.text = $"${cash}";
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        ResetPosition();
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
