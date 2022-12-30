using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class DamagePoision : SkillDamage
    {
        private void OnTriggerStay(Collider other)
        {
            float m_damagePerSec = 20f;
            if (other.gameObject.tag == "Player")
            {
                CharacterStat charStat = other.GetComponentInParent<CharacterStat>();
                if (charStat != null)
                {
                    charStat.AddDamage(m_damagePerSec * Time.fixedDeltaTime);

                }
            }
        }
    }
}