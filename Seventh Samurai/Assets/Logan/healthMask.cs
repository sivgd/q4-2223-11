using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthMask : MonoBehaviour
{
    
    public void moveMask(float currentVal, float maxVal)
    {
        float percentToMove = (float)currentVal / (float)maxVal;
        transform.localPosition = new Vector3((percentToMove - 1) * 75, 0, 0);
    }

    public void moveEnemyMask(float currentVal, float maxVal)
    {
        float percentToMove = (float)currentVal / (float)maxVal;
        transform.localPosition = new Vector3((percentToMove - 1) * 201, 0, 0);
    }

}
