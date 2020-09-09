
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    string hoverOverSound = "ButtonHover";
    AudioManager audioManager;

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    [SerializeField]
    Canvas controls;


    private void Start() {
        audioManager = AudioManager.instance;
        if(audioManager == null) {
            Debug.LogError("no audio manager found");
        }
        controls.enabled = false;
    }

    public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Replay() {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("WE QUIT THE GAME!");
        Application.Quit();
    }

    public void OnMouseOver() {
        audioManager.PlaySound(hoverOverSound);
    }

    public void Controls() {
        controls.enabled = true;
    }

    public void ControlsOff() {
        controls.enabled = false;
    }

}
