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
    public GameObject revolver;
    public GameObject axe;
    public GameObject assaultRifle;
    public GameObject grenade;

    public bool hasLoaded = false;
    public bool inMenu = false;

    private AudioSource audioSource;
    private TMP_Text hudCashText;
    private TMP_Text shopCashText;
    private float cash = 200.0f;

    void Start() {
        hudCashText = GameObject.Find("CashText").GetComponent<TMP_Text>();
        shopCashText = GameObject.Find("ShopCashText").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        UpdateCashUI();
    }

    void Update() {
        hasLoaded = true;
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
}
