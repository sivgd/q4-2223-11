using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public PlayerCombat PC;
    public bool attackText;
    private void OnTriggerEnter(Collider other)
    {
        TutorialCanvas.SetActive(true);

        if(attackText == true)
        {
            PC.enabled = true;
        }
    }
}
