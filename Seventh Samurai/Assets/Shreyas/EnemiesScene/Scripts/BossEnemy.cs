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
    float timeDuringAttack;
    public float timeBetweenAttack;
    public float timeBetweenAttack2;
    public float timeBetweenAttack3;
    bool keepTiming;
    [HideInInspector]
    public bool alreadyAttacked;

    [Header("Range")]
    public float attackRange, attackRange2, attackRange3;
    public bool playerInAttackRange, playerInAttackRange2, playerInAttackRange3;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    AttackBehaviour atLength;
    public float length;
    public GameObject fire;
    public Transform firePos;
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

        if (!playerInAttackRange2 && !alreadyAttacked && timeBetweenAttack > 0)
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
        }
        
        if(playerInAttackRange && playerInAttackRange2 && playerInAttackRange3)
        {
            Attack3();
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
            animator.SetBool("Attack", true);
            alreadyAttacked = true;
            yield return new WaitForSeconds(1.4f);
            Instantiate(fire, firePos.transform.position, firePos.transform.rotation);
            yield return new WaitForSeconds(1.233f);
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(2);
        }
        Invoke(nameof(Resetenemy2), timeBetweenAttack);
    }
    IEnumerator attackAnim2()
    {
        animator.SetBool("Attack2", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(length);
        animator.SetBool("Attack2", false);
        Invoke(nameof(Resetenemy), timeBetweenAttack2);
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
        float timeAdd = Random.Range(2, 5);
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
