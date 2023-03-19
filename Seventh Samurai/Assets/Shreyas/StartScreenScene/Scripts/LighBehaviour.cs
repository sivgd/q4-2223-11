using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighBehaviour : MonoBehaviour
{
    Light redLight;
    // Start is called before the first frame update
    void Start()
    {
        redLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        redLight.intensity = Mathf.PingPong(Time.time, 3);
    }
}
