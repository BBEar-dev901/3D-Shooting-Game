using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lecture.RigidBody.Tutorial04
{
    public class DamagePlayerBomd : SkillDamage
    {
        private void OnTriggerEnter(Collider other)
        {
            float m_damage = 30f;
            if (other.gameObject.tag == "Enemy")
            {
                EnemyStat enemyStat = other.GetComponentInParent<EnemyStat>();
                if (enemyStat != null)
                {
                    enemyStat.AddDamage(CharacterStat.BuffOffense * m_damage);
                }
            }
        }
    }
}