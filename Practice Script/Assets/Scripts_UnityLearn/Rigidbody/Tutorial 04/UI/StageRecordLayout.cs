using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public struct StageRecordMessage
    {
        public string _StageText;
        public string _StageRecordText;
        public string _BestStageRecordText;
        public string _NewRecordText;
    }
    public class StageRecordLayout : MonoBehaviour
    {
        [SerializeField] Text m_StageText;
        [SerializeField] Text m_StageRecordText;
        [SerializeField] Text m_BestStageRecordText;
        [SerializeField] Text m_NewRecordText;

        public void InitializeRecord(StageRecordMessage _display)
        {
            m_StageText.text = _display._StageText;
            m_StageRecordText.text = _display._StageRecordText;
            m_BestStageRecordText.text = _display._BestStageRecordText;
            m_NewRecordText.text = _display._NewRecordText;
        }
    }
}