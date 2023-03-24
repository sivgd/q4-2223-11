using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    [Header("EnemyMove")]
    NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    Animation anim;
    public Vector3 walkPoint;
    [HideInInspector]
    public bool walkPointSet;
    public float walkPointRange;

    [Header("Attack Stuff")]
    float timeDuringAttack;
    bool keepTiming;
    [HideInInspector]
    public bool alreadyAttacked;
    AttackBehaviour atLength;
    public float length;

    [Header("Attack 1")]
    public float attackRange;
    public float timeBetweenAttack;
    public bool playerInAttackRange;

    [Header("Attack 2")]
    public float attackRange2;
    public float coolDownTime = 2f;
    bool hit1;
    bool hit2;
    bool hit3;
    bool inAttackRange;
    public bool playerInAttackRange2;

    [Header("Attack 3")]
    public float attackRange3;
    public float timeBetweenAttack3;
    public bool playerInAttackRange3;
    public GameObject fire;
    public Transform firePos;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    //AIManager aimanager;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        atLength = animator.GetBehaviour<AttackBehaviour>();
        keepTiming = true;
    }
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInAttackRange2 = Physics.CheckSphere(transform.position, attackRange2, whatIsPlayer);
        playerInAttackRange3 = Physics.CheckSphere(transform.position, attackRange3, whatIsPlayer);

        if(keepTiming)
        {
            timeBetweenAttack -= Time.deltaTime;
        }

        if (!playerInAttackRange2 && !playerInAttackRange3 && !alreadyAttacked)
        {
            Chase();
            keepTiming = true;
        }

        if(playerInAttackRange && !playerInAttackRange2 && !playerInAttackRange3 && timeBetweenAttack <= 0)
        {
            Attack();
            keepTiming = false;
        }

        if (playerInAttackRange && playerInAttackRange2 && !playerInAttackRange3)
        {
            Attack2();
            keepTiming = false;
        }
        
        if(playerInAttackRange && playerInAttackRange2 && playerInAttackRange3)
        {
            Attack3();
            keepTiming = false;
        }

        animator.SetFloat("Move", agent.velocity.magnitude);

        length = animator.GetCurrentAnimatorStateInfo(0).length;
        timeDuringAttack = length + timeBetweenAttack;

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            animator.SetBool("Attack2", false);
            StartCoroutine(attackAnim());
        }
    }
    private void Attack2()
    {
        agent.SetDestination(transform.position);
        StartCoroutine(waitBeforeAttack());
    }
    private void Attack3()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            animator.SetBool("Attack2", false);
            StartCoroutine(attackAnim3());
        }
    }
    IEnumerator attackAnim()
    {
        for (int i = 0; i < 3; i++)
        {
            if(playerInAttackRange && !playerInAttackRange2 && !playerInAttackRange3)
            {
                animator.SetBool("Attack", true);
                alreadyAttacked = true;
                yield return new WaitForSeconds(1.8f);
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
        Invoke(nameof(Resetenemy2), timeBetweenAttack);
    }
    IEnumerator attackAnim2()
    {
        animator.SetBool("Attack2", true);
        alreadyAttacked = true;
        animator.SetBool("Hit1", true);
        yield return new WaitForSeconds(1.667f);
        animator.SetBool("Hit1", false);
        if (playerInAttackRange2 == true)
        {
            animator.SetBool("Hit2", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("Hit2", false);
            if(playerInAttackRange2 == true)
            {
                animator.SetBool("Hit3", true);
                yield return new WaitForSeconds(2.433f);
                animator.SetBool("Hit3", false);
            }
            else
            {
                animator.SetBool("Attack2", false);
                Invoke(nameof(Resetenemy), coolDownTime);
            }
        }
        else
        {
            animator.SetBool("Attack2", false);
            Invoke(nameof(Resetenemy), coolDownTime);
        }
        animator.SetBool("Attack2", false);
        Invoke(nameof(Resetenemy), coolDownTime);

    }
    IEnumerator attackAnim3()
    {
        animator.SetBool("Attack3", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Attack3", false);
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


}
