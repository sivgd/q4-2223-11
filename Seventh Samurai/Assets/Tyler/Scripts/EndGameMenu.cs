using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public int timeWait;
    public GameObject endUI;
    public GameObject fadeCanvas;
    public Animator fade;
    // Start is called before the first frame update
    void Start()
    {
        endUI.SetActive(false);
        fadeCanvas.SetActive(false);
        StartCoroutine("imDumb");
    }

    public IEnumerator imDumb()
    {
        yield return new WaitForSeconds(timeWait);
        endUI.SetActive(true);
    }

    public IEnumerator waitforFade()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("StartScreen");
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void returnToMenu()
    {
        fadeCanvas.SetActive(true);
        //fade.Play("Fade");
        StartCoroutine("waitforFade");
    }
}
