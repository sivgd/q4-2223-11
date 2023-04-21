using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public int numberOfHits;

    private void OnTriggerEnter(Collider other)
    {
        var anim = FindObjectOfType<Animator>();
        var BE = other.gameObject.GetComponent<BossEnemy>();
        if(BE != null)
        {
            BE.currentHealth -= damage;
            BE.animator.SetTrigger("Impact");
            numberOfHits += 1;
            if (BE.currentHealth <= 0)
            {
                BE.enabled = false;
                BE.col.enabled = false;
                BE.animator.SetBool("Death", true);
            }
        }
    }
}
