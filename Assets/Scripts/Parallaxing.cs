using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;  //planurile a caror viteza vreau sa o schimb
    private float[] parrallaxScales;  // proportiile miscarii camerei cu care misc planurile
    public float smoothing = 1f;     // cat de smooth sa fie miscarea paralela (> 0)

    private Transform cam;           // referinta la main camera transform
    private Vector3 previousCamPos;  // pozitia camerei in frame-ul anterior


    private void Awake() {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start() {
        //frame-ul anterior
        previousCamPos = cam.position;

        parrallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++) {
            parrallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update() {
        for(int i = 0 ; i < backgrounds.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parrallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = 
                new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //combin pozitia curenta + target
            backgrounds[i].position = 
                Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //setez previousCamPos cu pozitia curenta
        previousCamPos = cam.position;
    }

}
