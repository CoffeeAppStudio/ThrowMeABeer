using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabouretSlotScript : MonoBehaviour
{
    private bool tabouretInstanciated = false;
    public GameObject tabouret;

    private GameObject instanciatedTabouret;
    // Start is called before the first frame update
    


    
    public void spawnTabouret()
    {
        if (!tabouretInstanciated)
        {
            tabouretInstanciated = true;
            instanciatedTabouret = Instantiate(tabouret, this.transform.position + Vector3.up,transform.rotation);
            instanciatedTabouret.transform.parent = this.transform;
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("goToFloor");
        }
    }

    public void clearSlot()
    {
        if (tabouretInstanciated)
        {
            Destroy(instanciatedTabouret.gameObject);
            //play drinking anim
            tabouretInstanciated = false;
        }

    }
    
}
