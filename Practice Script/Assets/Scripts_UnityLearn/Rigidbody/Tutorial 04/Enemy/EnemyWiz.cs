using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyWiz : MonoBehaviour
    {
        enum WizBehaviorState
        {
            IDLE,
            FOLLOW,
            READY,
            AIMING,
        }
        private Rigidbody m_rigidbody;

        [Header("Wiz Movement")]
        [SerializeField] float forceFactor = 10f;
        //[SerializeField] Transform target;
        [HideInInspector] public Transform targetPlayer;
        [HideInInspector] public Transform shootPosition;

        [Header("Wiz Behavior")]
        [SerializeField] Transform aimPosition;
        [SerializeField] float attackableDistance = 17f;
        [SerializeField] float readyInterval = 0.5f;
        [SerializeField] float attackInterval = 2f;

        float readyTimer = 0f;
        float aimingTimer = 0f;

        WizBehaviorState eWizState = WizBehaviorState.IDLE;
        Renderer m_renderer;
        Color[] stateColor = { Color.white, Color.red, Color.green, Color.cyan };

        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.drag = 0.8f;
            m_rigidbody.useGravity = true;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            transform.GetChild(0).GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f; 
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.staticFriction = 1f; 
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounciness = 0.4f; 
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.frictionCombine = PhysicMaterialCombine.Maximum; 
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounceCombine = PhysicMaterialCombine.Average; 

            m_renderer = GetComponentInChildren<Renderer>();
            m_renderer.material.SetColor("_Color", stateColor[(int)WizBehaviorState.IDLE]);
        }
        void FixedUpdate()
        {
            Vector3 forwardNormal = (targetPlayer.position - transform.position).normalized;
            if((targetPlayer.position - transform.position).magnitude > attackableDistance)
            {
                m_rigidbody.AddForce(forceFactor * forwardNormal);
                eWizState = WizBehaviorState.FOLLOW;
            }
            else if(eWizState != WizBehaviorState.AIMING)
            {
                eWizState = WizBehaviorState.READY;
            }
            if(targetPlayer.gameObject.activeInHierarchy == false)
            {
                eWizState = WizBehaviorState.IDLE;
                return;
            }
        }
        private void Update()
        {
            switch (eWizState)
            {
                case WizBehaviorState.IDLE:
                    IdleBehavior();
                    break;
                case WizBehaviorState.FOLLOW:
                    FollowBehavior();
                    break;
                case WizBehaviorState.READY:
                    ReadyBehavior();
                    break;
                case WizBehaviorState.AIMING:
                    AimingBehavior();
                    break;
                default:
                    Debug.Log("EnemyWiz Error");
                    break;
            }
        }

        void IdleBehavior()
        {
            m_renderer.material.SetColor("_Color", stateColor[(int)WizBehaviorState.IDLE]);
            m_renderer.material.DisableKeyword("_EMISSION");
        }        
        void ReadyBehavior()
        {
            readyTimer += Time.deltaTime;
            if(readyTimer > readyInterval)
            {
                eWizState = WizBehaviorState.AIMING;
                readyTimer = 0f;
            }
            m_renderer.material.SetColor("_Color", stateColor[(int)WizBehaviorState.READY]);
        }
        void FollowBehavior()
        {
            readyTimer = 0f;
            aimingTimer = 0f;
            m_renderer.material.SetColor("_Color", stateColor[(int)WizBehaviorState.FOLLOW]);
            m_renderer.material.DisableKeyword("_EMISSION");
        }
        void AimingBehavior()
        {
            aimingTimer += Time.deltaTime;

            if(aimingTimer > attackInterval)
            {
                Shoot();
                aimingTimer = 0;
            }
            float _value;
            _value = Mathf.PingPong(aimingTimer * 5f, 1f);
            m_renderer.material.SetColor("_Color", stateColor[(int)WizBehaviorState.AIMING]);
            m_renderer.material.SetColor("_EmissionColor", new Color(_value, _value, _value));
            m_renderer.material.EnableKeyword("_EMISSION");
        }
        void Shoot()
        {
            RaycastHit hitInfo;
            bool isHitted = Physics.Linecast(aimPosition.position, shootPosition.position, out hitInfo, 1 << LayerMask.NameToLayer("Player"));
            if (isHitted)
            {
                GameObject _wizSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_WIZ);
                if (_wizSkill)
                {
                    _wizSkill.transform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    _wizSkill.SetActive(true);
                }
                GameObject _wizHitFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_WIZ_HIT);
                if(_wizHitFx != null)
                {
                    _wizHitFx.transform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    _wizHitFx.SetActive(true);
                }
            }

            eWizState = WizBehaviorState.FOLLOW;
        }
    }
}