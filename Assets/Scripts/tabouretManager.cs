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

    public MainAudioPlayerScript mainAudioScript;
    
    public Text ScoreDisplay;
    
    List<TabouretSlotScript> tabouretSlotList = new List<TabouretSlotScript>();

    private Timer_ timer;
    
    private float score = 0;
    public static float startTimeSpawnIntervalCustomers = 4;
    public float probabilitySpawnTwoCustomers = 0.33f;

    public float timeSpawnIntervalCustomers = startTimeSpawnIntervalCustomers; 

    private int bnBeers = 0;
    
    float Score => score;

    public int getFinalScore()
    {
        return (score*100).ConvertTo<int>();
    }

    public void init()
    {
        foreach( var tab in tabouretSlotList)
        {
            tab.init();
        } 
    }
    
    public void resetScore()
    {
        timeSpawnIntervalCustomers = startTimeSpawnIntervalCustomers;
        bnBeers = 0;
        score = 0;
        ScoreDisplay.text = "" + 0;
    }
    
    
    public void addScore(float scoreToAdd)
    {
        score += scoreToAdd;
        ScoreDisplay.text = "" + getFinalScore();
    }

    public float getDifficultyCurve()
    {
        return TabouretSlotScript.InitiallifeTime - (bnBeers / 20); 
    }

    public float getSpawnDifficultyCurve()
    {
        return startTimeSpawnIntervalCustomers - (bnBeers / 16);
    }
    
    
    public void beerMatHit()
    {
        TabouretSlotScript.setLifeTime(getDifficultyCurve());
        
        timeSpawnIntervalCustomers = getSpawnDifficultyCurve();
        
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
                if (UnityEngine.Random.Range(0, 1f) < probabilitySpawnTwoCustomers)
                {
                    int t1,t2;
                    do
                    {
                        t1 = UnityEngine.Random.Range(0, tabouretSlotList.Count);
                        t2 = UnityEngine.Random.Range(0, tabouretSlotList.Count);
                    } while (t1 == t2);
                    tabouretSlotList[t1].spawnTabouret();
                    tabouretSlotList[t2].spawnTabouret();
                    timer.start(timeSpawnIntervalCustomers);
                }
                else
                {
                    int randomSlot = UnityEngine.Random.Range(0, tabouretSlotList.Count);
                    tabouretSlotList[randomSlot].spawnTabouret();
                    timer.start(timeSpawnIntervalCustomers);
                }

            }
        }
        
    }


    
}
