﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
   

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Player")) {
            GameMaster.KillPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
        }
    }
}