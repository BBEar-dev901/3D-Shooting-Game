using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyBullet : MonoBehaviour
    {
        Rigidbody m_RigidBody;
        [SerializeField] float EmoveFactor = 325f;
        [SerializeField] bool m_useGravity = false;
        Transform reloadTransform;
        EnemyBulletController eController;

        float lifeTime = 4f;
        float EexpiredTimer = 0f;

        private void Awake()
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Attacker"), LayerMask.NameToLayer("EnemyBullet"), true);

            m_RigidBody = GetComponent<Rigidbody>();
            gameObject.SetActive(false);

            if (reloadTransform)
            {
                transform.position = reloadTransform.position;
                transform.localEulerAngles = reloadTransform.localEulerAngles;
            }
        }
        public void Shooting(bool _isDynamicAllocated = false)
        {
            Reload();
            if (m_useGravity == false)
            {
                m_RigidBody.useGravity = true;
                m_useGravity = true;
            }
            m_RigidBody.AddForce(transform.forward * EmoveFactor, ForceMode.Force);
        }
        public void Reload()
        {
            m_RigidBody.angularVelocity = Vector3.zero;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            m_useGravity = false;

            if (reloadTransform)
            {
                transform.position = reloadTransform.position;
                transform.eulerAngles = reloadTransform.eulerAngles;
                gameObject.SetActive(true);
            }
            EexpiredTimer = 0f;
        }
        private void OnEnable()
        {
            Reload();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                eController.EnemyBulletIsExpired(this);

                GameObject _EbulletSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_BULLET);
                if (_EbulletSkill)
                {
                    _EbulletSkill.transform.position = collision.GetContact(0).point;
                    _EbulletSkill.SetActive(true);
                }

                GameObject _Ebulletfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_BULLET);
                if (_Ebulletfx)
                {
                    _Ebulletfx.transform.position = collision.GetContact(0).point;
                    _Ebulletfx.SetActive(true);
                }
                GameObject _bloodfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_BLOODEXPLOSION);
                if (_bloodfx)
                {
                    _bloodfx.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                    _bloodfx.SetActive(true);
                }
                gameObject.SetActive(false);

            }
            if (collision.collider.CompareTag("Ground"))
            {
                eController.EnemyBulletIsExpired(this);

                GameObject _EbulletSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_BULLET);
                if (_EbulletSkill)
                {
                    _EbulletSkill.transform.position = collision.GetContact(0).point;
                    _EbulletSkill.SetActive(true);
                }

                GameObject _Ebulletfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_BULLET);
                if (_Ebulletfx)
                {
                    _Ebulletfx.transform.position = collision.GetContact(0).point;
                    _Ebulletfx.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            EexpiredTimer += Time.deltaTime;
            if (EexpiredTimer > lifeTime)
            {
                eController.EnemyBulletIsExpired(this);
                gameObject.SetActive(false);
            }
        }
        public void SetMuzzleTransform(Transform _transform)
        {
            reloadTransform = _transform;
        }

        public void SetBulletController(EnemyBulletController _eController)
        {
            eController = _eController;
        }

    }
} // namespace