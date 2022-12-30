using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class WorldspaceHealthBar : MonoBehaviour
    {
        public EnemyStat m_enemyStat;
        [SerializeField] protected Transform m_healthBarPivot;
        [SerializeField] protected Image m_healthBarImage;

        private void Update()
        {
            m_healthBarImage.fillAmount = m_enemyStat.m_currentHp / m_enemyStat.m_maxHp;
            m_healthBarPivot.forward = Camera.main.transform.forward;
            m_healthBarPivot.gameObject.SetActive(m_healthBarImage.fillAmount != 1);
        }


    }
}