using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpMovement : MonoBehaviour
{
    [Header("Cam Stuff")]
    public CharacterController controller;
    public Transform cam;

    [Header("Needed Values")]
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Grounding Stuff")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Animation Stuff")]
    public Animator animator;
    public float aniTransSpeed = 0.01f;

    [Header("Dash Stuff")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public bool canDash;
    public bool dashTrue;
    PlayerCombat PC;
    Vector3 velocity;
    public bool canMove;

    private void Start()
    {
        canMove = true;
        canDash = true;
        dashTrue = false;
        PC = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Input.GetKeyDown("e") && canDash == true && canMove == true)
        {
            PC.enabled = false;
            canDash = false;
            dashTrue = true;
            gravity = 0;
            animator.SetBool("Dash", true);
            StartCoroutine(Dash());
        }
        if(dashTrue == true)
        {
            PC.enabled = false;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && canMove == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        if (direction.magnitude >= 0.1f && canMove == true)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            animator.SetBool("Run", true);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Run", false);
        }

    }

    public IEnumerator Dash()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float startTime = Time.time;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }
        gravity = -32f;
        animator.SetBool("Dash", false);
        dashTrue = false;
        //controller.enabled = true;
        yield return new WaitForSeconds(0.1f);
        PC.enabled = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}