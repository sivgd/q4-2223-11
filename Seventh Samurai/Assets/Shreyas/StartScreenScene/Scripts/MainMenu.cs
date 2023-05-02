using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject anim;
    public Animator animMusic;
    public void PlayGame()
    {
        StartCoroutine(FadeWait());
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    IEnumerator FadeWait()
    {
        animMusic.enabled = true;
        anim.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
