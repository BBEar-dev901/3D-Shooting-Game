using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patroller Info", menuName = "ScriptableObjects/PatrollerInfo", order = 1)]
public class PatrollerInfo : ScriptableObject
{
    
    public Vector3[] pathNode;
    public float patrolSpeed = 2f;
    public float checkingTime = 1f;
    public float towardFactor = Mathf.PI;

    public float pursuerDistance = 10f;
    public float stoppingDistance = 1.5f;
    public float moveFactor = 5f;
    public float P_towardFactor = Mathf.PI * 2;
    public float fieldOfVision = Mathf.PI * 0.5f;

}
