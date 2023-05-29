using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public GameObject povManager;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ThrownObject"))
        {
            povManager.GetComponent<ChangeSceneScript>().changeScene();
        }
    }
}
