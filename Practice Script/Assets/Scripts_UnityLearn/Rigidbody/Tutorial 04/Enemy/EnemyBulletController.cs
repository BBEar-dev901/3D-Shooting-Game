using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyBulletController : MonoBehaviour
    {
        enum EBulletBehaviorState
        {
            IDLE,
            FOLLOW,
            READY,
            AIMING,
        }

        private Rigidbody m_rigidbody;

        [Header("Bullet Movement")]
        [SerializeField] float forceFactor = 7f;
        //[SerializeField] Transform target;
        [HideInInspector] public Transform targetPlayer;
        float _towardFactor = Mathf.PI * 2;

        [Header("Bullet Behavior")]
        [SerializeField] float attackableDistance = 20f;
        [SerializeField] float attackInterval = 3f;
        [SerializeField] float readyInterval = 1f;

        [Header("Bullet Gun")]
        [SerializeField] GameObject emptyBulletPrefab;
        [SerializeField] GameObject enemyBulletPrefab;
        //[SerializeField] GameObject EnemybulletGun;
        [SerializeField] Transform E_muzzleBulletTransform;
        int enemyBulletsCount = 20;
        int emptyBulletsCount = 20;
        Queue<EnemyBullet> EnemybulletsQueue = new Queue<EnemyBullet>();
        Queue<EmptyBullet> EmptybulletsQueue = new Queue<EmptyBullet>();

        bool _spectorShoot = false;
        Transform reloadTransform;
        
        EBulletBehaviorState eEBulletState = EBulletBehaviorState.IDLE;

        float _readyTimer = 0f;
        float _aimingTimer = 0f;

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

            E_muzzleBulletTransform = transform.GetChild(0).GetChild(0).GetChild(0);

            EnemyBullet enemyBullet;
            for (int i = 0; i < enemyBulletsCount; i++)
            {
                enemyBullet = CreateNewEnemyBullet();
                enemyBullet.gameObject.SetActive(false);
                enemyBullet.gameObject.name = "Enemy" + i; 
                EnemybulletsQueue.Enqueue(enemyBullet);
            }
            EmptyBullet emptyBullet;
            for (int i = 0; i < emptyBulletsCount; i++)
            {
                emptyBullet = CreateNewEmptyBullet();
                emptyBullet.gameObject.SetActive(false);
                emptyBullet.gameObject.name = "Empty" + i;
                EmptybulletsQueue.Enqueue(emptyBullet);
            }

        }

        void FixedUpdate()
        {
            if (eEBulletState == EBulletBehaviorState.IDLE || eEBulletState == EBulletBehaviorState.FOLLOW)
            {
                Vector3 forwardNormal = (targetPlayer.position - transform.position).normalized;
                Vector3 backNormal = (transform.position - targetPlayer.position).normalized;
                float _singleStep = _towardFactor * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, forwardNormal, _singleStep, 0.0f);

                if ((targetPlayer.position - transform.position).magnitude > attackableDistance)
                {
                    m_rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
                    m_rigidbody.AddForce(forceFactor * forwardNormal);
                    //rigidbody와 transform.rotation을 같이 쓰면 유도탄이 날아간다. 같이 쓰지 말 것.
                    //transform.rotation = Quaternion.LookRotation(newDirection);
                }
                else if ((targetPlayer.position - transform.position).magnitude < (18.5f + 0.1f))
                {
                    m_rigidbody.AddForce(forceFactor * backNormal);
                }
                else
                {
                    RaycastHit hitInfo;
                    bool isHitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, 20f, 1 << LayerMask.NameToLayer("Player"));

                    if (isHitted == true)
                    {
                        eEBulletState = EBulletBehaviorState.READY;
                    }
                    else if(isHitted == false)
                    {
                        m_rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
                    }
                }
            }
        }

        private void Update()
        {
            switch (eEBulletState)
            {
                case EBulletBehaviorState.IDLE:
                    IDLEBehavior();
                    break;
                case EBulletBehaviorState.FOLLOW:
                    FollowBehavior();
                    break;
                case EBulletBehaviorState.READY:
                    ReadyBehavior();
                    break;
                case EBulletBehaviorState.AIMING:
                    AimingBehavior();
                    break;
                default:
                    Debug.Log("EnemyBullet Error");
                    break;
            }
        }

        void IDLEBehavior()
        {
            _spectorShoot = false;
            _readyTimer = 0f;
            _aimingTimer = 0f;
        }

        void FollowBehavior()
        {
            _spectorShoot = false;
            _readyTimer = 0f;
            _aimingTimer = 0f;
        }

        void ReadyBehavior()
        {
            _readyTimer += Time.deltaTime;
            if (_readyTimer > readyInterval )
            {
                _spectorShoot = true;
                SpectorShoot();
                _readyTimer = 0;
            }
        }

        void AimingBehavior()
        {
            _aimingTimer += Time.deltaTime;
            if (_aimingTimer > attackInterval)
            {
                _spectorShoot = false;
                Shoot();
                _aimingTimer = 0;
            }
        }

        void SpectorShoot()
        {
            if(_spectorShoot == true)
            {
                EmptyBullet _eBullet;
                if (EnemybulletsQueue.Count > 0)
                {
                    _eBullet = EmptybulletsQueue.Dequeue();
                    _eBullet.gameObject.SetActive(true);
                }
                else
                {
                    _eBullet = CreateNewEmptyBullet();
                }
                _eBullet.SetMuzzleTransform(E_muzzleBulletTransform);
                _eBullet.Shooting();

                eEBulletState = EBulletBehaviorState.AIMING;
            }
        }

        void Shoot()
        {
                EnemyBullet _eBullet;
                if (EnemybulletsQueue.Count > 0)
                {
                    _eBullet = EnemybulletsQueue.Dequeue();
                    _eBullet.gameObject.SetActive(true);
                }
                else
                {
                    _eBullet = CreateNewEnemyBullet();
                }
                _eBullet.SetMuzzleTransform(E_muzzleBulletTransform);
                _eBullet.Shooting();
        }

        public EnemyBullet CreateNewEnemyBullet()
        {
            EnemyBullet newEBullet;
            newEBullet = Instantiate(enemyBulletPrefab).GetComponent<EnemyBullet>();
            newEBullet.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(1));
            newEBullet.SetBulletController(this);
            return newEBullet;
        }

        public void EnemyBulletIsExpired(EnemyBullet _ebullet)
        {
            _ebullet.gameObject.SetActive(false);
            EnemybulletsQueue.Enqueue(_ebullet);
            eEBulletState = EBulletBehaviorState.IDLE;
        }

        public EmptyBullet CreateNewEmptyBullet()
        {
            EmptyBullet newEmptyBullet;
            newEmptyBullet = Instantiate(emptyBulletPrefab).GetComponent<EmptyBullet>();
            newEmptyBullet.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(2));
            newEmptyBullet.SetBulletController(this);
            return newEmptyBullet;
        }

        public void EmptyBulletIsExpired(EmptyBullet _emptybullet)
        {
            _emptybullet.gameObject.SetActive(false);
            EmptybulletsQueue.Enqueue(_emptybullet);
        }

    }// EnemyBullet
}// namespace