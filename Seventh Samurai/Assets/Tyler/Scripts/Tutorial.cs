using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    private void OnTriggerEnter(Collider other)
    {
        TutorialCanvas.SetActive(true);
    }
}
