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
    [HideInInspector]
    public bool alreadyAttacked;

    [Header("Range")]
    public float sightRange, AttackRange;
    public bool playerSightInRange, playerInAttackRange;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    AttackBehaviour atLength;
    public float length;
    //AIManager aimanager;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        atLength = animator.GetBehaviour<AttackBehaviour>();
    }
    private void Update()
    {
        playerSightInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerSightInRange && !playerInAttackRange)
        {
            Patrol();
        }
        if (playerSightInRange && !playerInAttackRange)
        {
            Chase();
        }

        if (playerInAttackRange && playerSightInRange)
        {
            Attack();
        }

        animator.SetFloat("Move", agent.velocity.magnitude);

        length = animator.GetCurrentAnimatorStateInfo(0).length;
        timeDuringAttack = length + timeBetweenAttack;
    }

    private void Patrol()
    {
        animator.SetBool("Attack", false);
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
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
        yield return new WaitForSeconds(length);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
