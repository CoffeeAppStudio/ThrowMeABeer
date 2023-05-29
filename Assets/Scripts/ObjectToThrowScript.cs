using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectToThrowScript : MonoBehaviour
{
    public bool checked_ = false;
    public static List<ObjectToThrowScript> objectThrown = new List<ObjectToThrowScript>();
    public float speed = 0.01f;
    public float friction = 0.0001f;
    public float separationDistance = 0.1f; 
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
        this.transform.GetChild(0).Rotate(Vector3.up, speed*Time.deltaTime*100);
        this.transform.Translate(Vector3.forward * (speed*Time.deltaTime));
        checked_ = false;
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
        

        if (other.gameObject.CompareTag("ThrownObject") && !checked_)
        {
            if (speed > other.gameObject.GetComponent<ObjectToThrowScript>().getSpeed())
            {
                checked_ = true;
                Vector3 collisionNormal = other.contacts[0].normal;
                Vector3 newDirection = Vector3.Reflect(transform.forward, collisionNormal);
                //transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
                other.gameObject.GetComponent<ObjectToThrowScript>().setSpeed(-speed*0.8f);
                other.transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
                //SeparateObjects(other.gameObject);
                speed *=0.5f;
            }

        }
        
        

    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("bar") )
        {
            if (speed != 0)
            {
                if (Math.Abs(speed) < friction)
                {
                    speed = 0;
                }
                speed -= friction * Math.Sign(speed);
            }
        }
        
    }
    
    public void destroyObject()
    {
        Destroy(this.gameObject);
    }
    
    private void SeparateObjects(GameObject otherObject)
    {
        Vector3 direction = (otherObject.transform.position - transform.position).normalized;
        float separationAmount = separationDistance / 2f;

        // Move this object slightly away from the collision point
        transform.position -= direction * separationAmount;


    }
}