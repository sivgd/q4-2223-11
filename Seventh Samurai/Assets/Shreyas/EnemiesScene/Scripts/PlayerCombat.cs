using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    private tpMovement pMove;

    public float timeBetweenAttacks = 0.9f;

    Animator anim;
    [SerializeField] Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon.DisableTriggerBox();
        anim = GetComponent<Animator>();
        pMove = GetComponent<tpMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weapon.EnableTriggerBox();
            pMove.enabled = false;
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {

        

        if (Time.time - lastComboEnd > 1f && comboCounter <= combo.Count)
        {
            pMove.enabled = false;
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= timeBetweenAttacks)
            {
                

                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("AttackState", 0, 0);
                //pMove.enabled = false;
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
        
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            pMove.enabled = true;
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        weapon.DisableTriggerBox();
        lastComboEnd = Time.time;
    }
}