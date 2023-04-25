using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            move.speed = 9;
            move.animator.SetFloat("Speed", 1f);
            pc.enabled = true;
            pc.mat.EnableKeyword("_EMISSION");
            pc.playerTrail.SetActive(true);
        }
    }
}
