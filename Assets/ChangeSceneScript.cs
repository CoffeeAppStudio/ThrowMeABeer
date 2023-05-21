using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    
    public tabouretManager tabouretManager;
    
    public GameObject ui;

    private bool inUI;

    public Transform StartTransformCamera;
    
    public Transform EndTransformCamera;
    
    private Vector3 CameraStartPosition;
    private Quaternion CameraStartRotation;
    
    private Vector3 CameraEndPosition;
    private Quaternion CameraEndRotation;
    
    private Vector3 Startposition;
    private Quaternion StartRotation;
    
    private Vector3 EndPosition;
    private Quaternion EndRotation;
    
    public float speed = 0.0001f;

    public GameObject camera;

    public bool MovingToUi = false;
    
    public bool InUI
    {
        get => inUI;
        set => inUI = value;
    }
    
    void Start()
    {
        ui.SetActive(true);
        CameraStartPosition = StartTransformCamera.position;
        CameraStartRotation = StartTransformCamera.rotation;
        CameraEndPosition = EndTransformCamera.position;
        CameraEndRotation = EndTransformCamera.rotation;
        EndPosition = CameraStartPosition;
        EndRotation = CameraStartRotation;
        InUI = true;
        Debug.Log("started");
    }

    private bool isMoving = false;
    
    void Update()
    {
        if (isMoving)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, EndPosition, speed*Time.deltaTime);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, EndRotation, speed*Time.deltaTime);
            //si la camera a fini son mouvement à une variable pres
        
            if (Vector3.Distance(camera.transform.position, EndPosition) < 0.1f)
            {
                isMoving = false;
                InUI = MovingToUi;
            }
        }
        
    }
    
    public void changeScene()
    {
        if(InUI)
        {
            goToGameScene();
        }
        else
        {
            goToUiScene();
            
        }
    }

    public void goToUiScene()
    {
        
        foreach (var ob in ObjectToThrowScript.objectThrown)
        {
            ob.destroyObject();
        }
        
        Debug.Log("ChangeScene to UI");
        camera.transform.position = CameraEndPosition;
        EndPosition =  CameraStartPosition;
        camera.transform.rotation = CameraEndRotation;
        EndRotation =CameraStartRotation ;
        ui.SetActive(true);
        isMoving = true;
        InUI = true;
        MovingToUi = true;
        tabouretManager.playAnim(false);
    }

    public void goToGameScene()
    {
        Debug.Log("ChangeScene to game");
        camera.transform.position = CameraStartPosition;
        EndPosition = CameraEndPosition;
        camera.transform.rotation = CameraStartRotation;
        EndRotation = CameraEndRotation;
        ui.SetActive(false);
        isMoving = true;
        MovingToUi = false;
        tabouretManager.playAnim();
    }
    
    
    
}
