using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        Rigidbody m_RigidBody;
        [SerializeField] float moveFactor = 50f;
        [SerializeField] bool m_useGravity = false;
        // �ѱ��� ��ġ ���� ������ ����
        Transform reloadTransform;
        float lifeTime = 2f;
        float expiredTimer = 0f;

        bool isAllocatedDynamic;

        // ����Ʈ �߰��� ����
        public GameObject m_bulletFx;
        public GameObject m_bloodFx;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();

            if(reloadTransform)
            {
                // �Ѿ��� ��ġ�� �� ��ġ�� �ѱ��� ��ġ�� �������ֱ�
                transform.position = reloadTransform.position;
                transform.localEulerAngles = reloadTransform.localEulerAngles;
            }
        }
        public void Shooting(bool _isDynamicAllocated = false)
        {
            Reload();
            // ���尡 �ƴ� ���� ��ǥ���� �������� �Ѿ� ���ư��� �ϱ�
            m_RigidBody.AddForce(transform.forward * moveFactor, ForceMode.Force);
            if (m_useGravity)
            {
                m_RigidBody.useGravity = true;
            }
        }
        public void Reload()
        {
            // ������ �Ҷ� ��ġ ��
            m_RigidBody.angularVelocity = Vector3.zero;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;

            if (reloadTransform)
            {
                // �������� �ѱ��� ��ġ�� �Ѿ� ��ġ�ϱ�
                transform.position = reloadTransform.position;
                transform.eulerAngles = reloadTransform.eulerAngles;
            }
            // Ÿ�̸� �ʱ�ȭ ��Ű��
            expiredTimer = 0f;
        }
        private void OnEnable()
        {
            // �Ѿ��� ��Ÿ�� �� ������ �Լ� �ҷ�����
            Reload();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                CannonControl.Instance.BulletIsExpired(this);

                GameObject _bulletSkill = SkillPooler.Instance.GetCannonSkill(CannonSkillType.CANNON_SKILL_BULLET);
                if (_bulletSkill)
                {
                    _bulletSkill.transform.position = collision.GetContact(0).point;
                    _bulletSkill.SetActive(true);
                }

                GameObject _bulletfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_BULLETEXPLOSION);
                if (_bulletfx)
                {
                    _bulletfx.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                    _bulletfx.SetActive(true);
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
                CannonControl.Instance.BulletIsExpired(this);

                GameObject _bulletSkill = SkillPooler.Instance.GetCannonSkill(CannonSkillType.CANNON_SKILL_BULLET);
                if (_bulletSkill)
                {
                    _bulletSkill.transform.position = collision.GetContact(0).point;
                    _bulletSkill.SetActive(true);
                }

                GameObject _bulletfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_BULLETEXPLOSION);
                if (_bulletfx)
                {
                    _bulletfx.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                    _bulletfx.SetActive(true);
                }

                gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            expiredTimer += Time.deltaTime;
            // ���� �ð��� ������ ��ü ��ü ����
            if(expiredTimer > lifeTime)
            {
                CannonControl.Instance.BulletIsExpired(this);

                gameObject.SetActive(false);

            }
        }
        public void SetMuzzleTransform (Transform _transform)
        {
            // ĳ�� ��Ʈ�ѿ��� �ѱ��� ��ġ �� �޾ƿͼ� ���ý����ֱ�
            reloadTransform = _transform;
        }
    }
}

