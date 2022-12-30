using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Lecture.RigidBody.Tutorial04
{
    public class CannonDamageBar : MonoBehaviour
    {
        [SerializeField] Text m_damageText;
        float m_lifeTimer = 0f;

        public static CannonDamageBar Instance;

        private void Awake()
        {
            Instance = this;
        }
        public void TextPlay(float m_damage)
        {
            if (this.gameObject.activeInHierarchy == true)
            {
                m_damageText.color = new Color(1, 0, 0, 1);
                m_damageText.transform.position = new Vector3(m_damageText.transform.position.x, 0 , m_damageText.transform.position.z);
                if(m_damage < 1)
                {
                    m_damageText.text = string.Format("{0:f1}", m_damage);
                }
                else
                {
                    m_damageText.text = Mathf.Floor(m_damage).ToString();
                }
                StartCoroutine(TextMoving());
            }
        }
        IEnumerator TextMoving()
        {
            while (m_lifeTimer < 2.6f)
            {
                m_lifeTimer += Time.fixedDeltaTime;
                if (m_damageText.color.a > 0)
                {
                    m_damageText.color = new Color(m_damageText.color.r, m_damageText.color.g, m_damageText.color.b, m_damageText.color.a - 0.01f);
                    m_damageText.transform.position = new Vector3(m_damageText.transform.position.x, m_damageText.transform.position.y + 0.1f, m_damageText.transform.position.z);
                    transform.forward = Camera.main.transform.forward;
                }
                if (m_lifeTimer > 2.5f)
                {
                    gameObject.SetActive(false);
                    gameObject.transform.SetParent(GameObject.Find("Player Damage Pooler").transform);
                    m_lifeTimer = 0;
                }
                yield return new WaitForFixedUpdate();
            }
        }

    }
}