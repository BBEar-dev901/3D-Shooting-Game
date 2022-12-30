using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class WorldspaceBombHealthBar : WorldspaceHealthBar
    {
        void Update()
        {
            m_healthBarImage.fillAmount = m_enemyStat.m_currentHp / m_enemyStat.m_maxHp;
            m_healthBarPivot.transform.position = new Vector3(transform.GetChild(0).position.x, 2.5f, transform.GetChild(0).position.z);
            m_healthBarPivot.forward = Camera.main.transform.forward;
            m_healthBarPivot.gameObject.SetActive(m_healthBarImage.fillAmount != 1);
        }
    }
}