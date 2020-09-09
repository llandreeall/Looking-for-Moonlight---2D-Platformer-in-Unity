using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    [SerializeField]
    string mouseOverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    AudioManager audioManager;

    private void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("no audio manager found");
        }
    }


    public void Quit() {
        audioManager.PlaySound(buttonPressSound);
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void Retry() {
        audioManager.PlaySound(buttonPressSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver() {
        audioManager.PlaySound(mouseOverSound);
    }
}
