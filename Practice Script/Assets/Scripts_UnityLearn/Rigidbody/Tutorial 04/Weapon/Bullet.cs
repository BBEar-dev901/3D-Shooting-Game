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
        // 총구의 위치 값을 저장할 변수
        Transform reloadTransform;
        float lifeTime = 2f;
        float expiredTimer = 0f;

        bool isAllocatedDynamic;

        // 이펙트 추가용 변수
        public GameObject m_bulletFx;
        public GameObject m_bloodFx;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();

            if(reloadTransform)
            {
                // 총알을 배치할 때 위치를 총구의 위치로 지정해주기
                transform.position = reloadTransform.position;
                transform.localEulerAngles = reloadTransform.localEulerAngles;
            }
        }
        public void Shooting(bool _isDynamicAllocated = false)
        {
            Reload();
            // 월드가 아닌 로컬 좌표계의 앞쪽으로 총알 날아가게 하기
            m_RigidBody.AddForce(transform.forward * moveFactor, ForceMode.Force);
            if (m_useGravity)
            {
                m_RigidBody.useGravity = true;
            }
        }
        public void Reload()
        {
            // 재장전 할때 위치 값
            m_RigidBody.angularVelocity = Vector3.zero;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;

            if (reloadTransform)
            {
                // 재장전시 총구의 위치로 총알 배치하기
                transform.position = reloadTransform.position;
                transform.eulerAngles = reloadTransform.eulerAngles;
            }
            // 타이머 초기화 시키기
            expiredTimer = 0f;
        }
        private void OnEnable()
        {
            // 총알이 나타날 때 재장전 함수 불러오기
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
            // 일정 시간이 지나면 객체 실체 끄기
            if(expiredTimer > lifeTime)
            {
                CannonControl.Instance.BulletIsExpired(this);

                gameObject.SetActive(false);

            }
        }
        public void SetMuzzleTransform (Transform _transform)
        {
            // 캐논 컨트롤에서 총구의 위치 값 받아와서 세팅시켜주기
            reloadTransform = _transform;
        }
    }
}

