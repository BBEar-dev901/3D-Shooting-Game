using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class OffenseBuffPickup : Pickup
    {
        [SerializeField] float m_buffTime = 15f;
        [SerializeField] float m_buffFactor = 20f;

        protected override void OnPicked(CharacterStat _stat)
        {
            //_stat.SetBuffOffense(m_buffFactor);

            BuffData buffData;
            buffData.type = BuffType.OFFENSE;
            buffData.time = m_buffTime;
            buffData.value = m_buffFactor;
            BuffManager.AddBuffData(buffData);
        }

    }
}