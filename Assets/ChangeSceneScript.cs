using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void ChangeScene()
    {

        SceneManager.LoadScene("GameScene");
        //unload the current scene
        
    }
}
