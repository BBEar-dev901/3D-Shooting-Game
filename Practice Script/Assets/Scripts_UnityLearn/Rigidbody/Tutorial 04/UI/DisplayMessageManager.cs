using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public struct DisplayMessage
    {
        public string message;
        public DisplayMessage(string _msg)
        {
            message = _msg;
        }
    }
    public class DisplayMessageManager : MonoBehaviour
    {
        public static DisplayMessageManager Instance;

        [SerializeField] float m_displayTime = 3f;
        [SerializeField] float m_displayIntervalTime = 0.25f;
        [SerializeField] CanvasGroup m_canvasGroup;
        [SerializeField] Text m_notifyMassage;

        public Queue<DisplayMessage> displayMessagesQueue = new Queue<DisplayMessage>();

        float elapsedTimer = 0f;

        enum DisplayMessageStatus
        {
            None,
            Displaying,
            Breaking,
        }
        DisplayMessageStatus m_displayStatus = DisplayMessageStatus.None;
        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            InactiveMessage();
        }
        void Update()
        {
            float waitingMessageCount = displayMessagesQueue.Count;

            switch (m_displayStatus)
            {
                case DisplayMessageStatus.None:

                    if(waitingMessageCount > 0)
                    {
                        elapsedTimer = 0f;
                        m_displayStatus = DisplayMessageStatus.Displaying;
                        DisplayMessage msg = displayMessagesQueue.Dequeue();
                        m_notifyMassage.text = msg.message;
                        ActiveMessage();
                    }

                    break;
                case DisplayMessageStatus.Displaying:

                    elapsedTimer += Time.deltaTime;

                    if(elapsedTimer > m_displayTime)
                    {
                        elapsedTimer = 0f;
                        m_displayStatus = DisplayMessageStatus.Breaking;
                        InactiveMessage();
                    }
                    break;
                case DisplayMessageStatus.Breaking:

                    elapsedTimer += Time.deltaTime;

                    if(elapsedTimer > m_displayIntervalTime)
                    {
                        elapsedTimer = 0f;
                        m_displayStatus = DisplayMessageStatus.None;
                    }
                    break;
            }
        }
        void ActiveMessage()
        {
            m_canvasGroup.gameObject.SetActive(true);
        }
        void InactiveMessage()
        {
            m_canvasGroup.gameObject.SetActive(false);
        }
        public void RegisterMessage(DisplayMessage _msg)
        {
            displayMessagesQueue.Enqueue(_msg);
        }
    }
}