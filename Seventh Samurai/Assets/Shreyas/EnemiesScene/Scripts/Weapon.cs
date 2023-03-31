using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    BoxCollider triggerBox;

    public CapsuleCollider bossCollider;
    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bossEnemy = other.gameObject.GetComponent<BossEnemy>();
        if(bossEnemy != null)
        {
            bossEnemy.animator.SetBool("Attack", false);
            bossEnemy.animator.SetBool("Attack2", false);
            bossEnemy.animator.SetBool("Attack3", false);
            bossEnemy.animator.SetBool("Hit1", false);
            bossEnemy.animator.SetBool("Hit2", false);
            bossEnemy.animator.SetBool("Hit3", false);
            bossEnemy.health -= damage;
            StartCoroutine(ImpactWait());
            if (bossEnemy.health <= 0)
            {
                //DisableTriggerBox();
                StartCoroutine(DeathWait());
            }
        }

        IEnumerator ImpactWait()
        {
            bossEnemy.animator.SetBool("Impact", true);
            yield return new WaitForSeconds(0.167f);
            bossEnemy.animator.SetBool("Impact", false);
        }

        IEnumerator DeathWait()
        {
            bossEnemy.animator.SetBool("Death", true);
            bossCollider.enabled = false;
            bossEnemy.animator.SetBool("Impact", false);
            yield return new WaitForSeconds(3.9f);
            Destroy(bossEnemy.gameObject);
        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
