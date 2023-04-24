using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlowDown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();

        if (pc != null)
        {
            move.speed = 2;
            move.animator.speed = 0.5f;
            pc.enabled = false;
            pc.mat.DisableKeyword("_EMISSION");
            pc.playerTrail.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        if (pc != null)
        {
            move.speed = 2;
            move.animator.speed = 0.5f;
            pc.enabled = false;
            pc.mat.DisableKeyword("_EMISSION");
            pc.playerTrail.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var move = other.gameObject.GetComponent<tpMovement>();
        var pc = other.gameObject.GetComponent<PlayerCombat>();
        if (pc != null)
        {
            move.speed = 9;
            move.animator.speed = 1f;
            pc.enabled = true;
            pc.mat.EnableKeyword("_EMISSION");
            pc.playerTrail.SetActive(true);
        }
    }
}
