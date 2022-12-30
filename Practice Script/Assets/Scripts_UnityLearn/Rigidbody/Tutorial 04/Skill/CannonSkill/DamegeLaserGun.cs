using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class DamegeLaserGun : SkillDamage
    {
        float m_damage = 10f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyStat _enemyStat = other.GetComponentInParent<EnemyStat>();
                if (_enemyStat)
                {
                    _enemyStat.AddDamage(CharacterStat.BuffOffense * m_damage);
                }
            }
        }
    }
}