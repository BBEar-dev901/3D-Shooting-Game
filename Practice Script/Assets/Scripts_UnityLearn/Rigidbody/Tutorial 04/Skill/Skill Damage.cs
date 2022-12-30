using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class SkillDamage : MonoBehaviour
    {
        [SerializeField] protected float m_duration;

        private void OnEnable()
        {
            Invoke("StopDamage", m_duration);
        }
        void StopDamage()
        {
            gameObject.SetActive(false);
        }
    }
}