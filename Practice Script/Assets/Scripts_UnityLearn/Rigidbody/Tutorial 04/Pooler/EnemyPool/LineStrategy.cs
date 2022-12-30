using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum AttackLineType
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM,
        MAX_SIZE,
    }
    public class LineStrategy : ICompositeStrategy
    {
        AttackLineType m_eAttackLineType = AttackLineType.LEFT;
        EnemyType m_eEnemyType = EnemyType.ENEMYTYPE_BOMBING;
        float m_guidePosition = 40f;

        public void Composite()
        {
            switch (m_eAttackLineType)
            {
                case AttackLineType.LEFT:
                    CompositeAttackLineLeft();
                    break;
                case AttackLineType.RIGHT:
                    CompositeAttackLineRight();
                    break;
                case AttackLineType.TOP:
                    CompositeAttackLineTop();
                    break;
                case AttackLineType.BOTTOM:
                    CompositeAttackLineBottom();
                    break;
            }
        }
        public void SetAttackType(AttackLineType _eLineType, EnemyType _enemyType)
        {
            m_eAttackLineType = _eLineType;
            m_eEnemyType = _enemyType;
        }

        void CompositeAttackLineLeft()
        {
            for(int i = 0; i < 17; ++i)
            {
                EnemySpawner.Instance.SpawnEnemy(m_eEnemyType, new Vector3(-40f, 0, 40f - i * 5f), Vector3.right);
            }
        }

        void CompositeAttackLineRight()
        {
            for (int i = 0; i < 17; ++i)
            {
                EnemySpawner.Instance.SpawnEnemy(m_eEnemyType, new Vector3(40f, 0, 40f - i * 5f), -Vector3.right);
            }
        }

        void CompositeAttackLineTop()
        {
            for (int i = 0; i < 17; ++i)
            {
                EnemySpawner.Instance.SpawnEnemy(m_eEnemyType, new Vector3(40f -i * 5f, 0, 40f), -Vector3.forward);
            }
        }

        void CompositeAttackLineBottom()
        {
            for (int i = 0; i < 17; ++i)
            {
                EnemySpawner.Instance.SpawnEnemy(m_eEnemyType, new Vector3(40f - i * 5f, 0, -40f), Vector3.forward);
            }
        }


    }
}