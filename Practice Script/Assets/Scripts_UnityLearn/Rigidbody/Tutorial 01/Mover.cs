using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lecture.RigidBody.Tutorial01
{
    public class Mover : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0,0,1f * Time.deltaTime);
        }
    }
}

