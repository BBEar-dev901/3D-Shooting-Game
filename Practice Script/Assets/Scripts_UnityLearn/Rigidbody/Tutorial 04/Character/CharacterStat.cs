using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum CharacterHpState
    {
        FULLHP, //100% ~ 80%
        HALFHP, //79% ~ 50%
        LESSHP, //49% ~ 30%
        LITTLEHP, //29% ~ 0%
        MAX_SIZE,
    }
    public class CharacterStat : MonoBehaviour
    {

        public CharacterHpState m_CharacterHpState = CharacterHpState.FULLHP;
        public float MaxHp = 1500f;
        public float CurrentHp;
        StageManager _stageManager;
        CannonDamageBar m_damageBar;
        float _getDamage;
        [HideInInspector]public float _SumDamage = 0;

        const float defaultOffenseValue = 1.0f;
        const float defaultDefenceValue = 0.0f;

        public static float BuffOffense = defaultOffenseValue;
        public static float BuffDefense = defaultDefenceValue;

        int statenumber;


        void OnEnable()
        {
            CurrentHp = MaxHp;
            _stageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
            PlayerDamagePooler.Instance.InactiveDamageBar();
            _SumDamage = 0f;
            statenumber = (int)CharacterHpState.FULLHP;
        }

        public void AddDamage(float _damage)
        {
            CurrentHp -= _damage * (1.0f - BuffDefense);

            _getDamage = _damage * (1.0f - BuffDefense);

            if (CurrentHp < 0f)
            {
                CurrentHp = 0f;
                _SumDamage = 0f;
                gameObject.SetActive(false);
                
                _stageManager.OnStageFailed();
            }

            GameObject _DamageBar = PlayerDamagePooler.Instance.GetDamageCanvas();
            if(_DamageBar != null)
            {
                m_damageBar = _DamageBar.GetComponent<CannonDamageBar>();

                _DamageBar.transform.SetParent(transform.GetChild(2));
                _DamageBar.transform.position = transform.GetChild(2).position;
                _DamageBar.transform.forward = Camera.main.transform.forward;
                _DamageBar.SetActive(true);

                m_damageBar.TextPlay(GetDamage());
            }
            _SumDamage += _damage;
        }
        private void Update()
        {
            if (CurrentHp == 1500f && CurrentHp > 1000f)
            {
                m_CharacterHpState = CharacterHpState.FULLHP;
                statenumber = (int)CharacterHpState.FULLHP;
            }
            else if (CurrentHp <= 1000f && CurrentHp > 700f)
            {
                m_CharacterHpState = CharacterHpState.HALFHP;
                statenumber = (int)CharacterHpState.HALFHP;
            }
            else if (CurrentHp <= 700f && CurrentHp > 300f)
            {
                m_CharacterHpState = CharacterHpState.LESSHP;
                statenumber = (int)CharacterHpState.LESSHP;
            }
            else if (CurrentHp < 300f)
            {
                m_CharacterHpState = CharacterHpState.LITTLEHP;
                statenumber = (int)CharacterHpState.LITTLEHP;
            }
        }

        public float GetDamage()
        {
            return _getDamage;
        }

        public float GetSumDamage()
        {
            return _SumDamage;
        }
        public int GetHpState()
        {
            return statenumber;
        }

        public void SetBuffDefence(float _value)
        {
            BuffDefense = _value;
        }
        public void SetBuffOffense(float _value)
        {
            BuffOffense = _value;
        }
        public void DoHeal(float _value)
        {
            CurrentHp = CurrentHp + _value;
        }
        public void ActiveBuff(BuffType type, float _value)
        {
            switch (type)
            {
                case BuffType.HEAL:
                    DoHeal(_value);
                    break;
                case BuffType.DEFENCE:
                    BuffDefense = _value;
                    break;
                case BuffType.OFFENSE:
                    BuffOffense = _value;
                    break;
            }
        }
        public void InactiveBuff(BuffType type)
        {
            switch (type)
            {
                case BuffType.DEFENCE:
                    BuffDefense = defaultDefenceValue;
                    break;
                case BuffType.OFFENSE:
                    BuffOffense = defaultOffenseValue;
                    break;
            }
        }

    }
}