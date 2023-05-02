using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class theaterSceneReturn : MonoBehaviour
{
    public float cutSceneLength;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeAfterCutScene());
    }

    IEnumerator changeAfterCutScene()
    {
        yield return new WaitForSeconds(cutSceneLength);
        SceneManager.LoadScene("StartScreen");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

}
