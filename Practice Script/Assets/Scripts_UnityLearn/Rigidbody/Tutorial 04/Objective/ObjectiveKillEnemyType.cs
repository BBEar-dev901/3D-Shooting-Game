using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class ObjectiveKillEnemyType : Objective
    {
        public int killsToCompleteObjective = 50;
        [SerializeField] EnemyType enemyType;
        public override bool IsAchieved()
        {
            return EnemySpawner.Instance.enemyTypeKills[(int)enemyType] >= killsToCompleteObjective;
        }
        public override bool Completed()
        {
            Debug.Log("Kill Enemy Type Obejective Completed");

            return true;
        }
        public override void Updated()
        {
            DisplayObjectiveManager.Instance.UpdateObjective(
                m_displayObjectiveLayout, "( " + EnemySpawner.Instance.enemyTypeKills[(int)enemyType] + " / " + killsToCompleteObjective + " )");
        }
        protected void OnEnable()
        {
            objectiveType = ObjectiveType.KillEnemy;
            StartCoroutine(WaitAndDisplay(0.5f));
        }
        IEnumerator WaitAndDisplay(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            m_displayMessage.message = Title;
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);

            m_displayObjective._TitleText = Title;
            m_displayObjective._DescriptionText = Description;
            m_displayObjective._CounterText = "( 0 / " + killsToCompleteObjective + " )";
            m_displayObjectiveLayout = DisplayObjectiveManager.Instance.RegisterObjective(m_displayObjective);
        }
        protected void OnDisable()
        {
            if (ObjectiveManager.Instance._resetObjective != false)
            {
                return;
            }
            else
            {
                m_displayMessage.message = Title + " ¿Ï·á";
                DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);
                DisplayObjectiveManager.Instance.UpdateObjective(m_displayObjectiveLayout,
                                                "( " + EnemySpawner.Instance.enemyTypeKills[(int)enemyType] +
                                                " / " + killsToCompleteObjective + " )", true);
            }
        }
    }
}