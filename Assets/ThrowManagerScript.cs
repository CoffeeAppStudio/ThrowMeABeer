using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrowManagerScript : MonoBehaviour
{
    public float MaxPower = 1;
    public float MinPower = 0;
    public float Scaler = 10.5f;
    public float cooldownSecond = 1;
    private Timer_ timer = new Timer_();
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
        float scaledPower = Math.Max(Math.Min(MaxPower,power),MinPower) * Scaler;
        return scaledPower ;
    }
    
    void Update()
    {
        timer.update(Time.deltaTime);
        
        if (! ChangeSceneScript.InUI)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Input.GetTouch(0).position;

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
                if (Input.GetTouch(0).phase == TouchPhase.Ended && timer.Ended)
                {
                    timer.start(cooldownSecond);
                    Vector3 touchPosition = Input.GetTouch(0).position;

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