using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    //urgdhtdth
    private GameObject resumeButton;
    private GameObject settingsButton;
    private GameObject returnToMenu;
    private GameObject pauseCanvasObj;
    private GameObject settingsCanvasObj;

    //buttons

    //bools
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton = GameObject.Find("RESUME");
        settingsButton = GameObject.Find("SETTINGS");
        returnToMenu = GameObject.Find("MENU");
        pauseCanvasObj = GameObject.Find("pauseCanvasObj");
        settingsCanvasObj = GameObject.Find("settingsCanvasObj");
        pauseCanvasObj.SetActive(false);
        settingsCanvasObj.SetActive(false);
        isPaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
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
        SceneManager.LoadScene("startScreen");
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

}
