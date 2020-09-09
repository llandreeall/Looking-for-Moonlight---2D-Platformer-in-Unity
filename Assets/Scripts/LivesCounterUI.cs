﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour {

    private Text livesText;


    private void Awake() {
        livesText = GetComponent<Text>();
    }


    void Update() {
        livesText.text = "LIVES: " + GameMaster.RemainingLives;
    }
}
