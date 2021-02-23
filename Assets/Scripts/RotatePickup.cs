using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour
{
    Vector3 rotateAmount = new Vector3(0f,50f,0f);

    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}
