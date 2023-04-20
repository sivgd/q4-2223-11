using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var BE = other.gameObject.GetComponent<BossEnemy>();
        var bossHealth = other.gameObject.GetComponent<BossHealth>();
        if(bossHealth != null)
        {
            bossHealth.currentHealth -= damage;
            anim.SetTrigger("Impact");
            if (bossHealth.currentHealth <= 0)
            {
                BE.enabled = false;
                BE.col.enabled = false;
                anim.SetBool("Death", true);
            }
        }
    }
}
