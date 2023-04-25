using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AIManager : MonoBehaviour
{
    private static AIManager _instance;
    public static AIManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public Transform target;

    [Header("Radius")]
    public float CRadius = 0.5f;
    //public float MRadius = 0.5f;


    [Header("Unit Types")]
    public List<CloseRangeUnit> CUnits = new List<CloseRangeUnit>();
    //public List<MandrillUnit> MUnits = new List<MandrillUnit>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if (gameObject != null)
        {
            MakeAgentsCircleTarget();
        }
    }

    private void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < CUnits.Count; i++)
        {
            CUnits[i].MoveTo(new Vector3(
                target.position.x + CRadius * Mathf.Cos(2 * Mathf.PI * i / CUnits.Count),
                target.position.y,
                target.position.z + CRadius * Mathf.Sin(2 * Mathf.PI * i / CUnits.Count)));
        }

        //for (int i = 0; i < KUnits.Count; i++)
        //{
            //KUnits[i].MoveTo(new Vector3(
                //target.position.x + KRadius * Mathf.Cos(2 * Mathf.PI * i / KUnits.Count),
                //target.position.y,
                //target.position.z + KRadius * Mathf.Sin(2 * Mathf.PI * i / KUnits.Count)));
        //}
    }

}