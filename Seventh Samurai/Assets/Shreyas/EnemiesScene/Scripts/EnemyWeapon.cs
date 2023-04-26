using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    public GameObject playerHealthMask;

    public void Start()
    {
        playerHealthMask = GameObject.Find("Mask");
    }

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        //var move = other.gameObject.GetComponent<tpMovement>();
        if (pc != null)
        {
            playerHealthMask.GetComponent<healthMask>().moveMask(pc.maxHealth, -damage);
            pc.currentHealth -= damage;
            anim.SetTrigger("Impact");
        }
    }
}
