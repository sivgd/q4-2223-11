using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeWait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeWait()
    {
        yield return new WaitForSeconds(5);
        anim.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
