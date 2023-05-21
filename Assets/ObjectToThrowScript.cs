using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToThrowScript : MonoBehaviour
{
    private float lifeTime = 10005f;
    private float lifeTimer = 0f;
    private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        lifeTimer += Time.deltaTime;
        this.transform.Translate(Vector3.forward * speed);
        if (lifeTimer > lifeTime)
            Destroy(this.gameObject);
        
    }

    public void setSpeed(float spd)
    {
        speed = spd;
        lifeTime = 0.1f/speed;

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("saloon"))
        {
            speed = 0;
        }
    }
    
    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.CompareTag("bar"))
        {
            if (speed > 0)
            {
                Debug.Log("lowering the speed");
                speed -= 0.00001f;
            }
                
        }
    }

}
