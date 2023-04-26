using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDamage : MonoBehaviour
{
    public float damage;
    
    public GameObject playerHealthMask;

    public void Start()
    {
        playerHealthMask = GameObject.Find("Mask");
    }
    private void OnTriggerEnter(Collider other)
    {
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            playerHealthMask.GetComponent<healthMask>().moveMask(pc.maxHealth, damage);
            pc.currentHealth -= damage;
        }
    }
}
