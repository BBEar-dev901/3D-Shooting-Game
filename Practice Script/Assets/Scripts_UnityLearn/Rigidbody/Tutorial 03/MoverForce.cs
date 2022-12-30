using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lecture.RigidBody.Tutorial03;

public class MoverForce : BallMover
{
    protected override void Move()
    {
        m_rigidbody.AddForce(Vector3.up * moveFactor);
    }
}
