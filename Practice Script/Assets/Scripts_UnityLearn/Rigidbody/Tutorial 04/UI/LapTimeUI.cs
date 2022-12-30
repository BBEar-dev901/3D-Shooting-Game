using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Lecture.RigidBody.Tutorial04
{
    public enum LapTimerType
    {
        Stage,
        Game,
        All,
    }
    public class LapTimeUI : MonoBehaviour
    {
        public static LapTimeUI Instance;

        [SerializeField] Text m_stageLapTimerText;
        [SerializeField] Text m_gameLapTimerText;

        float m_stageLapTimer;
        float m_gameLapTimer;
        bool forceToUpdate = false;

        void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            if (forceToUpdate)
            {
                forceToUpdate = false;
                m_stageLapTimerText.text = GetTimeString(m_stageLapTimer);
                m_gameLapTimerText.text = GetTimeString(m_gameLapTimer);
            }
            else if(StageManager.playstate == PlayState.Run)
            {
                m_stageLapTimer += Time.deltaTime;
                m_gameLapTimer += Time.deltaTime;

                m_stageLapTimerText.text = GetTimeString(m_stageLapTimer);
            }
            else if(StageManager.playstate == PlayState.End)
            {
                m_stageLapTimerText.text = GetTimeString(m_stageLapTimer);
                m_gameLapTimerText.text = GetTimeString(m_gameLapTimer);
            }
        }
        String GetTimeString(float _time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
            return string.Format("{0:00}:{1:00}.{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
        public void ResetTimer(LapTimerType _type)
        {
            switch (_type)
            {
                case LapTimerType.Stage:
                    m_stageLapTimer = 0f;
                    break;
                case LapTimerType.Game:
                    m_gameLapTimer = 0f;
                    break;
                case LapTimerType.All:
                    m_stageLapTimer = 0f;
                    m_gameLapTimer = 0f;
                    break;
            }
            forceToUpdate = true;
        }
        public float getStageTime()
        {
            return m_stageLapTimer;
        }
        public float getGameTime()
        {
            return m_gameLapTimer;
        }
    }
}