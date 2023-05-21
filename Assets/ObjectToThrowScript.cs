using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToThrowScript : MonoBehaviour
{
    public static List<ObjectToThrowScript> objectThrown = new List<ObjectToThrowScript>();
    private float speed = 0.01f;
    private float friction = 0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        objectThrown.Add(this);
    }

    private void OnDestroy()
    {
        objectThrown.Remove(this);
    }
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed);
    }

    public void setSpeed(float spd)
    {
        //pour éviter le lag, peut ètre virer en release
        speed = spd;
    }

    public float getSpeed()
    {
        return speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("saloon") || other.gameObject.CompareTag("Tabouret"))
        {
            speed = 0;
            destroyObject();
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("bar") )
        {
            if (speed > 0)
            {
                Debug.Log("lowering the speed");
                speed -= friction;
            }
                
        }
    }
    
    public void destroyObject()
    {
        Destroy(this.gameObject);
    }

}
