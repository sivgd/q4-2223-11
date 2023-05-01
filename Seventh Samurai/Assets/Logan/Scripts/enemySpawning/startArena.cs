using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startArena : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject[] enemyTypes;
    public Transform[] spawnPoints;
    public float spawnRate = 1f;
    public int maxNumberOfEnemies;
    public List<GameObject> numberEnemiesAlive;
    [HideInInspector] public int numberOfSpawnPoints;
    [HideInInspector] public int numberOfEnemyTypes;
    [HideInInspector]public float timePassed;
    [HideInInspector]public int numberOfEnemies;

    public Animator doorAnimatorEntrance;
    public Animator doorAnimatorExit;
    public GameObject nextArena;

    public AudioSource doorNoise1;
    public AudioSource doorNoise2;

    public AudioSource music;

    [HideInInspector]public bool arenaStarted;

    private void Awake()
    {
        numberOfSpawnPoints = spawnPoints.Length;
        numberOfEnemyTypes = enemyTypes.Length;
        timePassed = spawnRate;
        deactivateArenaLocks();
        nextArena.SetActive(false);
    }
    private void Update()
    {
        if(arenaStarted == true)
        {
            if(numberOfEnemies < maxNumberOfEnemies)
            {
                timePassed += 1 * Time.deltaTime;

                if (timePassed >= spawnRate)
                {
                    spawnEnemies();
                    timePassed = 0;
                }
            }

            if(numberEnemiesAlive.Count == 0 && timePassed == 0)
            {
                arenaStarted = false;
                gameObject.SetActive(false);
                nextArena.SetActive(true);
                deactivateArenaLocks();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            activateArenaLocks();
            arenaStarted = true;
        }
    }


    public void activateArenaLocks()
    {
        doorAnimatorEntrance.SetBool("closeDoors", true);
        doorAnimatorExit.SetBool("closeDoors", true);
        doorNoise1.Play();
        doorNoise2.Play();

        music.Play();

        doorAnimatorEntrance.SetBool("openDoors", false);
        doorAnimatorExit.SetBool("openDoors", false);
    }

    public void deactivateArenaLocks()
    {
        doorAnimatorEntrance.SetBool("openDoors", true);
        doorAnimatorExit.SetBool("openDoors", true);
        doorNoise1.Play();
        doorNoise2.Play();

        music.Stop();

        doorAnimatorEntrance.SetBool("closeDoors", false);
        doorAnimatorExit.SetBool("closeDoors", false);
    }

    public void spawnEnemies()
    {
        int randIntTransform = Random.Range(0, numberOfSpawnPoints);
        int randIntType = Random.Range(0, numberOfEnemyTypes);

        GameObject enemy = Instantiate(enemyTypes[randIntType], spawnPoints[randIntTransform].position, Quaternion.identity);
        numberEnemiesAlive.Add(enemy);
        numberOfEnemies += 1;
    }
}
