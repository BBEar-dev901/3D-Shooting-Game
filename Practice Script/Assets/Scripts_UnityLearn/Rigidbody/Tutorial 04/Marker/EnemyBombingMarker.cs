using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class EnemyBombingMarker : MonoBehaviour
    {
        Transform e_transform;
        private void Start()
        {
            e_transform = GetComponentInParent<EnemyStat>().transform.GetChild(0).transform;
        }
        private void Update()
        {
            transform.position = new Vector3(e_transform.position.x , 10, e_transform.position.z);
            transform.rotation = Quaternion.identity;
        }
    }
}