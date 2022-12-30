using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04 {
    public class SurroundStrategy : ICompositeStrategy
    {
        Transform targetPlayer;
        EnemyType m_eEnemyType = EnemyType.ENEMYTYPE_BOMBING;
        float m_distande = 25f;

        public SurroundStrategy(Transform _tr)
        {
            targetPlayer = _tr;
        }
        public void Composite()
        {
            for(int i = 0; i < 18; ++i)
            {
                Quaternion _rotation = Quaternion.Euler(0f, i * 20f, 0);
                Vector3 _direction = _rotation * Vector3.forward;
                Vector3 _position = targetPlayer.position + _direction * m_distande;

                if(_position.x > 40f || _position.x < -40f || _position.z > 40f || _position.z < -40f)
                {
                    continue;
                }
                EnemySpawner.Instance.SpawnEnemy(m_eEnemyType, _position, -_direction);
            }
        }
        public void SetAttackType(EnemyType _enemyType)
        {
            m_eEnemyType = _enemyType;
        }
    }
}