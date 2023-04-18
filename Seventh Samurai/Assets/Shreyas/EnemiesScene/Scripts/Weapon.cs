using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    public CapsuleCollider bossCollider;

    public Animator anim;

    public BossEnemy BE;
    //int comboCounter;
    //public List<PlayerAttackSO> combo;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
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
                //Destroy(bossHealth.gameObject, 4);
            }
        }
    }
}
