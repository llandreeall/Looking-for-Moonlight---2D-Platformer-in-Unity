using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizeSavedData : MonoBehaviour {

    private PlayerStats stats;

    private void Awake() {
        stats = GameMaster.FindObjectOfType<GameMaster>().GetComponent<PlayerStats>();
        if (stats == null) {
            Debug.LogError("stats are null");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.Money = SavedDataBetweenScenes.Money;
        GameMaster.RemainingLives = SavedDataBetweenScenes.RemainingLives;
        stats.damage = SavedDataBetweenScenes.Damage;
        stats.movementSpeed = SavedDataBetweenScenes.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
