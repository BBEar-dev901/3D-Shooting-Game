using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lecture.RigidBody.Tutorial04;

public class DamageMagicRunic : SkillDamage
{
    private void OnTriggerStay(Collider other)
    {
        float m_damagePerSec = 15f;
        if(other.gameObject.tag == "Enemy")
        {
            EnemyStat enemyStat = other.GetComponentInParent<EnemyStat>();
            if(enemyStat != null)
            {
                enemyStat.AddDamage(CharacterStat.BuffOffense * m_damagePerSec * Time.fixedDeltaTime);
            }
        }
    }
}
