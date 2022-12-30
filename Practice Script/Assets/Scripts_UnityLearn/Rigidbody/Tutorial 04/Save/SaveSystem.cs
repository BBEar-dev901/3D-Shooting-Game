using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class SaveSystem : MonoBehaviour
    {
        StageManager stageManager;
        [SerializeField] CanvasGroup newScore;

        void Awake()
        {
            stageManager = GetComponent<StageManager>();
            PlayerPrefs.DeleteAll();
        }
        public void Save()
        {
            // 아무 키가 없을 때
            if (PlayerPrefs.HasKey("BestTime0") == false && PlayerPrefs.HasKey("stage0") == false)
            {
                for(int i = 0; i < stageManager.GetmaxStage(); i++)
                {
                    PlayerPrefs.SetFloat("BestTime" + i, 99999999f);
                    PlayerPrefs.SetFloat("stage" + i, 99999999f);
                }
                PlayerPrefs.SetFloat("BestGameTime", 99999999f);
                PlayerPrefs.SetFloat("GameTime", 99999999f);
            }
            // 비교
            if (PlayerPrefs.GetFloat("BestGameTime") > PlayerPrefs.GetFloat("GameTime"))
            {
                PlayerPrefs.SetFloat("BestGameTime", PlayerPrefs.GetFloat("GameTime"));
                newScore.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                newScore.transform.GetChild(0).gameObject.SetActive(false);
            }
            PlayerPrefs.Save();
        }

        public void UpdateRecord(int stagenumber)
        {
            if (PlayerPrefs.GetFloat("BestTime" + stagenumber) > PlayerPrefs.GetFloat("stage" + stagenumber))
            {
                PlayerPrefs.SetFloat("BestTime" + stagenumber, PlayerPrefs.GetFloat("stage" + stagenumber));
            }
            PlayerPrefs.Save();
        }

        public void stageTimeClear()
        {
            if (PlayerPrefs.GetFloat("stage0") != 99999999f)
            {
                for (int i = 0; i < stageManager.GetmaxStage(); i++)
                {
                    PlayerPrefs.SetFloat("stage" + i, 99999999f);
                }

                PlayerPrefs.SetFloat("GameTime", 99999999f);
            }
        }

    }
}

