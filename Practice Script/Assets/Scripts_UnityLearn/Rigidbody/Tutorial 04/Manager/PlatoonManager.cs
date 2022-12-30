using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class PlatoonManager : MonoBehaviour
    {
        public static PlatoonManager Instance;

        float expireTimer;
        float attackIntervalTime = 5f;
        bool IsRush = false;

        LineStrategy m_LineStrategy = new LineStrategy();
        SurroundStrategy m_SurroundStrategy;
        StageManager m_StageManager;

        private void Awake()
        {
            Instance = this;

            m_StageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
            Transform _tr = GameObject.Find("Cannon").transform;
            m_SurroundStrategy = new SurroundStrategy(_tr);
        }

        private void Update()
        {
            if (!IsRush)
            {
                return;
            }
            expireTimer += Time.deltaTime;
            if(expireTimer > attackIntervalTime)
            {
                expireTimer = 0;
            }
            else
            {
                return;
            }

            int randomValue = Random.Range(0, 10);
            int _type = 0;

            switch (randomValue)
            {
                case 0:
                    m_LineStrategy.SetAttackType(AttackLineType.LEFT, EnemyType.ENEMYTYPE_WIZ);
                    break;
                case 1:
                    m_LineStrategy.SetAttackType(AttackLineType.RIGHT, EnemyType.ENEMYTYPE_WIZ);
                    break;
                case 2:
                    m_LineStrategy.SetAttackType(AttackLineType.TOP, EnemyType.ENEMYTYPE_WIZ);
                    break;
                case 3:
                    m_LineStrategy.SetAttackType(AttackLineType.BOTTOM, EnemyType.ENEMYTYPE_WIZ);
                    break;
                case 4:
                    m_LineStrategy.SetAttackType(AttackLineType.LEFT, EnemyType.ENEMYTYPE_BOMBING);
                    break;
                case 5:
                    m_LineStrategy.SetAttackType(AttackLineType.RIGHT, EnemyType.ENEMYTYPE_BOMBING);
                    break;
                case 6:
                    m_LineStrategy.SetAttackType(AttackLineType.TOP, EnemyType.ENEMYTYPE_BOMBING);
                    break;
                case 7:
                    m_LineStrategy.SetAttackType(AttackLineType.BOTTOM, EnemyType.ENEMYTYPE_BOMBING);
                    break;
                case 8:
                    m_SurroundStrategy.SetAttackType(EnemyType.ENEMYTYPE_BOMBING);
                    _type = 1;
                    break;
                case 9:
                    m_SurroundStrategy.SetAttackType(EnemyType.ENEMYTYPE_WIZ);
                    _type = 1;
                    break;
            }
            if(_type == 0)
            {
                m_LineStrategy.Composite();
            }
            else
            {
                m_SurroundStrategy.Composite();
            }

        }

        public void CommandRush(bool _IsRush = true)
        {
            expireTimer = attackIntervalTime;
            IsRush = _IsRush;
        }

        public void Withdraw()
        {
            IsRush = false;
            EnemySpawner.Instance.Reset();
        }
    }
}