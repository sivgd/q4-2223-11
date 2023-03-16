using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponTesting : MonoBehaviour
{
    public GameObject attackCol;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {

            attackCol.SetActive(true);

            animator.SetBool("attack", true);
            Invoke("stopAnimation", 2.5f);
        }
    }

    public void stopAnimation()
    {
        animator.SetBool("attack", false);
        attackCol.SetActive(false);
    }



}
