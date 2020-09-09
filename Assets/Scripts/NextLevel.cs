using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public GameObject player;
    public Player playerScript;
    public PlayerStats stats;
    public GameObject gameMaster;

    private void Awake() {
        stats = GameMaster.FindObjectOfType<GameMaster>().GetComponent<PlayerStats>();
        if(stats == null) {
            Debug.LogError("stats are null");
        }
    }


    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Player") {
            Debug.Log("m-am lovit de player");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        SaveData();
    }

    public void SaveData() {
        SavedDataBetweenScenes.Money = GameMaster.Money;
        SavedDataBetweenScenes.RemainingLives = GameMaster.RemainingLives;
        SavedDataBetweenScenes.Damage = stats.damage;
        SavedDataBetweenScenes.Speed = (int)stats.movementSpeed;
    }
}
