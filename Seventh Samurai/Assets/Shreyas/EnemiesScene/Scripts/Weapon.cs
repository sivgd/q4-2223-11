using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public int numberOfHits;

    private GameObject bossHealthMask;


    [Header("Flow State Junk")]
    public bool inCombo;
    public float comboResetTime;
    public float currentComboTime;
    public float comboDecayRate;
    public int comboLevel;
    public int flowStateLevel;
    public bool flowState;


    private void Start()
    {
        bossHealthMask = GameObject.Find("EnemyMask");
    }

    private void Update()
    {
        if(inCombo == true)
        {
            currentComboTime -= comboDecayRate * Time.deltaTime;
            if(currentComboTime <= 0)
            {
                inCombo = false;
                comboLevel = 0;
            }

            if(comboLevel >= flowStateLevel)
            {
                flowState = true;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var BE = other.gameObject.GetComponent<BossEnemy>();
        var gruntEnemy = other.gameObject.GetComponent<MeleeGruntEnemy>();
        var rangedEnemy = other.gameObject.GetComponent<RangedEnemy>();
        if(BE != null)
        {
            bossHealthMask.GetComponent<healthMask>().moveEnemyMask(BE.currentHealth, BE.maxHealth);
            BE.currentHealth -= damage;
            BE.animator.SetTrigger("Impact");
            numberOfHits += 1;
            if (BE.currentHealth <= 0)
            {
                BE.enabled = false;
                BE.col.enabled = false;
                BE.animator.SetBool("Death", true);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            comboLevel += 1;
        }
        if(gruntEnemy != null)
        {
            gruntEnemy.currentHealth -= damage;
            gruntEnemy.animator.SetTrigger("Impact");
            if (gruntEnemy.currentHealth <= 0)
            {
                gruntEnemy.enabled = false;
                gruntEnemy.agent.speed = 0;
                gruntEnemy.gruntCol.enabled = false;
                gruntEnemy.animator.SetBool("Death", true);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            comboLevel += 1;
        }
        if (rangedEnemy != null)
        {
            rangedEnemy.currentHealth -= damage;
            rangedEnemy.gruntAnimator.SetTrigger("Impact");
            rangedEnemy.bowAnimator.SetTrigger("Impact");
            if (rangedEnemy.currentHealth <= 0)
            {
                rangedEnemy.enabled = false;
                rangedEnemy.agent.speed = 0;
                rangedEnemy.rangeCol.enabled = false;
                rangedEnemy.gruntAnimator.SetBool("Death", true);
                rangedEnemy.bowAnimator.SetBool("Death", true);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            comboLevel += 1;
        }
    }
}
