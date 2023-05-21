using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class ThrowManagerScript : MonoBehaviour
{

    public float MaxPower;
    public float MinPower;
    public float Scaler;
    
    public GameObject indicator;
    public GameObject objectToThrow;
    public GameObject povManager;
    ChangeSceneScript changeSceneScript;
    private Vector3 throwCenter;
    
    void Start()
    {
        throwCenter = new Vector3(Screen.width/2f,0.1f,0);
        changeSceneScript = povManager.GetComponent<ChangeSceneScript>();
    }
    
    float scalePower(float power)
    {
        power /= Screen.height;
        Debug.Log(power);
        float scaledPower = Math.Max(Math.Min(MaxPower,power),MinPower) * Scaler;
        Debug.Log("afterScale :" + power);
        return scaledPower ;
    }
    
    void Update()
    {
        if (! changeSceneScript.InUI)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Input.GetTouch(i).position;

                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    
                    Plane plane = new Plane(Vector3.up, transform.position);
                    
                    if (plane.Raycast(ray, out float distance))
                    {
                        Vector3 worldPosition = ray.GetPoint(distance);
                        Vector3 direction =  worldPosition - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);

                        float ThrowPower = scalePower(touchPosition.y - throwCenter.y );
                        indicator.transform.rotation = rotation;
                        indicator.transform.localScale = new Vector3(1,1, ThrowPower*0.7f);
                    }
                    
                }
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    Vector3 touchPosition = Input.GetTouch(i).position;

                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                    Plane plane = new Plane(Vector3.up, transform.position);

                    if (plane.Raycast(ray, out float distance))
                    {
                        Vector3 worldPosition = ray.GetPoint(distance);
                        Vector3 direction =  worldPosition - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        float ThrowPower = scalePower(touchPosition.y - throwCenter.y);
                        indicator.transform.rotation = rotation;
                        indicator.transform.localScale = new Vector3(1,1, ThrowPower*0.7f);
                        GameObject obj = Instantiate(objectToThrow, transform.position, rotation);

                        obj.GetComponent<ObjectToThrowScript>().setSpeed(ThrowPower);
                    }
                }
            }
        }
    }
}