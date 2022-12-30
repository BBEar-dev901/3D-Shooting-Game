using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyAxe : MonoBehaviour
    {
        enum EAxeBehaviorState
        {
            IDLE,
            FOLLOW,
            READY,
            ATTACK,
        }

        [Header("Enemy Axe Movement")]
        [SerializeField] float forceFactor = 12f;
        //[SerializeField] Transform target;
        [HideInInspector] public Transform targetPlayer;
        float _towardFactor = Mathf.PI * 2;

        [Header("Enemy Axe Behavior")]
        [SerializeField] Transform axeReady;
        [SerializeField] float attackableDistance = 5f;
        [SerializeField] float readyInterval = 1.5f;
        [SerializeField] float attackTime = 2f;
        
        Rigidbody m_rigidbody;
        float _readyTimer = 0f;
        float _attackTimer = 0f;


        float moveTime = 0.5f;
        float midValue = 0f;

        EAxeBehaviorState eAxeState = EAxeBehaviorState.IDLE;

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

            axeReady = transform.GetChild(0).GetChild(1);

            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Attacker"), LayerMask.NameToLayer("Attacker"), true);
        }

        void FixedUpdate()
        {
            if (eAxeState == EAxeBehaviorState.IDLE || eAxeState == EAxeBehaviorState.FOLLOW)
            {
                Vector3 _target = targetPlayer.position - new Vector3(0, 0.5f, 0);
                Vector3 forwardNormal = (targetPlayer.position - transform.position).normalized;
                float _singleStep = _towardFactor * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, forwardNormal, _singleStep, 0.0f);

                if ((targetPlayer.position - transform.position).magnitude > attackableDistance)
                {
                    m_rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
                    m_rigidbody.AddForce(forceFactor * forwardNormal);
                }
                else
                {
                    eAxeState = EAxeBehaviorState.READY;
                }
            }
        }
        private void Update()
        {
            switch (eAxeState)
            {
                case EAxeBehaviorState.IDLE:
                    IdleBehavior();
                    break;
                case EAxeBehaviorState.FOLLOW:
                    FollowBehavior();
                    break;
                case EAxeBehaviorState.READY:
                    ReadyBehavior();
                    break;
                case EAxeBehaviorState.ATTACK:
                    AttackBehavior();
                    break;
                default:
                    Debug.Log("EnemyAxe Error");
                    break;
            }
        }
        void IdleBehavior()
        {
            _readyTimer = 0f;
            midValue = 0f;
        }
        void FollowBehavior()
        {
            _readyTimer = 0f;
            midValue = 0f;
        }
        void ReadyBehavior()
        {
            _readyTimer += Time.deltaTime;
            if(_readyTimer > 0.5f)
            {
                GameObject _axeReadyfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_AXE_READY);
                if (_axeReadyfx != null)
                {
                    _axeReadyfx.transform.position = axeReady.position;
                    _axeReadyfx.SetActive(true);
                }
            }
            if (_readyTimer > readyInterval)
            {
                eAxeState = EAxeBehaviorState.ATTACK;
            }
        }
        void AttackBehavior()
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer <= attackTime)
            {
                midValue += Time.deltaTime / moveTime;
                transform.localEulerAngles = new Vector3(0f, midValue * 360f, 0f);
            } 
            else if(_attackTimer > attackTime)
            {
                eAxeState = EAxeBehaviorState.IDLE;
                _attackTimer = 0f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && eAxeState == EAxeBehaviorState.ATTACK)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);

                GameObject _axeSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_AXE);
                if (_axeSkill)
                {
                    _axeSkill.transform.position = collisionPoint;
                    _axeSkill.SetActive(true);
                }
                GameObject _axeAttackFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_AXE);
                if (_axeAttackFx != null)
                {
                    _axeAttackFx.transform.position = collisionPoint;
                    _axeAttackFx.SetActive(true);
                }
                GameObject _bloodFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_BLOODEXPLOSION);
                if (_bloodFx != null)
                {
                    _bloodFx.transform.position = collisionPoint;
                    _bloodFx.transform.rotation = Quaternion.LookRotation(collisionPoint.normalized * -1);
                    _bloodFx.SetActive(true);
                }
            }
        }

    }//EnemyAxe
}//namespace