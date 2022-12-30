using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class HealBuffPickup : Pickup
    {
        [SerializeField] float m_buffTime = 0.1f;
        [SerializeField] float m_buffFactor = 30f;

        protected override void OnPicked(CharacterStat _stat)
        {
            //_stat.SetBuffHeal(m_buffFactor);

            BuffData buffData;
            buffData.type = BuffType.HEAL;
            buffData.time = m_buffTime;
            buffData.value = m_buffFactor;
            BuffManager.AddBuffData(buffData);
        }
    }
}