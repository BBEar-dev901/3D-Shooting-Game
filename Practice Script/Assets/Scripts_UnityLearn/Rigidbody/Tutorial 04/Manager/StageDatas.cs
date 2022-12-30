using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lecture.RigidBody.Tutorial04
{
    [System.Serializable] public struct StageData
    {
        public uint stageId;
        public Objective[] objectives;
    }
    public class StageDatas : MonoBehaviour
    {
        public StageData[] Stages;
    }
}