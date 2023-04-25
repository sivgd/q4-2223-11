using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class CloseRangeUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public MeleeGruntEnemy MG;

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        MG = gameObject.GetComponent<MeleeGruntEnemy>();
        AIManager.Instance.CUnits.Add(this);
    }
    void Update()
    {
        if (MG.currentHealth <= 0)
        {
            AIManager.Instance.CUnits.Remove(this);
        }
    }

    public void MoveTo(Vector3 Position)
    {
        agent.SetDestination(Position);
    }
}
