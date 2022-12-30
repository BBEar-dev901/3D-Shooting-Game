using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial02
{
    public class Mover : MonoBehaviour
    {
        private Rigidbody rb;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnMouseDown()
        {
            Debug.Log("OnMouseDown()");
            //rb.AddForce(transform.forward * 500f);
        }
        private void OnMouseDrag()
        {
            Debug.Log("OnMouseDrag");
        }
        private void OnMouseEnter()
        {
            Debug.Log("OnMouseEnter");
        }
        private void OnMouseExit()
        {
            Debug.Log("OnMouseExit");
        }
        private void OnMouseOver()
        {
            Debug.Log("OnMouseOver");
        }
        private void OnMouseUp()
        {
            Debug.Log("OnMouseUp");
        }
        private void OnMouseUpAsButton()
        {
            Debug.Log("OnMouseUpAsButton");
        }
    }
}

