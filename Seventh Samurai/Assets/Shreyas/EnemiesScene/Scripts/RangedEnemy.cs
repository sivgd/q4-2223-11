using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    [Header("EnemyMove")]
    public GameObject playerObj;
    public Transform player;
    public Animator gruntAnimator;
    public Animator bowAnimator;
    public LayerMask whatIsGround, whatIsPlayer;
    [HideInInspector] public Vector3 walkPoint;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool walkPointSet;
    [HideInInspector] public float walkPointRange;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public CapsuleCollider rangeCol;
    [HideInInspector] public Rigidbody rb;

    [Header("Range")]
    public float AttackRange;
    public float timeBetweenAttack;
    [HideInInspector] public bool playerInAttackRange;

    [Header("Shoot")]
    public GameObject projectile;
    public GameObject firingPoint;
    public float shootForce;
    [HideInInspector] public int bulletsShot;
    [HideInInspector] public bool shooting;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackTime;
    public bool impactTrue;
    Weapon weapon;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rangeCol = gameObject.GetComponent<CapsuleCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Transform>();
        weapon = FindObjectOfType<Weapon>();
        impactTrue = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerInAttackRange && !alreadyAttacked && !impactTrue)
        {
            gruntAnimator.SetFloat("Move", 1);
            bowAnimator.SetFloat("Move", 1);
            agent.speed = 4;
            Chase();
        }

        if (playerInAttackRange && !impactTrue)
        {
            gruntAnimator.SetFloat("Move", 0);
            bowAnimator.SetFloat("Move", 0);
            agent.speed = 0;
            Attack();
        }
        if (impactTrue)
        {
            StartCoroutine(knockback());
        }
        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }

    IEnumerator knockback()
    {
        rb.isKinematic = false;
        rb.AddForce(-transform.forward * knockbackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(knockbackTime);
        rb.isKinematic = true;
        impactTrue = false;
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        firingPoint.transform.LookAt(player);

        if (!alreadyAttacked)
        {
            StartCoroutine(attackAnim());
        }
    }

    IEnumerator attackAnim()
    {
        gruntAnimator.SetBool("Attack", true);
        bowAnimator.SetBool("Attack", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(2f);
        gruntAnimator.SetBool("Fire", true);
        bowAnimator.SetBool("Fire", true);
        Shoot();
        yield return new WaitForSeconds(2.2f);
        gruntAnimator.SetBool("Fire", false);
        bowAnimator.SetBool("Fire", false);
        gruntAnimator.SetBool("Attack", false);
        bowAnimator.SetBool("Attack", false);
        Invoke(nameof(Resetenemy), timeBetweenAttack);
    }
    private void Resetenemy()
    {
        alreadyAttacked = false;
    }

    private void Shoot()
    {
        Ray ray = new Ray(firingPoint.transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10);

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - firingPoint.transform.position;

        GameObject currentBullet = Instantiate(projectile, firingPoint.transform.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }


}
