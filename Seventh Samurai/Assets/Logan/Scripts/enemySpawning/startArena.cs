using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startArena : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject[] enemyTypes;
    public Transform[] spawnPoints;
    public int numberOfSpawnPoints;
    public float spawnRate = 1f;
    public float timePassed;

    //public GameObject arenaLockEnter;
    //public GameObject arenaLockExit;

    public Animator doorAnimatorEntrance;
    public Animator doorAnimatorExit;

    public bool finishArena = true;


    private void Update()
    {
        if(finishArena == true)
        {
            doorAnimatorEntrance.SetBool("openDoors", true);
            doorAnimatorExit.SetBool("openDoors", true);

            doorAnimatorEntrance.SetBool("closeDoors", false);
            doorAnimatorExit.SetBool("closeDoors", false);
        }

        if(finishArena == false)
        {
            timePassed += 1 * Time.deltaTime;

            if(timePassed >= spawnRate)
            {
                spawnEnemies();
                timePassed = 0;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            activateArenaLocks();
            finishArena = false;
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

    public void spawnEnemies()
    {
        int randIntTransform = Random.Range(0, numberOfSpawnPoints);
        int randIntType = Random.Range(0, 2);

        Instantiate(enemyTypes[randIntType], spawnPoints[randIntTransform].position, Quaternion.identity);
    }
}
