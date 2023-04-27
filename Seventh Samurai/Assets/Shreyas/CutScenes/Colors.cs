using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public GameObject enemyTrail;
    public Material mat;

    void changeColor()
    {
        enemyTrail.SetActive(false);
        mat.DisableKeyword("_EMISSION");
    }

    void ResetColor()
    {
        enemyTrail.SetActive(true);
        mat.EnableKeyword("_EMISSION");
    }
}
