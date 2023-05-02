using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public float damage;
    public int numberOfHits;

    private GameObject bossHealthMask;
    private GameObject flowBarMask;

    [Header("Flow State Junk")]
    public bool inCombo;
    public float comboResetTime;
    public float currentComboTime;
    public float comboDecayRate;
    public int comboLevel;
    public int flowStateLevel;
    public bool flowState;
    public GameObject flowStateText;
    public Animator flowStateTextAni;

    public float flowResetTime;
    public float flowComboTime;
    public float flowDecayRate;

    public AudioSource hitSound;
    public AudioSource bossHitSound;

    public AudioSource flowBarSound;

    private void Start()
    {
        bossHealthMask = GameObject.Find("EnemyMask");
        flowBarMask = GameObject.Find("flowBarMask");
        flowStateText.SetActive(false);
        flowBarSound = GameObject.Find("FlowAchievedSound").GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (inCombo == true)
        {
            currentComboTime -= comboDecayRate * Time.deltaTime;
            if(currentComboTime <= 0)
            {
                inCombo = false;
                comboLevel = 0;
                flowBarMask.GetComponent<healthMask>().moveFocusMask(comboLevel, flowStateLevel);
            }

            if(comboLevel >= flowStateLevel)
            {
                flowState = true;
                
            }



        }

        if (flowState == true)
        {
            flowStateText.SetActive(true);
            //flowStateTextAni.Play("FlowStateActive");
            flowComboTime -= flowDecayRate * Time.deltaTime;
            if (flowComboTime <= 0)
            {
                flowState = false;
            }

        }

        if (flowState == false)
        {
            flowStateText.SetActive(false);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        var BE = other.gameObject.GetComponent<BossEnemy>();
        var gruntEnemy = other.gameObject.GetComponent<MeleeGruntEnemy>();
        var rangedEnemy = other.gameObject.GetComponent<RangedEnemy>();
        var rb = other.gameObject.GetComponent<Rigidbody>();
        var startArena = FindObjectOfType<startArena>();
        hitSound = other.gameObject.GetComponentInChildren<AudioSource>();
        if (BE != null)
        {
            bossHealthMask.GetComponent<healthMask>().moveEnemyMask(BE.currentHealth, BE.maxHealth);
            if (comboLevel <= flowStateLevel)
            {
                flowBarMask.GetComponent<healthMask>().moveFocusMask(comboLevel, flowStateLevel);
            }

            bossHitSound.Play();
            BE.currentHealth -= damage;
            BE.animator.SetTrigger("Impact");
            numberOfHits += 1;
            if (BE.currentHealth <= 0)
            {
                BE.enabled = false;
                BE.col.enabled = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            flowComboTime = flowResetTime;
            comboLevel += 1;
        }
        if(gruntEnemy != null)
        {
            if (comboLevel <= flowStateLevel)
            {
                flowBarMask.GetComponent<healthMask>().moveFocusMask(comboLevel, flowStateLevel);
            }
            hitSound.Play();
            gruntEnemy.currentHealth -= damage;
            gruntEnemy.impactTrue = true;
            gruntEnemy.animator.SetTrigger("Impact");
            if (gruntEnemy.currentHealth <= 0)
            {
                gruntEnemy.enabled = false;
                gruntEnemy.agent.speed = 0;
                gruntEnemy.gruntCol.enabled = false;
                startArena.numberEnemiesAlive.Remove(gruntEnemy.gameObject);
                gruntEnemy.animator.SetBool("Death", true);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            flowComboTime = flowResetTime;
            comboLevel += 1;
        }
        if (rangedEnemy != null)
        {
            if (comboLevel <= flowStateLevel)
            {
                flowBarMask.GetComponent<healthMask>().moveFocusMask(comboLevel, flowStateLevel);
            }
            hitSound.Play();
            rangedEnemy.currentHealth -= damage;
            rangedEnemy.impactTrue = true;
            rangedEnemy.gruntAnimator.SetTrigger("Impact");
            rangedEnemy.bowAnimator.SetTrigger("Impact");
            if (rangedEnemy.currentHealth <= 0)
            {
                rangedEnemy.enabled = false;
                rangedEnemy.agent.speed = 0;
                rangedEnemy.rangeCol.enabled = false;
                startArena.numberEnemiesAlive.Remove(rangedEnemy.gameObject);
                rangedEnemy.gruntAnimator.SetBool("Death", true);
                rangedEnemy.bowAnimator.SetBool("Death", true);
            }
            inCombo = true;
            currentComboTime = comboResetTime;
            flowComboTime = flowResetTime;
            comboLevel += 1;
        }
    }
}
