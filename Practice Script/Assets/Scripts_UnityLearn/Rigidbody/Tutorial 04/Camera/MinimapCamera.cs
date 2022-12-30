using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class MinimapCamera : MonoBehaviour
    {
        Transform cannonTransform;

        private void Start()
        {
            cannonTransform = GameObject.Find("Cannon").transform;
        }
        private void LateUpdate()
        {
            Vector3 playerPosition = cannonTransform.position;
            playerPosition.y = transform.position.y;
            transform.position = playerPosition;    
            transform.rotation = Quaternion.Euler(90f, cannonTransform.eulerAngles.y, 0.0f);
        }
    }
}