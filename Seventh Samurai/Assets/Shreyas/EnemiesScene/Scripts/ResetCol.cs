using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var anim = other.gameObject.GetComponent<Animator>();
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            move.speed = 9;
            move.animator.speed = 1f;
            pc.enabled = true;
            pc.mat.color = Color.cyan;
            pc.mat.SetColor("_EmissionColor", Color.cyan);
            pc.playerTrail.SetActive(true);
        }
    }
}