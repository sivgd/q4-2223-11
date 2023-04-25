using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class CloseRangeUnit : MonoBehaviour
{
    public NavMeshAgent agent;
    public MeleeGruntEnemy MG;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        MG = GetComponent<MeleeGruntEnemy>();
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
