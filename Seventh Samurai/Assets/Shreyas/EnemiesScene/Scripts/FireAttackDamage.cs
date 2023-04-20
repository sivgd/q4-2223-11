using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDamage : MonoBehaviour
{
    public float damage;
    public float damage2;

    //Animator anim;
    //tpMovement move;
    //PlayerCombat pc;

    //public GameObject player;
    //public GameObject enemySword;
    //public GameObject cam;
    //public GameObject trail;

    //[SerializeField] private Material mat;
    //[SerializeField] private Material mat2;
    private void Start()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.currentHealth -= damage;
            move.speed = 2;
            move.animator.speed = 0.5f;
            pc.enabled = false;
            pc.mat.color = Color.gray;
            pc.mat.SetColor("_EmissionColor", Color.gray);
            pc.playerTrail1.SetActive(false);
            if (playerHealth.currentHealth <= 0)
            {
                pc.playerTrail1.SetActive(false);
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

    private void OnTriggerStay(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth -= 0.1f;
            move.speed = 2;
            move.animator.speed = 0.5f;
            pc.enabled = false;
            pc.mat.color = Color.gray;
            pc.mat.SetColor("_EmissionColor", Color.gray);
            pc.playerTrail1.SetActive(false);
            //anim.SetTrigger("Impact");
            if (playerHealth.currentHealth <= 0)
            {
                pc.playerTrail1.SetActive(false);
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

    private void OnTriggerExit(Collider other)
    {
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        move.speed = 9;
        move.animator.speed = 1f;
        pc.enabled = true;
        pc.mat.color = Color.cyan;
        pc.mat.SetColor("_EmissionColor", Color.cyan);
        pc.playerTrail1.SetActive(true);
    }

    public void setSpeed()
    {
        var move = FindObjectOfType<tpMovement>();
        move.speed = 9;
        move.animator.speed = 1f;
    }
}
