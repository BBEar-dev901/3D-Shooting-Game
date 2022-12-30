using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial03
{
    public class BallMover : MonoBehaviour
    {
        [SerializeField] protected KeyCode moveActiveKey = KeyCode.UpArrow;
        [SerializeField] protected float moveFactor = 15f;

        protected Rigidbody m_rigidbody;
        void Awake()
        {
            Initialize();
        }
        protected void Initialize()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (Input.GetKey(moveActiveKey))
            {
                Move();
            }
        }
        virtual protected void Move()
        {
            Debug.Log(gameObject);
        }
    }
}

