using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("test");

        if(other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("TEST");
            Destroy(other.gameObject);
        }    
    }
}
