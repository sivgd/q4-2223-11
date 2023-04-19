using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    Animator anim;
    tpMovement move;
    PlayerCombat pc;

    public GameObject player;
    public GameObject cam;
    public GameObject trail;

    [SerializeField] private Material mat;
    [SerializeField] private Material mat2;
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
                Death();
            }
        }
    }

    public void Death()
    {
        trail.SetActive(false);
        mat.color = Color.gray;
        mat.SetColor("_EmissionColor", Color.gray);
        mat2.color = Color.gray;
        mat2.SetColor("_EmissionColor", Color.gray);
        cam.SetActive(true);
        move.enabled = false;
        pc.enabled = false;
        move.controller.enabled = false;
        anim.SetBool("Death", true);
    }
}
