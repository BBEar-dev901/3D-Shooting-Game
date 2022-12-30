using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EmptyBullet : MonoBehaviour
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
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EmptyBullet"), true);
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Attacker"), LayerMask.NameToLayer("EmptyBullet"), true);

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
            if (collision.collider.CompareTag("Ground"))
            {
                eController.EmptyBulletIsExpired(this);

                GameObject _Ebulletfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_EMPTY_BULLET);
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
                eController.EmptyBulletIsExpired(this);
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

    } //EmptyBullet
} // namespace