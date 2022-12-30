using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [Tooltip("Image component displaying current health")]
        public Image HealthFillImage;
        public Text HealthText;

        CharacterStat m_CharacterStat;

        private void Awake()
        {
            m_CharacterStat = GameObject.FindObjectOfType<CharacterStat>();
        }
        private void Update()
        {
            HealthFillImage.fillAmount = m_CharacterStat.CurrentHp / m_CharacterStat.MaxHp;
            HealthText.text = Mathf.RoundToInt(m_CharacterStat.CurrentHp).ToString() + "/" + m_CharacterStat.MaxHp.ToString("F0");
        }
    }
}