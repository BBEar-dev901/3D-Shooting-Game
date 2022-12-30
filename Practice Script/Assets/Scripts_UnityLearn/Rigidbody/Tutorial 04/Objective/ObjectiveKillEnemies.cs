using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class ObjectiveKillEnemies : Objective
    {
        public int killsToCompleteObjective = 100;

        public override bool IsAchieved()
        {
            //�Ϸ� ���� �Ǵ�
            return EnemySpawner.Instance.enemyKills >= killsToCompleteObjective;
        }
        public override bool Completed()
        {
            //�Ϸ� ������ Ư���ϰ� �ؾ��� ��
            Debug.Log("Kill Enemies Obejective Completed");

            return true;
        }
        public override void Updated()
        {
            //��� ������Ʈ ����� �Ұ�
            DisplayObjectiveManager.Instance.UpdateObjective(
                m_displayObjectiveLayout, "( " + EnemySpawner.Instance.enemyKills + " / " + killsToCompleteObjective + " )");
        }
        protected void OnEnable()
        {
            objectiveType = ObjectiveType.KillEnemy;
            StartCoroutine(WaitAndDisplay(0.5f));
        }
        IEnumerator WaitAndDisplay(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            m_displayMessage.message = "Eliminate " + killsToCompleteObjective.ToString() + " enemies";
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);

            m_displayObjective._TitleText = Title; 
            m_displayObjective._DescriptionText = Description; 
            m_displayObjective._CounterText = "( 0 / " + killsToCompleteObjective + " )";
            m_displayObjectiveLayout = DisplayObjectiveManager.Instance.RegisterObjective(m_displayObjective); 
        }
        protected void OnDisable()
        {
            if(ObjectiveManager.Instance._resetObjective == false)
            {
                m_displayMessage.message = "Eliminated " + killsToCompleteObjective.ToString() + " enemies";
                DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);
                DisplayObjectiveManager.Instance.UpdateObjective(m_displayObjectiveLayout, "( " + EnemySpawner.Instance.enemyKills + " / " + killsToCompleteObjective + " )", true);
            }
            else
            {
                return;
            }
        }

    }
}