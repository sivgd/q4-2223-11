using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    public GameObject[] cameras;
    //public GameObject Camera1;
    //public GameObject Camera2;
    //public GameObject Camera3;
    //public GameObject Camera4;

    public int currentCam;
    public GameObject Fade;

    public float camAnimTime;
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
            cameras[currentCam].SetActive(true);
            yield return new WaitForSeconds(camAnimTime);
            Fade.SetActive(false);
            Fade.SetActive(true);
            yield return new WaitForSeconds(1);
            cameras[currentCam].SetActive(false);
            if (currentCam == 3)
            {
                currentCam = 0;
            }
            else
            {
                currentCam++;
            }
        }


    }

}
