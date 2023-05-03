using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeAfterCutScene : MonoBehaviour
{
    public float cutSceneLength;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(changeAfterCutScene());
    }

    IEnumerator changeAfterCutScene()
    {
        yield return new WaitForSeconds(cutSceneLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
