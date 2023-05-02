using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    private GameObject pauseCanvasObj;
    private GameObject settingsCanvasObj;
    private GameObject deathCanvasObj;
    public GameObject fadeOut;

    //bools
    public bool isPaused;
    public bool isDead;
    public tpMovement tp;
    public PlayerCombat PC;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvasObj = GameObject.Find("pauseCanvasObj");
        settingsCanvasObj = GameObject.Find("settingsCanvasObj");
        deathCanvasObj = GameObject.Find("deathCanvasObj");
        deathCanvasObj.SetActive(false);
        pauseCanvasObj.SetActive(false);
        settingsCanvasObj.SetActive(false);
        isDead = false;
        isPaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false && !isDead)
        {
            pauseCanvasObj.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            unPause();
        }


        if(PC.currentHealth <= 0 && isDead == false)
        {
            isDead = true;
            StartCoroutine(DeathWait());
        }
    }

    public void unPause()
    {
        pauseCanvasObj.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void returnToMenuButton()
    {
        Time.timeScale = 1;
        pauseCanvasObj.SetActive(false);
        StartCoroutine(FadeWait());
    }

    public void resetScene()
    {
        Time.timeScale = 1;
        pauseCanvasObj.SetActive(false);
        deathCanvasObj.SetActive(false);
        StartCoroutine(FadeWaitReset());
    }

    public void openSettings()
    {
        pauseCanvasObj.SetActive(false);
        settingsCanvasObj.SetActive(true);
    }
    public void closeSettings()
    {
        pauseCanvasObj.SetActive(true);
        settingsCanvasObj.SetActive(false);
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(3);
        deathCanvasObj.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator FadeWait()
    {
        fadeOut.SetActive(true);
        tp.speed = 0;
        tp.canMove = false;
        PC.canAttack = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("startScreen");
    }

    IEnumerator FadeWaitReset()
    {
        fadeOut.SetActive(true);
        tp.speed = 0;
        tp.canMove = false;
        PC.canAttack = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
