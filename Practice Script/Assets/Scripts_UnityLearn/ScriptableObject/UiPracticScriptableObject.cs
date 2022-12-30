using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    [System.Serializable]
    public class RewardList
    {
        public Sprite RewardIcon;
        public int rewardCount;
    }
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UiPracticScriptableObject", order = 12)]
    public class UiPracticScriptableObject : ScriptableObject
    {
        public Sprite NPCIcon;
        public string NPCName;
        public string[] entranceMassages;
        public string[] exitMessages;
        public RewardList[] rewardList;
    }
}