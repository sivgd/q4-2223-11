using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        var move = other.gameObject.GetComponent<tpMovement>();
        if (pc != null)
        {
            move.animator.speed = 1;
            pc.currentHealth -= damage;
            anim.SetTrigger("Impact");
        }
    }
}
