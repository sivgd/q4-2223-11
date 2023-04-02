using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Normal Attack")]
public class PlayerAttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage;
}
