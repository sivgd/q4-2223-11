using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    BoxCollider triggerBox;

    public CapsuleCollider bossCollider;
    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bossEnemy = other.gameObject.GetComponent<BossEnemy>();
        if(bossEnemy != null)
        {
            //bossEnemy.health -= damage;
            //if (bossEnemy.health <= 0)
            //{
            //    Destroy(bossEnemy.gameObject);
            //}
        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
