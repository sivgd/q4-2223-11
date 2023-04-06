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
        var bossHealth = other.gameObject.GetComponent<BossHealth>();
        if(bossHealth != null)
        {
            bossHealth.currentHealth -= damage;
            if (bossHealth.currentHealth <= 0)
            {
                Destroy(bossHealth.gameObject);
            }
        }
    }
}
