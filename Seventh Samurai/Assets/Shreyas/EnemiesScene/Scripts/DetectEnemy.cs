using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public bool enemyDetectTrue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemyDetectTrue = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyDetectTrue = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(waitBeforeMove());
    }

    IEnumerator waitBeforeMove()
    {
        yield return new WaitForSeconds(3);
        enemyDetectTrue = false;
    }
}
