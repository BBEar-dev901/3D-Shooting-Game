using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] Text resultText;
        [Header("Game Time Text")]
        [SerializeField] Text GameTimeText;
        [SerializeField] Text bestGameTimeText;
        [Header("etc")]
        [SerializeField] StageManager m_StageManager;
        [SerializeField] Button restartButton;

        StageRecordMessage m_StageRecordMessage;
        GameObject StageRecordLayout;

        private void Start()
        {
            restartButton = GetComponentInChildren<Button>();

            restartButton.onClick.AddListener(OnClickRestartButton);

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        public void ShowResult(bool _bwin = true)
        {
            if (_bwin == true)
            {
                resultText.text = "Win!";
                // 저장한 게임타임 불러오기
                SetTimeText();
                //j_SetTimeText();
            }
            else
            {
                resultText.text = "Lose!";
                // 저장한 게임타임 불러오기
                SetTimeText();
                //j_SetTimeText();
            }
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        public void OnClickRestartButton()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            m_StageManager.OnRestartGame();
        }
        String GetTimeString(float _time)
        {
            if (_time == 99999999f)
            {
                return "No Time";
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
                return string.Format("{0:00}:{1:00}.{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            }
        }
        // PlayerPrefs saveSystem
        void SetTimeText()
        {
            for(int i = 0; i < m_StageManager.GetmaxStage(); i++)
            {
                m_StageRecordMessage._StageText = "Stage" + (i + 1);
                m_StageRecordMessage._StageRecordText = GetTimeString(PlayerPrefs.GetFloat("stage" + i));
                m_StageRecordMessage._BestStageRecordText = GetTimeString(PlayerPrefs.GetFloat("BestTime" + i));
                if ( PlayerPrefs.GetFloat("BestTime" + i) == PlayerPrefs.GetFloat("stage" + i) && PlayerPrefs.GetFloat("BestTime" + i) != 99999999f )
                {
                    m_StageRecordMessage._NewRecordText = "New Record!";
                }
                else
                {
                    m_StageRecordMessage._NewRecordText = " ";
                }
                StageRecordLayout = StageRecordManager.Instance.RegisterRecordLayout(m_StageRecordMessage);
            }
            GameTimeText.text = GetTimeString(PlayerPrefs.GetFloat("GameTime"));
            bestGameTimeText.text = GetTimeString(PlayerPrefs.GetFloat("BestGameTime"));
        }

        // json save system
        void j_SetTimeText()
        {
            for(int i = 0; i < m_StageManager.GetmaxStage(); i++)
            {
                m_StageRecordMessage._StageText = "Stage" + (i + 1);
                m_StageRecordMessage._StageRecordText = GetTimeString(JsonSaveSystem.Instance.j_LoadStageTime(i));
                m_StageRecordMessage._BestStageRecordText = GetTimeString(JsonSaveSystem.Instance.j_LoadBestStageTime(i));
                if (JsonSaveSystem.Instance.j_LoadBestStageTime(i) == JsonSaveSystem.Instance.j_LoadStageTime(i)
                                                                    && JsonSaveSystem.Instance.j_LoadBestStageTime(i) != 99999999f)
                {
                    m_StageRecordMessage._NewRecordText = "New Record!";
                }
                else
                {
                    m_StageRecordMessage._NewRecordText = " ";
                }
                StageRecordLayout = StageRecordManager.Instance.RegisterRecordLayout(m_StageRecordMessage);
            }

            GameTimeText.text = GetTimeString(JsonSaveSystem.Instance.j_LoadGameTime());
            bestGameTimeText.text = GetTimeString(JsonSaveSystem.Instance.j_LoadBestGameTime());

            if (JsonSaveSystem.Instance.j_LoadGameTime() == JsonSaveSystem.Instance.j_LoadBestGameTime() 
                                                                    && JsonSaveSystem.Instance.j_LoadBestGameTime() != 99999999f)
            {
                transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            }

        }

    }
}