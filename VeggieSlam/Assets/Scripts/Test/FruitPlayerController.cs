﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FruitPlayerController : MonoBehaviour {

    public static int Score = 0;
    public static float time = 60f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public ParticleSystem particle;
    public GameObject laser;
    public GameObject restart;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (time > 0)
        {
            time -= Time.deltaTime;
            scoreText.text = Score.ToString();
            timerText.text = ((int)time).ToString();
        }
        else
        {
            restart.SetActive(true);
            //laser.SetActive(true); //Enable for GVR
            //OVR Change Scene
            if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
            {
                RestartScene();
            }
        }
    }

    public void PlayParticle() {
        particle.Play();
    }

    public void RestartScene ()
    {
        SceneManager.LoadScene(0);
    }
}
