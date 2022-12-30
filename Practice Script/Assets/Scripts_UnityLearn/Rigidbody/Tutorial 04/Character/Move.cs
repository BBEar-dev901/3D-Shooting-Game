using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class Move : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        [Header("Character Movement")]
        [SerializeField] private float forceFactor = 10f;
        [SerializeField] private float rotateFactor = 5f;

        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            m_rigidbody.drag = 1f;
            m_rigidbody.useGravity = true;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            transform.GetChild(0).GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.staticFriction = 1f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounciness = 0.4f;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.frictionCombine = PhysicMaterialCombine.Maximum;
            transform.GetChild(0).GetComponent<CapsuleCollider>().material.bounceCombine = PhysicMaterialCombine.Average;
        }
        void FixedUpdate()
        {
            float right = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            Vector3 eulerAngle = new Vector3(0, right * rotateFactor, 0);
            m_rigidbody.MoveRotation(Quaternion.Euler(eulerAngle) * m_rigidbody.rotation);

            m_rigidbody.AddForce(forceFactor * transform.forward * forward);
        }
    }
}