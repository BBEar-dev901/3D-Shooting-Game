using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyPoision : MonoBehaviour
    {
        enum EPoisionBehaviorState
        {
            IDLE,
            FOLLOW,
            READY,
            AIMING,
        }

        [Header("Enemy Poision Movement")]
        [SerializeField] float forceFactor = 12f;
        //[SerializeField] Transform target;
        [HideInInspector] public Transform targetPlayer;
        float _towardFactor = Mathf.PI * 2;

        [Header("Enemy Poision Behavior")]
        [SerializeField] Transform Pmuzzletransform;
        [SerializeField] float attackableDistance = 17f;
        [SerializeField] float readyTime = 1f;
        [SerializeField] float attackTime = 0.5f;

        LineRenderer _line;
        Rigidbody m_rigidbody;
        float _readyTimer = 0f;
        float _attackTimer = 0f;
        float _poisionTimer = 0f;
        float PoisionTime = 5f;
        bool _poisionAttack = false;
        
        CannonControl control;
        
        GameObject _poisionReadyFx;
        GameObject _poisionFx;
        GameObject _poisionSkill;
        
        RaycastHit hitInfo;
        RaycastHit _AhitInfo;

        Vector3 forwardNormal;
        float _singleStep;
        Vector3 newDirection;

        EPoisionBehaviorState ePoisionState = EPoisionBehaviorState.IDLE;

        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.drag = 0.8f;
            m_rigidbody.useGravity = true;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            transform.GetChild(0).GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounciness = 0.4f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.frictionCombine = PhysicMaterialCombine.Maximum;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounceCombine = PhysicMaterialCombine.Average;

            _line = GetComponent<LineRenderer>();
            _line.SetWidth(0.1f, 0.1f);
            _line.SetColors(Color.cyan, Color.red);
            _line.enabled = false;

            Pmuzzletransform = transform.GetChild(0).GetChild(0).GetChild(0);

            control = GameObject.FindObjectOfType<CannonControl>();

        }
        void FixedUpdate()
        {

            switch (ePoisionState)
            {
                case EPoisionBehaviorState.IDLE:
                    Move();
                    break;
                case EPoisionBehaviorState.FOLLOW:
                    Move();
                    break;
                case EPoisionBehaviorState.READY:
                    Ready();
                    break;
                case EPoisionBehaviorState.AIMING:
                    Aiming();
                    break;
                default:
                    Debug.Log("Enemy Poision Error");
                    break;
            }
        }

        void Update()
        {
            switch (ePoisionState)
            {
                case EPoisionBehaviorState.IDLE:
                    IdleBehavior();
                    break;
                case EPoisionBehaviorState.FOLLOW:
                    FollowBehavior();
                    break;
                case EPoisionBehaviorState.READY:
                    ReadyBehavior();
                    break;
                case EPoisionBehaviorState.AIMING:
                    AimingBehavior();
                    break;
                default:
                    Debug.Log("Enemy Poision Error");
                    break;
            }

            if(_poisionAttack == true)
            {
                _poisionTimer += Time.deltaTime;
                if(_poisionTimer > PoisionTime)
                {
                    _poisionAttack = false;
                    _poisionTimer = 0f;
                }
            }

        }
        void IdleBehavior()
        {
            return;
        }
        void FollowBehavior()
        {
            return;
        }
        void ReadyBehavior()
        {
            if(_readyTimer > readyTime)
            {
                ReadyShoot();
                _readyTimer = 0f;
            }
        }
        void AimingBehavior()
        {
            if(_attackTimer > attackTime)
            {
                Shoot();
                _attackTimer = 0f;
            }
        }

        void Move()
        {
            _readyTimer = 0f;
            _attackTimer = 0f;
            _line.enabled = false;

            forwardNormal = (targetPlayer.position - transform.position).normalized;
            _singleStep = _towardFactor * Time.deltaTime;
            newDirection = Vector3.RotateTowards(transform.forward, forwardNormal, _singleStep, 0.0f);

            if ((targetPlayer.position - transform.position).magnitude > attackableDistance)
            {
                m_rigidbody.AddForce(forceFactor * forwardNormal);
                m_rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
                ePoisionState = EPoisionBehaviorState.FOLLOW;
            }
            else
            {
                RaycastHit hitInfo;
                bool isHitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, attackableDistance, 1 << LayerMask.NameToLayer("Player"));

                if (isHitted == true)
                {
                    if (_poisionAttack == true)
                    {
                        ePoisionState = EPoisionBehaviorState.IDLE;
                    }
                    else if (_poisionAttack == false)
                    {
                        ePoisionState = EPoisionBehaviorState.READY;
                    }
                }
                else if (isHitted == false)
                {
                    m_rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
                }
            }
        }
        void Ready()
        {
            _readyTimer += Time.deltaTime;
        }
        void Aiming()
        {
            _attackTimer += Time.deltaTime;

            if (ePoisionState == EPoisionBehaviorState.AIMING)
            {
                RaycastHit hitInfo;
                bool isHitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, attackableDistance, 1 << LayerMask.NameToLayer("Player"));
                if (isHitted == false)
                {
                    _poisionReadyFx.SetActive(false);
                    ePoisionState = EPoisionBehaviorState.IDLE;
                }
            }
        }

        void ReadyShoot()
        {
            bool isHitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, attackableDistance, 1 << LayerMask.NameToLayer("Player"));
            if (isHitted == true)
            {

                _line.enabled = true;
                _line.SetPosition(0, Pmuzzletransform.position);
                _line.SetPosition(1, hitInfo.point);

                _poisionReadyFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_POISION_READY);
                if (_poisionReadyFx != null)
                {
                    _poisionReadyFx.transform.position = hitInfo.point;
                    _poisionReadyFx.SetActive(true);
                }
                ePoisionState = EPoisionBehaviorState.AIMING;
            }
            else if (isHitted == false)
            {
                ePoisionState = EPoisionBehaviorState.IDLE;
            }
        }

        void Shoot()
        {
            bool isHitted = Physics.Raycast(transform.position, transform.forward, out _AhitInfo, attackableDistance, 1 << LayerMask.NameToLayer("Player"));
            if (isHitted == true)
            {
                _poisionAttack = true;
                _poisionSkill = SkillPooler.Instance.GetEnemySkill(EnemySkillType.ENEMY_SKILL_POISION);

                if (_poisionSkill != null)
                {
                    _poisionSkill.transform.SetParent(GameObject.Find("Cannon").transform.GetChild(0).GetChild(2));
                    _poisionSkill.SetActive(true);
                }
                _poisionFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_ENEMY_POISION);
                if (_poisionFx != null)
                {
                    _poisionFx.transform.SetParent(GameObject.Find("Cannon").transform.GetChild(0).GetChild(2));
                    _poisionFx.SetActive(true);
                }
                ePoisionState = EPoisionBehaviorState.IDLE;
            }
            else if(isHitted == false)
            {
                ePoisionState = EPoisionBehaviorState.IDLE;
            }
        }

    }//EnemyPoision
}//namespace