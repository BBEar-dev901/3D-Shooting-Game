using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyStat : MonoBehaviour
    {
        public EnemyType enemyType { get; set; }
        public float m_maxHp = 30f;
        public float m_currentHp;
        void OnEnable()
        {
            m_currentHp = m_maxHp;
        }
        public void AddDamage(float _damage)
        {
            m_currentHp -= _damage;
            if(m_currentHp < 0f)
            {
                EnemySpawner.Instance.DespawnEnemy((int)enemyType, gameObject);
            }
        }
    }
}