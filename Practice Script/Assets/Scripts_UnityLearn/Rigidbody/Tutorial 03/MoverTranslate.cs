using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lecture.RigidBody.Tutorial03;

public class MoverTranslate : BallMover
{
    protected override void Move()
    {
        transform.Translate(Vector3.up * moveFactor * Time.deltaTime);
    }
}
