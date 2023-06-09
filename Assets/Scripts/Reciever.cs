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
            MainAudioPlayerScript.instance.playBell();
            
            ObjectToThrowScript objScript = other.gameObject.GetComponent<ObjectToThrowScript>();
            TabouretSlotScript tabouretSlotScript = GetComponentInParent<TabouretSlotScript>();
            float t =  TabouretSlotScript.LifeTime - tabouretSlotScript.Life;
            GetComponentInParent<TabouretSlotScript>().GetComponentInParent<tabouretManager>().addScore(t);
            GetComponentInParent<TabouretSlotScript>().GetComponentInParent<tabouretManager>().beerMatHit();
            //take the beer
            objScript.setSpeed(0);
            objScript.destroyObject();
            tabouretSlotScript.clearSlot();
        }
    }
}
