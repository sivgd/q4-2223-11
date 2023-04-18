using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    //public CapsuleCollider playerCollider;

    public Animator anim;

    public tpMovement move;
    public PlayerCombat pc;

    public GameObject cam;
    //int comboCounter;
    //public List<PlayerAttackSO> combo;
    private void Start()
    {
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
