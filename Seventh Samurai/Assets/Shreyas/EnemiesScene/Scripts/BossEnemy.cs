using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    [Header("Enemy Move")]
    NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    Animation anim;
    public Vector3 walkPoint;
    [HideInInspector]
    public bool walkPointSet;
    public float walkPointRange;
    bool canRotate;
    public CapsuleCollider col;
    public EnemyWeapon weapon;
    public Material mat;
    public GameObject Trail;

    [Header("Attack Stuff")]
    float timeDuringAttack;
    bool keepTiming;
    [HideInInspector]
    public bool alreadyAttacked;
    //AttackBehaviour atLength;
    //public float length;

    [Header("Fire Attack")]
    public float attackRange;
    public float timeBetweenAttack;
    public bool playerInAttackRange;
    public GameObject fire;
    public Transform firePos;

    [Header("Basic Attack")]
    public float attackRange2;
    public float coolDownTime = 2f;
    public bool playerInAttackRange2;

    [Header("Dash Attack")]
    public float dashSpeed;
    public Transform playerGroundCheck;
    public float timeBetweenAttack3;
    public float attackRange3;
    public bool playerInAttackRange3;
    bool attack3 = false;
    Vector3 lastPosition;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 lookDirection;
    Vector3 direction;
    Rigidbody rb;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //atLength = animator.GetBehaviour<AttackBehaviour>();
        rb = GetComponent<Rigidbody>();
        keepTiming = true;
        canRotate = true;
    }
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInAttackRange2 = Physics.CheckSphere(transform.position, attackRange2, whatIsPlayer);
        playerInAttackRange3 = Physics.CheckSphere(transform.position, attackRange3, whatIsPlayer);
        if (keepTiming)
        {
            timeBetweenAttack -= Time.deltaTime;
        }

        if (!playerInAttackRange2 && playerInAttackRange3 && !alreadyAttacked)
        {
            animator.SetFloat("Move", 0);
            Chase();
            keepTiming = true;
        }

        if(playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3 && timeBetweenAttack <= 0)
        {
            animator.SetFloat("Move", 0);
            Attack();
            keepTiming = false;
        }

        if (playerInAttackRange && playerInAttackRange2 && playerInAttackRange3)
        {
            animator.SetFloat("Move", 0);
            Attack2();
            keepTiming = false;
        }
        
        if(!playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3 && !alreadyAttacked)
        {
            animator.SetFloat("Move", 0);
            Attack3();
            keepTiming = false;
        }

        //length = animator.GetCurrentAnimatorStateInfo(0).length;
        //timeDuringAttack = length + timeBetweenAttack;

        if(canRotate == true)
        {
            lookDirection = new Vector3(playerGroundCheck.position.x, transform.position.y, playerGroundCheck.position.z);
            direction = (lookDirection - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        }
    }
    private void Chase()
    {
        animator.SetFloat("Move", 1);
        agent.SetDestination(player.position);
    }
    //Fire Attack
    private void Attack()
    {
        if(attack3 == false)
        {
            agent.SetDestination(transform.position);
        }
        if (!alreadyAttacked)
        {
            animator.SetBool("Attack2", false);
            StartCoroutine(attackAnim());
        }
    }
    IEnumerator attackAnim()
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3)
            {
                animator.SetBool("Attack", true);
                alreadyAttacked = true;
                yield return new WaitForSeconds(1.4f);
                Instantiate(fire, firePos.transform.position, firePos.transform.rotation);
                yield return new WaitForSeconds(1.2f);
                animator.SetBool("Attack", false);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                break;
            }
        }
        animator.ResetTrigger("Impact");
        Invoke(nameof(Resetenemy2), timeBetweenAttack);
    }

    //Combo Attack
    private void Attack2()
    {
        if (attack3 == false)
        {
            agent.SetDestination(transform.position);
        }
        StartCoroutine(waitBeforeAttack());
    }
    IEnumerator attackAnim2()
    {
        coolDownTime = Random.Range(2f, 5f);
        weapon.damage = 2;
        animator.SetBool("Attack2", true);
        alreadyAttacked = true;
        animator.SetBool("Hit1", true);
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("Hit1", false);
        if (playerInAttackRange2 == true)
        {
            weapon.damage = 5;
            animator.SetBool("Hit2", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Hit2", false);
            if (playerInAttackRange2 == true)
            {
                weapon.damage = 8;
                animator.SetBool("Hit3", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Hit3", false);
            }
            else
            {
                animator.ResetTrigger("Impact");
                animator.SetBool("Attack2", false);
                Invoke(nameof(Resetenemy), coolDownTime);
            }
        }
        else
        {
            animator.ResetTrigger("Impact");
            animator.SetBool("Attack2", false);
            Invoke(nameof(Resetenemy), coolDownTime);
        }
        animator.ResetTrigger("Impact");
        animator.SetBool("Attack2", false);
        Invoke(nameof(Resetenemy), coolDownTime);
    }

    //Dash Attack
    private void Attack3()
    {
        attack3 = true;
        if (!alreadyAttacked)
        {
            animator.SetBool("Attack2", false);
            StartCoroutine(attackAnim3());
        }
    }
    IEnumerator attackAnim3()
    {
        animator.SetBool("Attack3", true);
        alreadyAttacked = true;
        agent.enabled = false;
        Vector3 startingPos = transform.position;
        animator.SetBool("Attack3", true);
        lastPosition = playerGroundCheck.position;
        yield return new WaitForSeconds(0.13f);
        //slashEffect.SetActive(true);
        for (float time = 0; time < 1; time += Time.deltaTime * dashSpeed)
        {
            if (!playerInAttackRange2)
            {
                canRotate = false;
                transform.position = Vector3.Lerp(startingPos, lastPosition, time);
            }
            yield return null;
        }
        animator.SetBool("BossLanded", true);
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("Impact");
        canRotate = true;
        animator.SetBool("BossLanded", false);
        agent.enabled = true;

        if(NavMesh.SamplePosition(playerGroundCheck.transform.position, out NavMeshHit hit, 1f, agent.areaMask) && !playerInAttackRange2)
        {
            agent.Warp(transform.position);
        }
        animator.SetBool("Attack3", false);
        attack3 = false;
        Invoke(nameof(Resetenemy), timeBetweenAttack3);

    }
    private void Resetenemy()
    {
        alreadyAttacked = false;
    }
    private void Resetenemy2()
    {
        float timeAdd = Random.Range(10, 15);
        alreadyAttacked = false;
        timeBetweenAttack += timeAdd;
        keepTiming = true;
    }

    IEnumerator waitBeforeAttack()
    {
        yield return new WaitForSeconds(0.1f);
        if (!alreadyAttacked)
        {
            animator.SetBool("Attack3", false);
            StartCoroutine(attackAnim2());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange3);

    }

    void ChangeColor()
    {
        Trail.SetActive(false);
        mat.color = Color.gray;
        mat.SetColor("_EmissionColor", Color.gray);
    }

    void ResetColor()
    {
        Trail.SetActive(true);
        Color orange = new Color(0.7490196f, 0.2823529f, 0.01176471f);
        Color orangeGlow = new Color(1.059274f, 0.06100529f, 0f);
        mat.color = orange;
        mat.SetColor("_EmissionColor", orangeGlow);
    }
}
