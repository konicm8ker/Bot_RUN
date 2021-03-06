using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour
{
    Vector3 rotateAmount = new Vector3(0f,1200f,0f);
    bool disableFan = false;
    

    void Update()
    {
        // If drone bot is alive, spin fans
        if(disableFan == false)
        {
            transform.Rotate(rotateAmount * Time.deltaTime);
        }
        else
        {
            // If drone bot is dead, slowly stop fans then disable script
            rotateAmount.y -= 2f;
            if(rotateAmount.y > 0){
                transform.Rotate(rotateAmount * Time.deltaTime);
            }
            else
            {
                // Disable dust particles emission when fan comes to a stop
                var dustEmission = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
                dustEmission.enabled = false;
                enabled = false;
            }
        }
    }

    void DisableDroneFan()
    {
        disableFan = true;
    }
}
