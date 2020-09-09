using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player") {
            GameMaster.Money += 1;
            Instantiate(explosion, transform.position, transform.rotation);
            Object.Destroy(this.gameObject, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
