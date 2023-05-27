using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DefaultNamespace;
public class tabouretManager : MonoBehaviour
{
    
    List<Animator> tabouretAnimList = new List<Animator>();
    
    List<TabouretSlotScript> tabouretSlotList = new List<TabouretSlotScript>();
    
    Timer_ timer = new Timer_();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        tabouretSlotList = this.gameObject.GetComponentsInChildren<TabouretSlotScript>().ToList();
        //tabouretAnimList = this.gameObject.GetComponentsInChildren<Animator>().ToList();

    }
    

    public void clearSlots()
    {
        tabouretSlotList.ForEach(slot => slot.clearSlot());
    }

    public void Update()
    {
        if (!ChangeSceneScript.InUI)
        {
            timer.update(Time.deltaTime);
            if (timer.Ended)
            {
                //spawn a new tabouret on the slot choosen randomly
                int randomSlot = UnityEngine.Random.Range(0, tabouretSlotList.Count);
                timer.start(5);
                tabouretSlotList[randomSlot].spawnTabouret();
            }
        }

        
    }

    public void playAnim(bool playForward = true)
    {
        foreach (Animator anim in tabouretAnimList)
        {
            if(playForward)
                anim.SetTrigger("goToFloor");
            else
                anim.SetTrigger("goToTable");
        }
    }
    
}
