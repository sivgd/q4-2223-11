using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLock : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    // Start is called before the first frame update
    void Start()
    {
        door1.SetActive(false);
        door2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider player)
    {
        door1.SetActive(true);
        door2.SetActive(true);
    }
}
