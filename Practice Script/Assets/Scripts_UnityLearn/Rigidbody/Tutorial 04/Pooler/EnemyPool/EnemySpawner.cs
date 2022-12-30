using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;

        int numberOfEnemies = 0;
        int[] numberOfEachEnemis = new int[(int)EnemyType.ENEMYTYPE_MAX_SIZE];

        public int enemyKills = 0;
        public int[] enemyTypeKills = new int[(int)EnemyType.ENEMYTYPE_MAX_SIZE];

        ObjectiveUpdateMessage m_objectiveUpdateMessage;

        private void Awake()
        {
            Instance = this;
        }
        public bool SpawnEnemy(EnemyType _enemyType, Vector3 _position, Vector3 _forward)
        {
            GameObject _enemy = EnemyPooler.Instance.GetEnemy(_enemyType);
            if (_enemy)
            {
                _enemy.transform.position = _position;
                _enemy.transform.rotation = Quaternion.Euler(_forward);
                _enemy.SetActive(true);
                ++numberOfEnemies;
                ++numberOfEachEnemis[(int)_enemyType];
                return true;
            }
            return false;
        }
        public bool DespawnEnemy(int _enemyType, GameObject _enemy)
        {
            if (_enemy)
            {
                _enemy.SetActive(false);
                --numberOfEnemies;
                --numberOfEachEnemis[(int)_enemyType];

                ++enemyKills;
                ++enemyTypeKills[(int)_enemyType];

                m_objectiveUpdateMessage.msgType = ObjectiveMessageType.KillEnemy;
                m_objectiveUpdateMessage.arg0 = (uint)_enemyType;
                ObjectiveManager.Instance.UpdateMessage(m_objectiveUpdateMessage);

                return true;
            }
            return false;
        }
        public void Reset()
        {
            numberOfEnemies = 0;
            enemyKills = 0;

            for (int i = 0; i < (int)EnemyType.ENEMYTYPE_MAX_SIZE; ++i)
            {
                numberOfEachEnemis[i] = 0;
                enemyTypeKills[i] = 0;
            }
            EnemyPooler.Instance.InactiveAll();
        }
    }
}