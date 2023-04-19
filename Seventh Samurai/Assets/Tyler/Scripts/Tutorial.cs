using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public PlayerCombat PC;
    public bool attackText;
    public Animator door1;
    public Animator door2;
    private void OnTriggerEnter(Collider other)
    {
        TutorialCanvas.SetActive(true);
        door1.SetTrigger("Door1)");
        door2.SetTrigger("Door2)");

        if(attackText == true)
        {
            PC.enabled = true;
        }
    }
}
