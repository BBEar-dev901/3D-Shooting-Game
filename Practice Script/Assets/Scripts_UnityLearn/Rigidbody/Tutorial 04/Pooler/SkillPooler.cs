using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum CannonSkillType
    {
        CANNON_SKILL_BULLET,
        CANNON_SKILL_LASER_GUN,
        CANNON_SKILL_MAGIC_RUNIC,
        CANNON_SKILL_MAX_SIZE,
    }
    public enum EnemySkillType
    {
        ENEMY_SKILL_BOMBING,
        ENEMY_SKILL_WIZ,
        ENEMY_SKILL_BULLET,
        ENEMY_SKILL_AXE,
        ENEMY_SKILL_POISION,
        ENEMY_SKILL_MAX_SIZE,
    }
    public class SkillPooler : MonoBehaviour
    {
        public static SkillPooler Instance;

        [Header("Cannon Skill Damage Object")]
        [SerializeField] GameObject skillBullet;
        [SerializeField] int countBullet;
        [SerializeField] GameObject skillLaserGun;
        [SerializeField] int countLaserGun;
        [SerializeField] GameObject skillMagicRunic;
        [SerializeField] int countMagicRunic;

        [Header("Enemy Skill Damage Object")]
        [SerializeField] GameObject skillBombingBomb;
        [SerializeField] int countBombingBomb;
        [SerializeField] GameObject skillWizHit;
        [SerializeField] int countWizHit;
        [SerializeField] GameObject skillEnemyBullet;
        [SerializeField] int countEnemyBullet;
        [SerializeField] GameObject skillAxe;
        [SerializeField] int countAxe;
        [SerializeField] GameObject skillPoision;
        [SerializeField] int countPoision;

        List<GameObject>[] cannonSkillPool = new List<GameObject>[(int)CannonSkillType.CANNON_SKILL_MAX_SIZE];
        List<GameObject>[] enemySkillPool = new List<GameObject>[(int)EnemySkillType.ENEMY_SKILL_MAX_SIZE];

        void Awake()
        {
            Instance = this;

            IntializeCannonSkillObject();
            IntializeEnemySkillObject();
        }

        void IntializeCannonSkillObject()
        {
            cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_BULLET] = new List<GameObject>();
            cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_LASER_GUN] = new List<GameObject>();
            cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_MAGIC_RUNIC] = new List<GameObject>();

            for (int i = 0; i < countBullet; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillBullet);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_BULLET].Add(_skill);
            }

            for (int i = 0; i < countLaserGun; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillLaserGun);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_LASER_GUN].Add(_skill);
            }
            for (int i = 0; i < countWizHit; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillMagicRunic);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                cannonSkillPool[(int)CannonSkillType.CANNON_SKILL_MAGIC_RUNIC].Add(_skill);
            }
        }

        void IntializeEnemySkillObject()
        {
            enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_BOMBING] = new List<GameObject>();
            enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_WIZ] = new List<GameObject>();
            enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_BULLET] = new List<GameObject>();
            enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_AXE] = new List<GameObject>();
            enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_POISION] = new List<GameObject>();

            for (int i = 0; i < countBombingBomb; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillBombingBomb);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_BOMBING].Add(_skill);
            }
            for (int i = 0; i < countWizHit; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillWizHit);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_WIZ].Add(_skill);
            }
            for (int i = 0; i < countEnemyBullet; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillEnemyBullet);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_BULLET].Add(_skill);
            }
            for (int i = 0; i < countAxe; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillAxe);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_AXE].Add(_skill);
            }
            for (int i = 0; i < countPoision; ++i)
            {
                GameObject _skill = (GameObject)Instantiate(skillPoision);
                _skill.SetActive(false);
                _skill.transform.SetParent(transform);
                enemySkillPool[(int)EnemySkillType.ENEMY_SKILL_POISION].Add(_skill);
            }

        }
        public GameObject GetCannonSkill(CannonSkillType _effectType)
        {
            for (int i = 0; i < cannonSkillPool[(int)_effectType].Count; ++i)
            {
                if (!cannonSkillPool[(int)_effectType][i].activeInHierarchy)
                {
                    return cannonSkillPool[(int)_effectType][i];
                }
            }
            return null;
        }
        public GameObject GetEnemySkill(EnemySkillType _effectType)
        {
            for (int i = 0; i < enemySkillPool[(int)_effectType].Count; ++i)
            {
                if (!enemySkillPool[(int)_effectType][i].activeInHierarchy)
                {
                    return enemySkillPool[(int)_effectType][i];
                }
            }
            return null;
        }
    }
}