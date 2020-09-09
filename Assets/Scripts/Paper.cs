using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour {

    Canvas canvas;
    GameObject player;
    float difference = 2;
    bool show;

    private void Awake() {
        canvas = GetComponentInChildren<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        show = false;
        canvas.enabled = false;
        canvas.GetComponentInChildren<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update() {

        Debug.Log(player.transform.position.x + " " + transform.position.x);

        if((transform.position.x - difference < player.transform.position.x) 
            && (transform.position.x + difference > player.transform.position.x)) {
            canvas.enabled = true;
        } else {
            canvas.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.P) && canvas.enabled == true) {
            canvas.GetComponentInChildren<Image>().enabled = !(canvas.GetComponentInChildren<Image>().enabled);
        }


    }
}
