using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum ObjectiveMessageType
    {
        KillEnemy,
        PickupItems,
    }
    public struct ObjectiveUpdateMessage
    {
        public ObjectiveMessageType msgType;
        public uint arg0;
        public uint arg1;
    }
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;

        List<Objective> objectives = new List<Objective>();
        StageManager m_stageManager;

        public bool _resetObjective = false;

        private void Awake()
        {
            Instance = this;
            m_stageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
        }
        public void AddObjective(Objective _obj)
        {
            objectives.Add(_obj);
        }
        private void Update()
        {
            if(objectives.Count < 1)
            {
                return;
            }
            foreach(var objective in objectives)
            {
                if (objective.IsAchieved() && _resetObjective == false)
                {
                    objective.Completed();
                    Destroy(objective.gameObject);
                    objectives.Remove(objective);

                    if(objectives.Count == 0)
                    {
                        m_stageManager.OnStageCleared();
                    }

                    break;
                }
            }
        }
        public void UpdateMessage(ObjectiveUpdateMessage _msg)
        {
            switch (_msg.msgType)
            {
                case ObjectiveMessageType.KillEnemy:
                    KillEnemyMessage(_msg);
                    break;
                case ObjectiveMessageType.PickupItems:
                    //픽업 아이템 만들었을때
                    break;
            }
        }
        void KillEnemyMessage(ObjectiveUpdateMessage _msg)
        {
            foreach(var objective in objectives)
            {
                if(objective.objectiveType == ObjectiveType.KillEnemy)
                {
                    objective.Updated();
                }
            }
        }
        public void ResetGameObjective()
        {
            _resetObjective = true;
            foreach (var objective in objectives)
            {
                Destroy(objective.gameObject);
            }

            objectives.Clear();

            if (objectives.Count == 0)
            {
                _resetObjective = false;
            }
        }

    }
}