using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject anim;
    public tpMovement tp;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeWait());
    }

    IEnumerator FadeWait()
    {
        tp.speed = 0;
        tp.canMove = false;
        anim.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
