using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour
{
    
    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    private AudioManager audioManager;

    [SerializeField]
    private StatusIndicator statusIndicator;

    private PlayerStats stats;

    void Start()
    {
        stats = PlayerStats.instance;
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        } else {
            Debug.LogError("No status indicator referenced on Player");
        }

        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        audioManager = AudioManager.instance;
        if(audioManager == null) {
            Debug.LogError("no audio manager found");
        }
    }

    private void Update()
    {
        if(transform.position.y <= -20)
        {
            DamagePlayer(99999999);
        }
    }

    void OnUpgradeMenuToggle(bool active)
    {
        if (this != null)
        {
            //handle what happens when the upgrade menu is toggled
            GetComponent<Platformer2DUserControl>().enabled = !active;
            Weapon _weapon = GetComponentInChildren<Weapon>();
            if (_weapon != null)
                _weapon.enabled = !active;
        }
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0) {
            //play death sound
            audioManager.PlaySound(deathSoundName);
            GameMaster.KillPlayer(this);
        } else {
            //play damage sound
            audioManager.PlaySound(damageSoundName);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
}
