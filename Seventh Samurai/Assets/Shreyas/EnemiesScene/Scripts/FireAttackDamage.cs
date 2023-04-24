using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            pc.currentHealth -= damage;
        }
    }
}
