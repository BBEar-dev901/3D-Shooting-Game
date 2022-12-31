using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    NormalCamera,
    CombatCamera,
    MaxSize,
}
[CreateAssetMenu(fileName = "CameraDataSet", menuName = "ScriptableObjects/Camera Data Setting", order = 5)]
public class CameraDataSet : ScriptableObject
{
    [System.Serializable]
    public class CameraSet
    {
        public CameraType cameraType;
        public Vector3 cameraOffset;
        public float moveDamping;
        public float rotationDamping;
    }

    public CameraSet[] cameraSets;
}