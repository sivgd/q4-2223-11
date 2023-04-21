using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startArena : MonoBehaviour
{

    public GameObject enemyType1;
    public GameObject enemyType2;

    //public GameObject arenaLockEnter;
    //public GameObject arenaLockExit;

    public Animator doorAnimatorEntrance;
    public Animator doorAnimatorExit;

    public bool finishArena = false;


    private void Update()
    {
        if(finishArena == true)
        {
            doorAnimatorEntrance.SetBool("openDoors", true);
            doorAnimatorExit.SetBool("openDoors", true);

            doorAnimatorEntrance.SetBool("closeDoors", false);
            doorAnimatorExit.SetBool("closeDoors", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            activateArenaLocks();
        }
    }


    public void activateArenaLocks()
    {
        doorAnimatorEntrance.SetBool("closeDoors", true);
        doorAnimatorExit.SetBool("closeDoors", true);

        doorAnimatorEntrance.SetBool("openDoors", false);
        doorAnimatorExit.SetBool("openDoors", false);
    }

    public void deactivateArenaLocks()
    {
        doorAnimatorEntrance.SetBool("openDoors", true);
        doorAnimatorExit.SetBool("openDoors", true);

        doorAnimatorEntrance.SetBool("closeDoors", false);
        doorAnimatorExit.SetBool("closeDoors", false);
    }


}
