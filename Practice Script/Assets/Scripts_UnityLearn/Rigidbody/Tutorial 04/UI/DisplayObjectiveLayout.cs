using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public struct DisplayObjectiveMessage
    {
        public string _TitleText;
        public string _DescriptionText;
        public string _CounterText;
    }
    public class DisplayObjectiveLayout : MonoBehaviour
    {
        [SerializeField] Text m_TitleText;
        [SerializeField] Text m_DescriptionText;
        [SerializeField] Text m_CounterText;
        [SerializeField] Image m_StatusImage;

        public void InitializeObjective(DisplayObjectiveMessage _display)
        {
            m_TitleText.text = _display._TitleText;
            m_DescriptionText.text = _display._DescriptionText;
            m_CounterText.text = _display._CounterText;
        }
        public void UpdateCounter(string _counterText)
        {
            m_CounterText.text = _counterText;
        }
        public void UpdateCompleted()
        {
            m_StatusImage.color = Color.green;
            m_CounterText.color = Color.green;
            m_DescriptionText.color = Color.green; 
        }
    }
}