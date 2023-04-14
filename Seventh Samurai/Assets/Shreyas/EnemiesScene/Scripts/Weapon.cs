using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float damage;

    public CapsuleCollider bossCollider;

    int comboCounter;
    public List<PlayerAttackSO> combo;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var bossHealth = other.gameObject.GetComponent<BossHealth>();
        if(bossHealth != null)
        {
            bossHealth.currentHealth -= combo[comboCounter].damage;
            if (bossHealth.currentHealth <= 0)
            {
                Destroy(bossHealth.gameObject);
            }
        }
    }
}
