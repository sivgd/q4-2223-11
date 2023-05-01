using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeGruntEnemy : MonoBehaviour
{
    [Header("EnemyMove")]
    [HideInInspector] public NavMeshAgent agent;
    public GameObject playerObj;
    public Transform player;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;
    [HideInInspector] public Vector3 walkPoint;
    [HideInInspector] public bool walkPointSet;
    [HideInInspector] public float walkPointRange;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public bool DeathTrue;
    [HideInInspector] public Rigidbody rb;
    public CapsuleCollider gruntCol;
    DetectEnemy detect;

    [Header("Attack")]
    public float timeBetweenAttack;
    public float AttackRange;
    [HideInInspector] public bool playerInAttackRange;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackTime;
    public bool impactTrue;
    Weapon weapon;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;


    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        gruntCol = gameObject.GetComponent<CapsuleCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        weapon = FindObjectOfType<Weapon>();
        detect = gameObject.GetComponentInChildren<DetectEnemy>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Transform>();
        impactTrue = false;
        currentHealth = maxHealth;
        DeathTrue = false;
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);


        if (!playerInAttackRange && detect.enemyDetectTrue == false && !impactTrue)
        {
            animator.SetFloat("Move", 1);
            agent.speed = 8;
            Chase();
        }
        else if(detect.enemyDetectTrue == true)
        {
            animator.SetFloat("Move", 0);
            agent.speed = 0;
        }
        if (playerInAttackRange)
        {
            animator.SetFloat("Move", 0);
            Attack();
            direction = (player.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        }
        if(impactTrue)
        {
            StartCoroutine(knockback());
        }
    }

    IEnumerator knockback()
    {
        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        rb.isKinematic = false;
        rb.AddForce(-transform.forward * knockbackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(knockbackTime);
        rb.isKinematic = true;
        impactTrue = false;
    }

    private void Chase()
    {
        animator.ResetTrigger("Impact");
        animator.SetBool("Attack", false);
        agent.SetDestination(player.position);

    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

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
