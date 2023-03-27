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
    public GameObject slashEffect;

    [Header("Fire Attack")]
    public float attackRange;
    public float timeBetweenAttack;
    public bool playerInAttackRange;
    public GameObject fire;
    public Transform firePos;

    [Header("Basic Attack")]
    public float attackRange2;
    public float coolDownTime = 2f;
    bool hit1;
    bool hit2;
    bool hit3;
    bool inAttackRange;
    public bool playerInAttackRange2;

    [Header("Jump Attack")]
    public AnimationCurve HeightCurve;
    public float JumpSpeed;
    public Transform playerGroundCheck;
    public float timeBetweenAttack3;
    public float attackRange3;
    public bool playerInAttackRange3;
    bool attack3 = false;

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
        slashEffect.SetActive(false);
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
            Chase();
            keepTiming = true;
        }

        if(playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3 && timeBetweenAttack <= 0)
        {
            Attack();
            keepTiming = false;
        }

        if (playerInAttackRange && playerInAttackRange2 && playerInAttackRange3)
        {
            Attack2();
            keepTiming = false;
        }
        
        if(!playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3 && !alreadyAttacked)
        {
            Attack3();
            keepTiming = false;
        }

        animator.SetFloat("Move", agent.velocity.magnitude);

        length = animator.GetCurrentAnimatorStateInfo(0).length;
        timeDuringAttack = length + timeBetweenAttack;


        direction = (playerGroundCheck.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }
    //private void lockRot()
    //{
    //    Vector3 playerLock = new Vector3(transform.position.x, player.position.y, player.position.z);
    //}
    //private void lookAtPlayer()
    //{
    //    direction = (player.position - transform.position).normalized;
    //    rotGoal = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    //}
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
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
        slashEffect.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if (playerInAttackRange && !playerInAttackRange2 && playerInAttackRange3)
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
        slashEffect.SetActive(false);
        Invoke(nameof(Resetenemy2), timeBetweenAttack);
    }

    private void Attack2()
    {
        if(attack3 == false)
        {
            agent.SetDestination(transform.position);
        }
        StartCoroutine(waitBeforeAttack());
    }
    IEnumerator attackAnim2()
    {
        slashEffect.SetActive(true);
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
            if (playerInAttackRange2 == true)
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
        yield return new WaitForSeconds(0.13f);
        slashEffect.SetActive(true);
        for (float time = 0; time < 1; time += Time.deltaTime * JumpSpeed)
        {
            if (!playerInAttackRange2)
            {
                transform.position = Vector3.Lerp(startingPos, playerGroundCheck.transform.position, time) + Vector3.up * HeightCurve.Evaluate(time);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerLock - transform.position), time);
            }
            yield return null;
        }
        animator.SetBool("BossLanded", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("BossLanded", false);
        //slashEffect.SetActive(false);
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
        slashEffect.SetActive(false);
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
