using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCameraScript : MonoBehaviour
{
    public GameObject[] cameras;
    public List<float> camAnimTimes;

    public int numberOfCams;
    public int currentCam;
    public int currentCamAnimTime;
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
        while (currentCam <= numberOfCams && currentCamAnimTime <= numberOfCams)
        {
            cameras[currentCam].SetActive(true);
            yield return new WaitForSeconds(camAnimTimes[currentCamAnimTime]);
            if(currentCam <= numberOfCams)
            {
                cameras[currentCam].SetActive(false);
                currentCam++;
                currentCamAnimTime++;
            }
            else
            {
                cameras[currentCam].SetActive(true);
                break;
            }
        }
    }
}
