using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateMouse : MonoBehaviour
{

    public bool mouseOn;

    // Start is called before the first frame update
    void Start()
    {
        if (mouseOn == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
