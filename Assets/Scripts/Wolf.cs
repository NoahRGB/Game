using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

    public float audioCooldown = 5.0f;
    public AudioClip deathSound;
    public List<AudioClip> clips = new List<AudioClip>();

    private bool isDead = false;
    private AudioSource audioSource;
    private float nextAudioTime = 0.0f;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        
        if (!isDead && Time.time > nextAudioTime) {
            nextAudioTime += audioCooldown;
            int clipIndex = Random.Range(0, clips.Count);
            audioSource.PlayOneShot(clips[clipIndex]);
        }
        
    }

    public void die() {
        isDead = true;
        audioSource.PlayOneShot(deathSound);
    }
}
