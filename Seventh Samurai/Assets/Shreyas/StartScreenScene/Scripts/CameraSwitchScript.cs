using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;

    public GameObject Fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraSwitchTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CameraSwitchTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.5f);
            Fade.SetActive(false);
            Fade.SetActive(true);
            yield return new WaitForSeconds(1);
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            Fade.SetActive(false);
            Fade.SetActive(true);
            yield return new WaitForSeconds(1);
            Camera2.SetActive(false);
            Camera3.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            Fade.SetActive(false);
            Fade.SetActive(true);
            yield return new WaitForSeconds(1);
            Camera3.SetActive(false);
            Camera4.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            Fade.SetActive(false);
            Fade.SetActive(true);
            yield return new WaitForSeconds(1);
            Camera4.SetActive(false);
            Camera1.SetActive(true);
        }
    }
}
