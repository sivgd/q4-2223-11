using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    public BoxCollider playerCollider;
    public GameObject player;
    Animator anim;
    tpMovement move;
    PlayerCombat pc;
    public GameObject cam;

    [SerializeField] private Material mat;
    private void Start()
    {
        anim = player.GetComponent<Animator>();
        move = player.GetComponent<tpMovement>();
        pc = player.GetComponent<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth -= damage;
            anim.SetTrigger("Impact");

            if (playerHealth.currentHealth <= 0)
            {
                mat.color = Color.red;
                mat.SetColor("_EmissionColor", Color.red);
                cam.SetActive(true);
                move.enabled = false;
                pc.enabled = false;
                move.controller.enabled = false;
                anim.SetBool("Death", true);
                //Destroy(playerHealth.gameObject, 4);
            }
        }
    }
}
