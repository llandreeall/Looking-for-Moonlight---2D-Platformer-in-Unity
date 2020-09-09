using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class StopCameraAtBeggining : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -6)
        {
            gameObject.GetComponent<Camera2DFollow>().enabled = false;
            Debug.Log("Acum ar trebui sa se opreasca, camera are pozitia -7");
        }
            

        if (gameObject.GetComponent<Camera2DFollow>().enabled == false && GameObject.FindGameObjectWithTag("Player").transform.position.x > -4)
        {
            gameObject.GetComponent<Camera2DFollow>().enabled = true;
            gameObject.GetComponent<Camera2DFollow>().target = GameObject.FindGameObjectWithTag("Player").transform;
           Debug.Log("Acum ar trebui sa porneasca,playerul are pozitia -10");
        }
            
    }
}
