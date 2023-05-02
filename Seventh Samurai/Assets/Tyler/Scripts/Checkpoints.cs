using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 respawnLoc;


    void Start()
    {
        Player = GameObject.Find("Player");

        if(respawnLoc != new Vector3(0, 0, 0))
        {
            Player.transform.position = respawnLoc;
        }
    }

    private void Update()
    {
        Debug.Log(respawnLoc);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            respawnLoc = other.transform.position;
        }
    }

    public void lastCheckpoint()
    {
        
        Player.transform.position = respawnLoc;
        
    }

}
