using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class DamageBomb : SkillDamage
    {
        private void OnTriggerEnter(Collider other)
        {
            float m_damage = 30f;
            if (other.gameObject.tag == "Player")
            {
                CharacterStat characterStat = other.GetComponentInParent<CharacterStat>();
                if (characterStat != null)
                {
                    characterStat.AddDamage(m_damage);
                }
            }
        }

    }
}