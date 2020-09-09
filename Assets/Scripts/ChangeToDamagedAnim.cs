using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToDamagedAnim : MonoBehaviour
{   

    void Update()
    {
    
        if(this.GetComponent<Enemy>().stats.curHealth <= 10)
            this.GetComponent<Animator>().SetTrigger("Dead");
        
    }

}
