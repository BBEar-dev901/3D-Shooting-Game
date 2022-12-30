using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Lecture.RigidBody.Tutorial04
{
    public class JsonSaveSystem : MonoBehaviour
    {
        public List<float> bestStageRecord;
        public float bestGameRecord;
        public List<float> stageRecord;
        public float gameRecord;
        public static JsonSaveSystem Instance;
        StageManager stageManager;
        string savepath;
        string writejsonData;

        private void Awake()
        {
            Instance = this;

            stageManager = GetComponent<StageManager>();
            // ����
            savepath = Application.persistentDataPath + "/RecordSaveData.json";
        }
        public void j_firstSave()
        {

        }
        public void j_SaveTime()
        {
            // �ð� ���� �����ϱ�
            gameRecord = LapTimeUI.Instance.getGameTime();

            if(j_LoadBestGameTime() > gameRecord)
            {
                bestGameRecord = gameRecord;
                File.WriteAllText(savepath, writejsonData);
            }
            else
            {
                File.WriteAllText(savepath, writejsonData);
            }
        }

        public void j_UpdateStageTime(int _stage)
        {
            // ���ϰ� �����ϱ�
            stageRecord[_stage] = LapTimeUI.Instance.getStageTime();

            if(j_LoadBestStageTime(_stage) > stageRecord[_stage])
            {
                bestStageRecord [_stage] = stageRecord[_stage];
                File.WriteAllText(savepath, writejsonData);
            }
            else
            {
                File.WriteAllText(savepath, writejsonData);
            }
        }

        // �ҷ�����
        public float j_LoadGameTime()
        {
            string readjsonData = File.ReadAllText(savepath);
            return gameRecord;
        }
        public float j_LoadBestGameTime()
        {
            string readjsonData = File.ReadAllText(savepath);
            return bestGameRecord;
        }
        public float j_LoadStageTime(int _stage)
        {
            string readjsonData = File.ReadAllText(savepath);
            return stageRecord[_stage];
        }
        public float j_LoadBestStageTime(int _stage)
        {
            string readjsonData = File.ReadAllText(savepath);
            return bestStageRecord[_stage];
        }

        public void j_resetStageTime()
        {
            if(stageRecord.Count != 0)
            {
                for(int i = 0; i < stageManager.GetmaxStage(); i++)
                {
                    stageRecord[i] = 99999999f;
                }
                gameRecord = 99999999f;
                File.WriteAllText(savepath, writejsonData);
            }
        }

    }
}