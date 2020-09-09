using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStats : MonoBehaviour
{

    public bool interact = false;
    public bool ok = false;
    public GameObject bubble;
    public Canvas interactCanvas;
    public Canvas messageCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (bubble == null)
            bubble = transform.Find("Bubble").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact == true)
        {
            if (ok == false && Input.GetKeyDown(KeyCode.T))
            {
                messageCanvas.gameObject.SetActive(true);
                ok = true;
            } else if (ok == true && Input.GetKeyDown(KeyCode.T))
            {
                messageCanvas.gameObject.SetActive(false);
                ok = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            interactCanvas.gameObject.SetActive(true);
            interact = true;
            bubble.gameObject.SetActive(false);
        }
        
        
        
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            messageCanvas.gameObject.SetActive(false);
            ok = false;
            interactCanvas.gameObject.SetActive(false);
            interact = false;
            bubble.gameObject.SetActive(true);
        }

    }
}
