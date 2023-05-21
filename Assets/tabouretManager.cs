using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tabouretManager : MonoBehaviour
{
    
    List<Animator> tabouretAnimList = new List<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        tabouretAnimList = this.gameObject.GetComponentsInChildren<Animator>().ToList();
        Debug.Log("tabouretAnimList : " + tabouretAnimList.Count);
    }

    
    public void playAnim(bool playForward = true)
    {
        Debug.Log("playAnim");
        foreach (Animator anim in tabouretAnimList)
        {
            print("anim : " + anim.name);
            
            if(playForward)
                anim.SetTrigger("goToFloor");
            else
                anim.SetTrigger("goToTable");
        }
    }
    
}
