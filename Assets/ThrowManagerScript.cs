using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ThrowManagerScript : MonoBehaviour
{
    public GameObject objectToThrow;
    public GameObject povManager;
    ChangeSceneScript changeSceneScript;
    
    void Start()
    {
        changeSceneScript =povManager.GetComponent<ChangeSceneScript>();
    }
    
    void Update()
    {
        if (! changeSceneScript.InUI)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Input.GetTouch(i).position;
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    Plane plane = new Plane(Vector3.up, transform.position);
                    if (plane.Raycast(ray, out float distance))
                    {
                        Vector3 worldPosition = ray.GetPoint(distance);
                        Vector3 direction =  worldPosition - transform.position ;
                        
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        
                        GameObject obj = Instantiate(objectToThrow, transform.position, rotation);
                        
                        obj.GetComponent<ObjectToThrowScript>().setSpeed(direction.magnitude*0.001f);
                    }
                }
            }
        }
    }
}