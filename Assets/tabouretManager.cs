using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class tabouretManager : MonoBehaviour
{

    public Text ScoreDisplay;
    
    List<TabouretSlotScript> tabouretSlotList = new List<TabouretSlotScript>();

    private Timer_ timer;
    
    private float score = 0;
    
    float Score => score;

    public int getFinalScore()
    {
        return (score*100).ConvertTo<int>();
    }
    
    public void resetScore()
    {
        score = 0;
        ScoreDisplay.text = "Score : " + 0;
    }
    
    
    public void addScore(float scoreToAdd)
    {
        score += scoreToAdd;
        ScoreDisplay.text = "Score : " + getFinalScore();
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer_();
        tabouretSlotList = this.gameObject.GetComponentsInChildren<TabouretSlotScript>().ToList();
    }
    

    public void clearSlots()
    {
        tabouretSlotList.ForEach(slot => slot.clearSlot());
    }

    public void Update()
    {
        timer.update(Time.deltaTime);
        if (!ChangeSceneScript.InUI)
        {
            if (timer.Ended)
            {
                //spawn a new tabouret on the slot choosen randomly
                int randomSlot = UnityEngine.Random.Range(0, tabouretSlotList.Count);
                timer.start(5);
                tabouretSlotList[randomSlot].spawnTabouret();
            }
        }
        
    }


    
}
