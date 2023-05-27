using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public float sensitivity = 0.01f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ThrownObject"))
        {
            ObjectToThrowScript objScript = other.gameObject.GetComponent<ObjectToThrowScript>();
            //take the beer
            
            objScript.setSpeed(0);
            objScript.destroyObject();
            GetComponentInParent<TabouretSlotScript>().clearSlot();
        


        }
    }
}
