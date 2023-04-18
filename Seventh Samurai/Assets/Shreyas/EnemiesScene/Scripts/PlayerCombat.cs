using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<PlayerAttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    private tpMovement pMove;

    public float timeBetweenAttacks = 0.9f;
    public bool canAttack;
    Animator anim;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        anim = GetComponent<Animator>();
        pMove = GetComponent<tpMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack == true && pMove.isGrounded == true)
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 1f && comboCounter <= combo.Count)
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

                if(comboCounter + 1 > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.3f);
            //pMove.speed = 9;
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        pMove.speed = 9;
    }
}
