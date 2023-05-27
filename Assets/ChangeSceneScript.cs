using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    private string scoreFilepath;
    private string MaxScore;
    public tabouretManager tabouretManager;
    
    public GameObject ui;

    public GameObject gameUI;

    private static bool inUI;

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

    public Text MaxScoreDisplay;
    
    public static bool InUI
    {
        get => inUI;
        set => inUI = value;
    }

    void writeToScoreFile(String s )
    {
        File.WriteAllText(scoreFilepath, s);
        MaxScoreDisplay.text = "All Time Best "+ "\n" + s;
    }

    private void Awake()
    {
        scoreFilepath = Path.Combine(Application.persistentDataPath, "scores.txt");
        if (! File.Exists(scoreFilepath) )
        {
            writeToScoreFile("0");
        }
    }

    void Start()
    {

        TextReader reader = new StreamReader(scoreFilepath);
        string s = reader.ReadLine();
        MaxScoreDisplay.text = "All Time Best "+"\n"+ s;
        reader.Close();
        ui.SetActive(true);
        gameUI.SetActive(false);
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
            if (Vector3.Distance(camera.transform.position, EndPosition) < 0.1f)
            {
                
                isMoving = false;
                InUI = MovingToUi;
            }
        }
    }

    public void saveMaxScoreToFile()
    {
        TextReader reader = new StreamReader(scoreFilepath);
        string s = reader.ReadLine();
        reader.Close();
        if (int.Parse(s) < tabouretManager.getFinalScore() )
        {
            writeToScoreFile(tabouretManager.getFinalScore()+"");
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
        camera.transform.position = CameraEndPosition;
        EndPosition =  CameraStartPosition;
        camera.transform.rotation = CameraEndRotation;
        EndRotation =CameraStartRotation ;
        gameUI.SetActive(false);
        ui.SetActive(true);
        isMoving = true;
        InUI = true;
        MovingToUi = true;
        tabouretManager.clearSlots();
        saveMaxScoreToFile();
    }

    public void goToGameScene()
    {
        tabouretManager.resetScore();
        camera.transform.position = CameraStartPosition;
        EndPosition = CameraEndPosition;
        camera.transform.rotation = CameraStartRotation;
        EndRotation = CameraEndRotation;
        ui.SetActive(false);
        gameUI.SetActive(true);
        isMoving = true;
        MovingToUi = false;
    }
}
