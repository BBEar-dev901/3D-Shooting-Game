using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class PickupSpawner : MonoBehaviour
    {
        [Header("Heal Buff")]
        [SerializeField] GameObject m_Healitem;
        [SerializeField] int m_countHealitem;
        [Header("Defence Buff")]
        [SerializeField] GameObject m_Defenceitem;
        [SerializeField] int m_countDefenceitem;
        [Header("Offense Buff")]
        [SerializeField] GameObject m_Offenseitem;
        [SerializeField] int m_countOffenseitem;

        List<GameObject>[] BuffPool = new List<GameObject>[(int)BuffType.MAX];
        CharacterStat m_CharacterStat;
        StageManager m_StageManager;
        Transform targetPlayer;

        JsonPickupDataManager jsondata;
        List<SpawnData>[] jsonpickupdata;
        public static PickupSpawner Instance;

        
        float m_SpawnTimer;
        float m_damage;
        float m_sDamage = 100f;
        float m_SpawnTime = 10f;
        float m_distande = 7f;

        bool m_randomSpawn = false;

        private void Awake()
        {
            Instance = this;

            targetPlayer = GameObject.Find("Cannon").transform;

            m_CharacterStat = targetPlayer.GetComponent<CharacterStat>();
            m_StageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
            jsondata = GameObject.Find("jsondata").GetComponent<JsonPickupDataManager>();
            jsondata.DataLoad();

            BuffPool[(int)BuffType.HEAL] = new List<GameObject>();
            BuffPool[(int)BuffType.DEFENCE] = new List<GameObject>();
            BuffPool[(int)BuffType.OFFENSE] = new List<GameObject>();

            for (int i = 0; i < m_countHealitem; i++)
            {
                GameObject _fx = (GameObject)Instantiate(m_Healitem);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform);
                BuffPool[(int)BuffType.HEAL].Add(_fx);
            }
            for (int i = 0; i < m_countDefenceitem; i++)
            {
                GameObject _fx = (GameObject)Instantiate(m_Defenceitem);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform);
                BuffPool[(int)BuffType.DEFENCE].Add(_fx);
            }
            for (int i = 0; i < m_countOffenseitem; i++)
            {
                GameObject _fx = (GameObject)Instantiate(m_Offenseitem);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform);
                BuffPool[(int)BuffType.OFFENSE].Add(_fx);
            }
        }
        private void Update()
        {
            if (m_randomSpawn == true)
            {
                if (StageManager.playstate == PlayState.Run)
                {
                    m_SpawnTimer += Time.deltaTime;
                    if (m_SpawnTimer > m_SpawnTime)
                    {
                        m_SpawnTimer = 0;
                        RandomSpawn();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    m_SpawnTimer = 0;
                }
            }
            else
            {
                if (StageManager.playstate == PlayState.Run)
                {
                    if (m_CharacterStat.GetSumDamage() < m_sDamage)
                    {
                        return;
                    }
                    else
                    {
                        UnRandomSpawn(m_CharacterStat.GetHpState());
                        m_CharacterStat._SumDamage = 0f;
                    }
                }
            }
        }
        void RandomSpawn()
        {
            // 랜덤 스폰
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    // 힐 방어
                    SpawnBuff(BuffType.HEAL, 2);
                    SpawnBuff(BuffType.DEFENCE);
                    break;
                case 1:
                    // 힐 공격
                    SpawnBuff(BuffType.HEAL, 2);
                    SpawnBuff(BuffType.OFFENSE);
                    break;
                case 2:
                    // 방어 공격
                    SpawnBuff(BuffType.DEFENCE, 2);
                    SpawnBuff(BuffType.OFFENSE, 2);
                    break;
                case 3:
                    // 셋 다
                    SpawnBuff(BuffType.HEAL, 2);
                    SpawnBuff(BuffType.DEFENCE, 2);
                    SpawnBuff(BuffType.OFFENSE, 2);
                    break;
            }
        }
        void UnRandomSpawn(int _type)
        {
            // 플레이어 중심으로 생성
            switch (_type)
            {
                case 0:
                    SpawnBuff(BuffType.OFFENSE,2);
                    break;
                case 1:
                    SpawnBuff(BuffType.OFFENSE);
                    SpawnBuff(BuffType.DEFENCE);
                    break;
                case 2:
                    SpawnBuff(BuffType.DEFENCE);
                    SpawnBuff(BuffType.HEAL);
                    break;
                case 3:
                    SpawnBuff(BuffType.OFFENSE);
                    SpawnBuff(BuffType.DEFENCE);
                    SpawnBuff(BuffType.HEAL);
                    break;
            }
        }
        void JsonDataSpawn()
        {
            jsonpickupdata = new List<SpawnData>[m_StageManager.GetmaxStage()];
            // json 파일 데이터 읽어와서 스폰
            for(int i = 0; i < m_StageManager.GetmaxStage(); i++)
            {
                jsonpickupdata[i] = new List<SpawnData>();
            }
            foreach(var item in jsondata.Data)
            {
                jsonpickupdata[item.StageId].Add(item);
            }
        }
        void SpawnjsonBuff()
        {
            // 타이머 돌리기
            // 스테이지 비교하기
            // 좌표에 버프 아이템 생성하기
        }
        void SpawnBuff(BuffType _type, int _count = 1)
        {
            if (m_randomSpawn == true)
            {
                for (int i = 0; i < _count; i++)
                {
                    int _xPosition = Random.Range(-40, 40);
                    int _zPosition = Random.Range(-40, 40);

                    GameObject _obj = GetType(_type);
                    _obj.transform.position = new Vector3(_xPosition, 1, _zPosition);
                    _obj.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < _count; i++)
                {
                    int R_rotation = Random.Range(0, 360);
                    Quaternion _rotation = Quaternion.Euler(0f, R_rotation, 0);
                    Vector3 _direction = _rotation * Vector3.forward;
                    Vector3 _position = targetPlayer.position + _direction * m_distande;
                    if (_position.x > 40f || _position.x < -40f || _position.z > 40f || _position.z < -40f)
                    {
                        continue;
                    }
                    GameObject _obj = GetType(_type);
                    _obj.transform.position = _position;
                    _obj.SetActive(true);
                }
            }
        }
        public GameObject GetType(BuffType _type)
        {
            if (BuffPool[(int)_type] == null)
            {
                return null;
            }
            for (int i = 0; i < BuffPool[(int)_type].Count; ++i)
            {
                if (!BuffPool[(int)_type][i].activeInHierarchy)
                {
                    return BuffPool[(int)_type][i];
                }
            }
            return null;
        }
        public void InactiveBuffAll()
        {
            for (int i = 0; i < (int)BuffType.MAX; ++i)
            {
                for (int j = 0; j < BuffPool[i].Count; ++j)
                {
                    BuffPool[i][j].SetActive(false);
                }
            }
        }

    }
}
