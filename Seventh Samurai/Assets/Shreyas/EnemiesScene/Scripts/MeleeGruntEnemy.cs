using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeGruntEnemy : MonoBehaviour
{
    [Header("EnemyMove")]
    NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;
    [HideInInspector] public Vector3 walkPoint;
    [HideInInspector] public bool walkPointSet;
    [HideInInspector] public float walkPointRange;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public CapsuleCollider gruntCol;

    [Header("Attack")]
    public float timeBetweenAttack;
    public float AttackRange;
    [HideInInspector] public bool playerInAttackRange;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [HideInInspector] public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        gruntCol = gameObject.GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerInAttackRange)
        {
            animator.SetFloat("Move", 1);
            Chase();
        }

        if (playerInAttackRange)
        {
            animator.SetFloat("Move", 0);
            Attack();
        }
    }

    private void Chase()
    {
        animator.SetBool("Attack", false);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        if (!alreadyAttacked)
        {
            StartCoroutine(attackAnim());
        }
    }
    IEnumerator attackAnim()
    {
        animator.SetBool("Attack", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(1);
        animator.SetBool("Attack", false);
        Invoke(nameof(Resetenemy), timeBetweenAttack);
    }
    private void Resetenemy()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }


}
