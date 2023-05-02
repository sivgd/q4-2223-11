using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    [Header("Enemy Move")]
    NavMeshAgent agent;
    Animation anim;
    [HideInInspector]
    public Vector3 walkPoint;
    [HideInInspector]
    public bool walkPointSet;
    [HideInInspector]
    public float walkPointRange;

    [Header("Necessary Items")]
    public Transform player;
    public Material mat;
    public GameObject Trail;
    public GameObject fire;
    public Transform firePos;
    public LayerMask whatIsGround, whatIsPlayer;
    [HideInInspector] public CapsuleCollider col;
    [HideInInspector] public Animator animator;
    EnemyWeapon weapon;
    bool canRotate;
    public AudioSource fireAttackSound;
    public AudioSource chargeUpSound;
    public AudioSource launchSound;
    public AudioSource swingSound;


    [Header("Attack Stuff")]
    float timeDuringAttack;
    bool keepTiming;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public bool isAttacking;

    [Header("Basic Attack")]
    public int damage1;
    public int damage2;
    public int damage3;
    public float basicAttackRange;
    public float coolDownTime = 2f;
    [HideInInspector] public bool playerInBasicAttackRange;

    [Header("Fire Attack")]
    public float fireAttackRange;
    public float timeBetweenFireAttack;
    [HideInInspector] public bool playerInFireAttackRange;

    [Header("Dash Attack")]
    public float dashSpeed;
    public float timeBetweenDashAttack;
    bool attack3 = false;
    Vector3 lastPosition;

    [Header("Reverse Dash")]
    public float reverseDashForce;
    public float reverseDashTime;
    bool reverseDashTrue;
    public int numberOfHitsToActivate;
    Weapon playerWeapon;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Look")]
    float turnSpeed = 0.1f;
    Quaternion rotGoal;
    Vector3 lookDirection;
    Vector3 direction;
    Rigidbody rb;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = gameObject.GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        weapon = FindObjectOfType<EnemyWeapon>();
        playerWeapon = FindObjectOfType<Weapon>();
        keepTiming = true;
        canRotate = true;
        reverseDashTrue = false;
        isAttacking = false;
        ResetColor();

        currentHealth = maxHealth;
    }
    private void Update()
    {
        playerInBasicAttackRange = Physics.CheckSphere(transform.position, basicAttackRange, whatIsPlayer);
        playerInFireAttackRange = Physics.CheckSphere(transform.position, fireAttackRange, whatIsPlayer);
        if (keepTiming)
        {
            timeBetweenFireAttack -= Time.deltaTime;
            timeBetweenDashAttack -= Time.deltaTime;
        }

        if (timeBetweenFireAttack <= 0 || timeBetweenDashAttack <= 0)
        {
            keepTiming = false;
        }

        if (!playerInBasicAttackRange && playerInFireAttackRange && !isAttacking && !reverseDashTrue)
        {
            animator.SetFloat("Move", 1);
            agent.speed = 6;
            Chase();
            keepTiming = true;
            if (timeBetweenFireAttack <= 0 || timeBetweenDashAttack <= 0)
            {
                keepTiming = false;
            }
            else
            {
                keepTiming = true;
            }
        }

        if (playerInBasicAttackRange && playerInFireAttackRange && !isAttacking && !reverseDashTrue)
        {
            animator.SetFloat("Move", 0);
            agent.speed = 0;
            BasicAttack();
            keepTiming = true;
            if (timeBetweenFireAttack <= 0 || timeBetweenDashAttack <= 0)
            {
                keepTiming = false;
            }
            else
            {
                keepTiming = true;
            }
        }

        if (playerInFireAttackRange && timeBetweenFireAttack <= 0 && !reverseDashTrue)
        {
            animator.SetFloat("Move", 0);
            agent.speed = 0;
            FireAttack();
            keepTiming = false;
        }

        if (!playerInBasicAttackRange && !alreadyAttacked && timeBetweenDashAttack <= 0 && !reverseDashTrue && !isAttacking)
        {
            animator.SetFloat("Move", 0);
            agent.speed = 0;
            DashAttack();
            keepTiming = false;
        }
        if(playerWeapon.numberOfHits >= numberOfHitsToActivate)
        {
            ReverseDash();
        }

        if(canRotate == true)
        {
            lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
            direction = (lookDirection - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        }
    }

    //Reverse Dash
    void ReverseDash()
    {
        animator.SetBool("Attack2", false);
        StartCoroutine(reverseDashAnim());
    }
    IEnumerator reverseDashAnim()
    {
        col.enabled = false;
        reverseDashTrue = true;
        animator.ResetTrigger("Impact");
        animator.SetBool("ReverseJump", true);
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        rb.AddForce(-transform.forward * reverseDashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(reverseDashTime);
        playerWeapon.numberOfHits = 0;
        rb.isKinematic = true;
        col.enabled = true;
        animator.SetBool("ReverseJumpLanded", true);
        yield return new WaitForSeconds(1.6f);
        reverseDashTrue = false;
        animator.SetBool("ReverseJumpLanded", false);
        animator.SetBool("ReverseJump", false);
    }

    //Chase
    private void Chase()
    {
        animator.SetFloat("Move", 1);
        agent.SetDestination(player.position);
    }

    //Combo Attack
    private void BasicAttack()
    {
        if (!alreadyAttacked)
        {
            StartCoroutine(basicAttackAnim());
        }
    }

    IEnumerator basicAttackAnim()
    {
        col.enabled = false;
        isAttacking = true;
        coolDownTime = Random.Range(3f, 5f);
        animator.ResetTrigger("Impact");
        animator.SetBool("Attack2", true);
        alreadyAttacked = true;
        //Hit1
        weapon.damage = damage1;
        animator.SetBool("Hit1", true);
        swingSound.Play();
        yield return new WaitForSeconds(1f);
        animator.SetBool("Hit1", false);
        //Hit2
        weapon.damage = damage2;
        animator.SetBool("Hit2", true);
        swingSound.Play();
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hit2", false);
        //Hit3
        weapon.damage = damage3;
        animator.SetBool("Hit3", true);
        swingSound.Play();
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hit3", false);
        //Reset
        col.enabled = true;
        isAttacking = false;
        animator.ResetTrigger("Impact");
        animator.SetBool("Attack2", false);
        Invoke(nameof(ResetBasicAttack), coolDownTime);
    }
    private void ResetBasicAttack()
    {
        alreadyAttacked = false;
    }
    //Fire Attack
    private void FireAttack()
    {
        //fireAttackSound = fire.GetComponentInChildren<AudioSource>();

        if (attack3 == false)
        {
            agent.SetDestination(transform.position);
        }
        if (!alreadyAttacked)
        {
            StartCoroutine(fireAttackAnim());
        }
    }

    IEnumerator fireAttackAnim()
    {
        fireAttackSound.Play();
        col.enabled = false;
        isAttacking = true;
        for (int i = 0; i < 3; i++)
        {
            if (playerInFireAttackRange && !playerInBasicAttackRange)
            {
                animator.SetBool("Attack", true);
                alreadyAttacked = true;
                yield return new WaitForSeconds(1f);
                Instantiate(fire, firePos.transform.position, firePos.transform.rotation);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Attack", false);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                break;
            }
        }
        animator.ResetTrigger("Impact");
        col.enabled = true;
        isAttacking = false;
        Invoke(nameof(FireAttackReset), timeBetweenFireAttack);
    }

    private void FireAttackReset()
    {
        alreadyAttacked = false;
        timeBetweenFireAttack += 15;
        keepTiming = true;
    }

    //Dash Attack
    private void DashAttack()
    {
        attack3 = true;
        if (!alreadyAttacked)
        {
            StartCoroutine(dashAttackAnim());
        }
    }

    IEnumerator dashAttackAnim()
    {
        animator.SetBool("Attack3", true);
        isAttacking = true;
        alreadyAttacked = true;
        Vector3 startingPos = transform.position;
        chargeUpSound.Play();
        float dashGo = Random.Range(2, 3);
        yield return new WaitForSeconds(dashGo);
        launchSound.Play();
        lastPosition = new Vector3(player.position.x, 0, player.position.z);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("DashTrue", true);
        
        for (float time = 0; time < 1; time += Time.deltaTime * dashSpeed)
        {
            if (!playerInBasicAttackRange)
            {
                canRotate = false;
                transform.position = Vector3.Lerp(startingPos, lastPosition, time);
            }
            yield return null;
        }
        animator.SetBool("DashTrue", false);
        animator.SetBool("DashEnd", true);
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Impact");
        canRotate = true;
        animator.SetBool("DashEnd", false);

        if(NavMesh.SamplePosition(player.transform.position, out NavMeshHit hit, 1f, agent.areaMask) && !playerInBasicAttackRange)
        {
            agent.Warp(transform.position);
        }
        animator.SetBool("Attack3", false);
        isAttacking = false;
        attack3 = false;
        Invoke(nameof(DashAttackReset), timeBetweenDashAttack);

    }
    private void DashAttackReset()
    {
        alreadyAttacked = false;
        timeBetweenDashAttack += 10;
        keepTiming = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fireAttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, basicAttackRange);

    }

    void ChangeColor()
    {
        Trail.SetActive(false);
        mat.DisableKeyword("_EMISSION");
    }

    void ResetColor()
    {
        Trail.SetActive(true);
        mat.EnableKeyword("_EMISSION");
    }
}
