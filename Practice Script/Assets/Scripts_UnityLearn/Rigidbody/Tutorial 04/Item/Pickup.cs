using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class Pickup : MonoBehaviour
    {
        Vector3 m_StartPosition;

        private void Start()
        {
            m_StartPosition = transform.position;
        }
        private void Update()
        {
            float bobbingAnimationPhase = (Mathf.Sin(Time.time * Mathf.PI * 2f) * 0.15f) + 0.25f;
            transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

            transform.Rotate(Vector3.up, 360f * Time.deltaTime * 0.5f, Space.Self);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                CharacterStat characterStat = other.GetComponentInParent<CharacterStat>();
                OnPicked(characterStat);

                Debug.Log("Pickup an Item");
                gameObject.SetActive(false);
            }
        }
        protected virtual void OnPicked(CharacterStat _stat)
        {
            PlayPickupFeedback();
        }
        public void PlayPickupFeedback()
        {

        }
    }
}