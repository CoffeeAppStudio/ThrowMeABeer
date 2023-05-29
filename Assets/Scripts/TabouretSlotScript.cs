using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabouretSlotScript : MonoBehaviour
{
    private bool tabouretInstanciated = false;
    public GameObject tabouret;

    private GameObject instanciatedTabouret;
    // Start is called before the first frame update

    private float life = 0;
    public float lifeTime = 10;

    public float LifeTime => lifeTime;
    public float Life => life;

    private void Update()
    {
        if (tabouretInstanciated)
        {
            life += Time.deltaTime;
            if (life > lifeTime)
            {
                clearSlot();
                life = 0;
            }
        }

    }


    public void spawnTabouret()
    {
        if (!tabouretInstanciated)
        {
            life = 0;
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
            life = 0;
            Destroy(instanciatedTabouret.gameObject);
            //play drinking anim
            tabouretInstanciated = false;
            
        }

    }
    
}
