using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    // 이펙트 상태 분류
    public enum EffectType
    {
        EFFECTTYPE_BULLETEXPLOSION,
        EFFECTTYPE_BLOODEXPLOSION,
        EFFECTTYPE_MAGICRUNIC,
        EFFECTTYPE_LASER_GUN,
        EFFECTTYPE_ENEMYBOMBING_BOMB,
        EFFECTTYPE_ENEMY_WIZ_HIT,
        EFFECTTYPE_ENEMY_BULLET,
        EFFECTTYPE_EMPTY_BULLET,
        EFFECTTYPE_ENEMY_AXE,
        EFFECTTYPE_ENEMY_AXE_READY,
        EFFECTTYPE_ENEMY_POISION_READY,
        EFFECTTYPE_ENEMY_POISION,
        EFFECTTYPE_MAX_SIZE,
    }
    public class EffectPooler : MonoBehaviour
    {
        // 컴포넌트에 나타낼 때 헤더로 분류하기
        [Header("Bullet Explosion Effect")]
        // 받아올 이펙트와 이펙트 개수
        [SerializeField] GameObject bulletFx;
        [SerializeField] int countBulletPool;

        [Header("Enemy Blood Effect")]
        [SerializeField] GameObject bloodFx;
        [SerializeField] int countBloodPool;

        [Header("Magic Runic Effect")]
        [SerializeField] GameObject magicRunicFx;
        [SerializeField] int countMagicRunicPool;
        
        [Header("Laser Gun Effect")]
        [SerializeField] GameObject laserGunFx;
        [SerializeField] int countLaserGunPool;

        [Header("Enemy Bombing Bomb Effect")]
        [SerializeField] GameObject enemybombingBombFx;
        [SerializeField] int countEnemyBombingBombPool;

        [Header("Enemy Wiz Hit Effect")]
        [SerializeField] GameObject enemyWizHitFx;
        [SerializeField] int countEnemyWizHitPool;
        
        [Header("Enemy Bullet Effect")]
        [SerializeField] GameObject enemyBulletFx;
        [SerializeField] int countEnemyBulletPool;
        
        [Header("Empty Bullet Effect")]
        [SerializeField] GameObject emptyBulletFx;
        [SerializeField] int countEmptyBulletPool;

        [Header("Enemy Axe Effect")]
        [SerializeField] GameObject enemyAxeFx;
        [SerializeField] int countEnemyAxePool;

        [Header("Enemy Axe Ready Effect")]
        [SerializeField] GameObject axeReadyFx;
        [SerializeField] int countAxeReadyPool;

        [Header("Enemy Poision Ready Effect")]
        [SerializeField] GameObject poisionReadyFx;
        [SerializeField] int countPoisionReadyPool;

        [Header("Enemy PoisionShoot Effect")]
        [SerializeField] GameObject poisionFx;
        [SerializeField] int countPoisionPool;

        // 이펙트를 받아와서 저장해 둘 리스트를 형식으로 가지는 배열
        List<GameObject>[] effectPool = new List<GameObject>[(int)EffectType.EFFECTTYPE_MAX_SIZE];

        public static EffectPooler Instance;

        private void Awake()
        {
            Instance = this;
            // 배열에 리스트로 이펙트 저장하기
            effectPool[(int)EffectType.EFFECTTYPE_BULLETEXPLOSION] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_BLOODEXPLOSION] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_MAGICRUNIC] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_LASER_GUN] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMYBOMBING_BOMB] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_WIZ_HIT] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_BULLET] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_EMPTY_BULLET] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_AXE] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_AXE_READY] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_POISION_READY] = new List<GameObject>();
            effectPool[(int)EffectType.EFFECTTYPE_ENEMY_POISION] = new List<GameObject>();

            for (int i = 0; i < countBulletPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(bulletFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(0));
                effectPool[(int)EffectType.EFFECTTYPE_BULLETEXPLOSION].Add(_fx);
            }
            for (int i = 0; i < countBloodPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(bloodFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(1));
                effectPool[(int)EffectType.EFFECTTYPE_BLOODEXPLOSION].Add(_fx);
            }
            for (int i = 0; i < countMagicRunicPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(magicRunicFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(2));
                effectPool[(int)EffectType.EFFECTTYPE_MAGICRUNIC].Add(_fx);
            }
            for (int i = 0; i < countLaserGunPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(laserGunFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(3));
                effectPool[(int)EffectType.EFFECTTYPE_LASER_GUN].Add(_fx);
            }
            for (int i = 0; i < countEnemyBombingBombPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(enemybombingBombFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(4));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMYBOMBING_BOMB].Add(_fx);
            }
            for (int i = 0; i < countEnemyWizHitPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(enemyWizHitFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(5));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_WIZ_HIT].Add(_fx);
            }
            for (int i = 0; i < countEnemyBulletPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(enemyBulletFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(6));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_BULLET].Add(_fx);
            }
            for (int i = 0; i < countEmptyBulletPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(emptyBulletFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(7));
                effectPool[(int)EffectType.EFFECTTYPE_EMPTY_BULLET].Add(_fx);
            }
            for (int i = 0; i < countEnemyAxePool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(enemyAxeFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(8));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_AXE].Add(_fx);
            }
            for (int i = 0; i < countAxeReadyPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(axeReadyFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(9));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_AXE_READY].Add(_fx);
            }
            for (int i = 0; i < countPoisionReadyPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(poisionReadyFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(10));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_POISION_READY].Add(_fx);
            }
            for (int i = 0; i < countPoisionPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(poisionFx);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform.GetChild(11));
                effectPool[(int)EffectType.EFFECTTYPE_ENEMY_POISION].Add(_fx);
            }

        }
        public GameObject GetEffect(EffectType _effectType)
        {
            if(effectPool[(int)_effectType] == null)
            {
                return null;
            }
            for(int i = 0; i < effectPool[(int)_effectType].Count; ++i)
            {
                if (!effectPool[(int)_effectType][i].activeInHierarchy)
                {
                    return effectPool[(int)_effectType][i];
                }
            }

            return null;

        }
    }
}

