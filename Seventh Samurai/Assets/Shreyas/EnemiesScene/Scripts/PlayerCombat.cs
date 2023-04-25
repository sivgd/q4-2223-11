using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<PlayerAttackSO> combo;
    [Header("Necessary Items")]
    public float timeBetweenAttacks;
    public GameObject playerTrail;
    public GameObject cam;
    public Material mat;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;
    

    [HideInInspector] public bool canAttack;
    [HideInInspector] public tpMovement pMove;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    Weapon weapon;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        pMove = gameObject.GetComponent<tpMovement>();
        weapon = FindObjectOfType<Weapon>();
        canAttack = true;
        ResetColor();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack == true && pMove.isGrounded == true)
        {
            mat.color = Color.cyan;
            mat.SetColor("_EmissionColor", Color.cyan);
            Attack();
        }

        if (currentHealth <= 0)
        {
            canAttack = false;
            playerTrail.SetActive(false);
            mat.DisableKeyword("_EMISSION");
            cam.SetActive(true);
            enabled = false;
            pMove.enabled = false;
            pMove.controller.enabled = false;
            pMove.animator.speed = 0.5f;
            anim.SetBool("Death", true);
        }
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.01f && comboCounter <= combo.Count)
        {
            pMove.speed = 0;
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= timeBetweenAttacks)
            {
                if (Input.GetKeyDown("e") && pMove.canDash == true && pMove.canMove == true)
                {
                    pMove.enabled = true;
                    pMove.canDash = false;
                    pMove.dashTrue = true;
                    pMove.gravity = 0;
                    pMove.animator.SetBool("Dash", true);
                    pMove.StartCoroutine(pMove.Dash());
                }
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("AttackState", 0, 0);
                weapon.damage = combo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;
                if (comboCounter + 1 > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.35f);
            pMove.speed = 0;
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        pMove.speed = 9;
    }


    void changeColor()
    {
        playerTrail.SetActive(false);
        mat.DisableKeyword("_EMISSION");
    }

    void ResetColor()
    {
        playerTrail.SetActive(true);
        mat.EnableKeyword("_EMISSION");
    }
}
