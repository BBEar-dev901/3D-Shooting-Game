using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum EnemyType
    {
        ENEMYTYPE_BOMBING,
        ENEMYTYPE_WIZ,
        ENEMYTYPE_BULLET,
        ENEMYTYPE_AXE,
        ENEMYTYPE_POISION,
        ENEMYTYPE_MAX_SIZE,
    }
    public class EnemyPooler : MonoBehaviour
    {
        public static EnemyPooler Instance;

        [Header("Enemy Bombing")]
        [SerializeField] GameObject enemyBombing;
        [SerializeField] int countEnemyBombingPool;
        [Header("Enemy Wiz")]
        [SerializeField] GameObject enemyWiz;
        [SerializeField] int countEnemyWizPool;
        [Header("Enemy Bullet")]
        [SerializeField] GameObject enemyBullet;
        [SerializeField] int countEnemyBulletPool;
        [Header("Enemy Axe")]
        [SerializeField] GameObject enemyAxe;
        [SerializeField] int countEnemyAxePool;
        [Header("Enemy Poision")]
        [SerializeField] GameObject enemyPoision;
        [SerializeField] int countEnemyPoisionPool;

        Transform attackTarget;
        Transform followTarget;

        List<GameObject>[] enemyPool = new List<GameObject>[(int)EnemyType.ENEMYTYPE_MAX_SIZE];

        private void Awake()
        {
            Instance = this;

            followTarget = GameObject.Find("Cannon").transform;
            attackTarget = GameObject.Find("Target Position").transform;

            enemyPool[(int)EnemyType.ENEMYTYPE_BOMBING] = new List<GameObject>();
            enemyPool[(int)EnemyType.ENEMYTYPE_WIZ] = new List<GameObject>();
            enemyPool[(int)EnemyType.ENEMYTYPE_BULLET] = new List<GameObject>();
            enemyPool[(int)EnemyType.ENEMYTYPE_AXE] = new List<GameObject>();
            enemyPool[(int)EnemyType.ENEMYTYPE_POISION] = new List<GameObject>();

            for (int i = 0; i < countEnemyBombingPool; ++i)
            {
                GameObject _enemy = (GameObject)Instantiate(enemyBombing);
                _enemy.SetActive(false);
                _enemy.transform.SetParent(transform);
                _enemy.GetComponent<EnemyBombing>().targetPlayer = followTarget;
                _enemy.GetComponent<EnemyStat>().enemyType = EnemyType.ENEMYTYPE_BOMBING;
                enemyPool[(int)EnemyType.ENEMYTYPE_BOMBING].Add(_enemy);
            }
            for (int i = 0; i < countEnemyWizPool; ++i)
            {
                GameObject _enemy = (GameObject)Instantiate(enemyWiz);
                _enemy.SetActive(false);
                _enemy.transform.SetParent(transform);
                _enemy.GetComponent<EnemyWiz>().targetPlayer = followTarget;
                _enemy.GetComponent<EnemyWiz>().shootPosition = attackTarget;
                _enemy.GetComponent<EnemyStat>().enemyType = EnemyType.ENEMYTYPE_WIZ;
                enemyPool[(int)EnemyType.ENEMYTYPE_WIZ].Add(_enemy);
            }
            for (int i = 0; i < countEnemyBulletPool; ++i)
            {
                GameObject _enemy = (GameObject)Instantiate(enemyBullet);
                _enemy.SetActive(false);
                _enemy.transform.SetParent(transform);
                _enemy.GetComponent<EnemyBulletController>().targetPlayer = followTarget;
                _enemy.GetComponent<EnemyStat>().enemyType = EnemyType.ENEMYTYPE_BULLET;
                enemyPool[(int)EnemyType.ENEMYTYPE_BULLET].Add(_enemy);
            }
            for (int i = 0; i < countEnemyAxePool; ++i)
            {
                GameObject _enemy = (GameObject)Instantiate(enemyAxe);
                _enemy.SetActive(false);
                _enemy.transform.SetParent(transform);
                _enemy.GetComponent<EnemyAxe>().targetPlayer = followTarget;
                _enemy.GetComponent<EnemyStat>().enemyType = EnemyType.ENEMYTYPE_AXE;
                enemyPool[(int)EnemyType.ENEMYTYPE_AXE].Add(_enemy);
            }
            for (int i = 0; i < countEnemyPoisionPool; ++i)
            {
                GameObject _enemy = (GameObject)Instantiate(enemyPoision);
                _enemy.SetActive(false);
                _enemy.transform.SetParent(transform);
                _enemy.GetComponent<EnemyPoision>().targetPlayer = followTarget;
                _enemy.GetComponent<EnemyStat>().enemyType = EnemyType.ENEMYTYPE_POISION;
                enemyPool[(int)EnemyType.ENEMYTYPE_POISION].Add(_enemy);
            }
        }
        public GameObject GetEnemy(EnemyType _enemyType)
        {
            if (enemyPool[(int)_enemyType] == null)
            {
                return null;
            }
            for (int i = 0; i < enemyPool[(int)_enemyType].Count; ++i)
            {
                if (!enemyPool[(int)_enemyType][i].activeInHierarchy)
                {
                    return enemyPool[(int)_enemyType][i];
                }
            }
            return null;
        }
        public void InactiveAll()
        {
            for (int i = 0; i < (int)EnemyType.ENEMYTYPE_MAX_SIZE; ++i)
            {
                for(int j = 0; j < enemyPool[i].Count; ++j)
                {
                    enemyPool[i][j].SetActive(false);
                }
            }
        }

    }
}