using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lecture.RigidBody.Tutorial03;

public class MoverVelocity : BallMover
{
    protected override void Move()
    {
        m_rigidbody.AddForce(Vector3.up * moveFactor, ForceMode.VelocityChange);
    }
    void Update()
    {
        if (Input.GetKeyDown(moveActiveKey))
        {
            Move();
        }
    }
}
