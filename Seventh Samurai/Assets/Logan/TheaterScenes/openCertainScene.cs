using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openCertainScene : MonoBehaviour
{
    public string sceneName;
   public void openScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
