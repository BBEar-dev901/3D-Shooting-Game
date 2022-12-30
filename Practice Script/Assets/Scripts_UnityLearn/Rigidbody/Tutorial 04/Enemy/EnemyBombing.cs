using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyBombing : MonoBehaviour
    {
        enum BombingBehaviorState
        {
            IDLE,
            AIMING,
        }
        private Rigidbody m_rigidbody;
        Renderer m_renderer;
        Color init_color;
        BombingBehaviorState eBombState = BombingBehaviorState.AIMING;

        [Header("Enemy Movement")]
        [SerializeField] float forceFactor = 10f;
        //[SerializeField] Transform target;
        [HideInInspector] public Transform targetPlayer;
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.angularDrag = 1f;
            
            m_renderer = GetComponentInChildren<Renderer>();
            init_color = m_renderer.material.color;

            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Attacker"), LayerMask.NameToLayer("Attacker"), true);
        }
        void FixedUpdate()
        {
            if( targetPlayer.gameObject.activeInHierarchy == false )
            {
                m_renderer.material.color = Color.white;
                eBombState = BombingBehaviorState.IDLE;
                return;
            }
            else
            {
                m_renderer.material.color = init_color;
            }
            Vector3 forwardNormal = (targetPlayer.position - transform.position).normalized;
            m_rigidbody.AddForce(forceFactor * forwardNormal);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                gameObject.SetActive(false);

                GameObject _bombingSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_BOMBING);
                if (_bombingSkill)
                {
                    _bombingSkill.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                    _bombingSkill.SetActive(true);
                }
                GameObject _bombFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMYBOMBING_BOMB);
                if(_bombFx != null)
                {
                    _bombFx.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                    _bombFx.SetActive(true);
                }
            }
        }
    }
}