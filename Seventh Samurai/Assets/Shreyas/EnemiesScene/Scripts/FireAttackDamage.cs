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
            anim.SetTrigger("Impact");
            if (playerHealth.currentHealth <= 0)
            {
                pc.playerTrail1.SetActive(false);
                pc.mat.color = Color.gray;
                pc.mat.SetColor("_EmissionColor", Color.gray);
                pc.mat.color = Color.gray;
                pc.mat2.SetColor("_EmissionColor", Color.gray);
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
            anim.SetTrigger("Impact");
            if (playerHealth.currentHealth <= 0)
            {
                pc.playerTrail1.SetActive(false);
                pc.mat.color = Color.gray;
                pc.mat.SetColor("_EmissionColor", Color.gray);
                pc.mat.color = Color.gray;
                pc.mat2.SetColor("_EmissionColor", Color.gray);
                pc.cam.SetActive(true);
                move.enabled = false;
                pc.enabled = false;
                move.controller.enabled = false;
                anim.SetBool("Death", true);
            }
        }
    }
}
