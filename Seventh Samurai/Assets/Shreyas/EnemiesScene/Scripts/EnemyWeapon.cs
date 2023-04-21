using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
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
            anim.SetTrigger("Impact");
            if (pc.currentHealth <= 0)
            {
                pc.playerTrail.SetActive(false);
                pc.mat.color = Color.gray;
                pc.mat.SetColor("_EmissionColor", Color.gray);
                pc.cam.SetActive(true);
                pc.enabled = false;
                move.enabled = false;
                move.controller.enabled = false;
                anim.SetBool("Death", true);
            }
        }
    }
}
