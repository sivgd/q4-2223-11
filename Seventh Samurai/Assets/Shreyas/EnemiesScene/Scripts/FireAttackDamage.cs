using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDamage : MonoBehaviour
{
    public float damage;

    private void Start()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            pc.currentHealth -= damage;
            if (pc.currentHealth <= 0)
            {
                pc.playerTrail.SetActive(false);
                pc.mat.color = Color.gray;
                pc.mat.SetColor("_EmissionColor", Color.gray);
                pc.cam.SetActive(true);
                move.enabled = false;
                pc.enabled = false;
                move.controller.enabled = false;
                anim.SetBool("Death", true);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    var anim = other.gameObject.GetComponent<Animator>();
    //    var move = other.gameObject.GetComponent<tpMovement>();
    //    var pc = other.gameObject.GetComponent<PlayerCombat>();
    //    if (pc != null)
    //    {
    //        move.speed = 2;
    //        move.animator.speed = 0.5f;
    //        pc.enabled = false;
    //        pc.mat.color = Color.gray;
    //        pc.mat.SetColor("_EmissionColor", Color.gray);
    //        pc.playerTrail.SetActive(false);
    //        if (pc.currentHealth <= 0)
    //        {
    //            pc.playerTrail.SetActive(false);
    //            pc.mat.color = Color.gray;
    //            pc.mat.SetColor("_EmissionColor", Color.gray);
    //            pc.cam.SetActive(true);
    //            move.enabled = false;
    //            pc.enabled = false;
    //            move.controller.enabled = false;
    //            anim.SetBool("Death", true);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    var move = other.gameObject.GetComponent<tpMovement>();
    //    var pc = other.gameObject.GetComponent<PlayerCombat>();
    //    if(pc != null)
    //    {
    //        move.speed = 9;
    //        move.animator.speed = 1f;
    //        pc.enabled = true;
    //        pc.mat.color = Color.cyan;
    //        pc.mat.SetColor("_EmissionColor", Color.cyan);
    //        pc.playerTrail.SetActive(true);
    //    }
    //}

    //public void setSpeed()
    //{
    //    var move = FindObjectOfType<tpMovement>();
    //    move.speed = 9;
    //    move.animator.speed = 1f;
    //}
}
