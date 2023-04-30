using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;

    public GameObject playerHealthMask;
    public GameObject flowBarMask;

    public void Start()
    {
        playerHealthMask = GameObject.Find("Mask");
        flowBarMask = GameObject.Find("flowBarMask");
    }

    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        var playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();
        //var move = other.gameObject.GetComponent<tpMovement>();
        if (pc != null)
        {
            StopCoroutine(pc.playerRegenHealth());
            pc.currentHealth -= damage;
            playerHealthMask.GetComponent<healthMask>().moveMask(pc.currentHealth, pc.maxHealth);
            pc.healPlayer = false;
            StartCoroutine(pc.playerRegenHealth());
            anim.SetTrigger("Impact");
            playerWeapon.comboLevel = 0;
            playerWeapon.inCombo = false;
            playerWeapon.currentComboTime = 0;
            playerWeapon.flowState = false;

            flowBarMask.GetComponent<healthMask>().moveFocusMask(pc.GetComponentInChildren<Weapon>().comboLevel, pc.GetComponentInChildren<Weapon>().flowStateLevel);

            //StopCoroutine(pc.playerRegenHealth());
        }
    }
}
