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


    Vector3 velocity;
    


    private void Start()
    {

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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            animator.SetFloat("movementSpeed", 1);
            //StartCoroutine(changeAniMove());
            //StopCoroutine(changeAniMoveDown());

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("movementSpeed", 0);
        }
        //else
        //{
        //    StopCoroutine(changeAniMove());
        //    StartCoroutine(changeAniMoveDown());
        //}
    }   

        //IEnumerator changeAniMove()
        //{
        //    if (animator.GetFloat("movementSpeed") <= 1)
        //    {
        //        animator.SetFloat("movementSpeed", (animator.GetFloat("movementSpeed") + (aniTransSpeed * 3)));
        //    }

        //    yield return null;
        //}

        //IEnumerator changeAniMoveDown()
        //{
        //    if (animator.GetFloat("movementSpeed") >= 0)
        //    {
        //        animator.SetFloat("movementSpeed", (animator.GetFloat("movementSpeed") - aniTransSpeed));
        //    }

        //    yield return null;
        //}

    

}