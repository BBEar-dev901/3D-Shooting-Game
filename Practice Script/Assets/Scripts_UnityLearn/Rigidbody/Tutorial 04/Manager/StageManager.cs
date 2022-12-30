using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Minigame.TicTacToe;

namespace Lecture.RigidBody.Tutorial04
{
    public enum PlayState
    {
        Idle,
        Ready,
        Run,
        End,
    }
    public class StageManager : MonoBehaviour
    {
        SaveSystem saveSystem;
        DisplayMessage m_displayMessage = new DisplayMessage();
        PlatoonManager m_platoonManager;
        ReadyCountUI m_readyCountUI;
        BuffManager m_buffManager;
        const float readyTime = 3f;
        StageDatas m_stageDatas;
        int currentStage = 0;
        int maxStage;
        GameObject f_marker;
        GameObject f_marker1;
        public static PlayState playstate = PlayState.Idle;

        CameraMove cameraControl;

        public MinigameResult ticTacToeResult;
        
        [SerializeField] EndGameUI m_EndGameUI;
        [SerializeField] CharacterStat m_CharacterStat;

        [SerializeField] GameObject gameTicTacToe;

        Vector3 charaP;

        private void Start()
        {
            cameraControl = Camera.main.GetComponent<CameraMove>();

            saveSystem = GetComponent<SaveSystem>();
            m_stageDatas = GetComponent<StageDatas>();
            maxStage = m_stageDatas.Stages.Length;
            m_platoonManager = GameObject.Find("Platoon Manager").GetComponent<PlatoonManager>();
            m_readyCountUI = GameObject.Find("GameReadyUI").GetComponent<ReadyCountUI>();
            m_buffManager = GameObject.Find("Buff Indicator").GetComponent<BuffManager>();
            f_marker = GameObject.Find("Plane Marker").gameObject;
            f_marker1 = GameObject.Find("Plane Marker 2").gameObject;
            charaP = m_CharacterStat.gameObject.transform.position;

            f_marker.SetActive(false);
            f_marker1.SetActive(false);
            saveSystem.Save();
            saveSystem.stageTimeClear();
            m_readyCountUI.countDownStart();
            StartCoroutine(StartStage(0, 5f));
        }
        public int GetmaxStage()
        {
            return maxStage;
        }
        IEnumerator StartStage(int _stageIndex, float _delayTime = readyTime)
        {
            ticTacToeResult.result = 0;

            if (_stageIndex == 0)
            {
                LapTimeUI.Instance.ResetTimer(LapTimerType.All);
                LapTimeUI.Instance.transform.GetChild(0).gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1.0f);

            playstate = PlayState.Idle;

            m_displayMessage.message = "Stage " + (currentStage + 1) + " Start...";
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);

            yield return new WaitForSeconds(_delayTime);

            ConstructStageData();
            playstate = PlayState.Ready;
            if (_stageIndex != 0)
            {
                LapTimeUI.Instance.ResetTimer(LapTimerType.Stage);
            }

            // 미니맵 배경
            if (_stageIndex == 0)
            {
                f_marker.SetActive(true);
            }
            else
            {
                f_marker.SetActive(false);
                f_marker1.SetActive(true);
            }

            yield return new WaitForSeconds(1.5f);

            playstate = PlayState.Run;
            cameraControl.SetCameraType(CameraType.CombatCamera);
            m_platoonManager.CommandRush();
        }

        public void OnStageFailed()
        {
            cameraControl.SetCameraType(CameraType.NormalCamera);
            playstate = PlayState.End;
            saveSystem.Save();

            LapTimeUI.Instance.ResetTimer(LapTimerType.All);
            PickupSpawner.Instance.InactiveBuffAll();

            m_displayMessage.message = "<color=red>Mission FAIL</color>\n<size=20>The End</size>";
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);

            m_platoonManager.CommandRush(false);

            m_EndGameUI.ShowResult(false);
        }

        void ConstructStageData()
        {
            int countObj = m_stageDatas.Stages[currentStage].objectives.Length;
            for(int i = 0; i < countObj; ++i)
            {
                Objective _obj = Instantiate(m_stageDatas.Stages[currentStage].objectives[i]);
                ObjectiveManager.Instance.AddObjective(_obj);
            }
            ++currentStage;
        }
        public void OnStageCleared()
        {
            cameraControl.SetCameraType(CameraType.NormalCamera);
            playstate = PlayState.End;
            //스테이지 시간 저장
            PlayerPrefs.SetFloat("stage" + (currentStage - 1), LapTimeUI.Instance.getStageTime());
            saveSystem.UpdateRecord((currentStage - 1));

            m_displayMessage.message = "Stage " + currentStage + " cleared...";
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);
            PickupSpawner.Instance.InactiveBuffAll();

            CleanupStageData();
        }
        void CleanupStageData()
        {
            DisplayObjectiveManager.Instance.UnregisterAllObjectives();
            m_platoonManager.Withdraw();

            if(currentStage < maxStage)
            {
                // Scene 추가
                //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                //SceneManager.sceneUnloaded += OnMinigameEnded;

                // Prefabs 1
                //Instantiate(gameTicTacToe, transform);
                //return;
                // Prefabs 2
                //MinigameStart();

                //미니게임 없는 것
                 NextStage();
            }
            else
            {
                GameWin();
            }
        }
        void MinigameStart()
        {
            GameObject gameObject = Instantiate(gameTicTacToe);
            gameObject.GetComponent<GameManager>().SetStageManager(this);
        }
        public void OnMinigameEnded(/*Scene minigameScene*/ int _iResult)
        {
            // Scene 추가
            //SceneManager.sceneUnloaded += OnMinigameEnded;
            //Debug.Log("OnMinigameEnded ( " + ticTacToeResult.result + " ) : ( " + minigameScene.name + minigameScene.buildIndex + " )");
            //if(ticTacToeResult.result == 1)
            //{
            //    m_buffManager.RewardSetBuff();
            //}

            // Prefabs 추가
            if (_iResult == 1)
            {
                m_buffManager.RewardSetBuff();
            }
            NextStage();
        }
        void NextStage()
        {
            m_readyCountUI.countDownStart();
            StartCoroutine(StartStage(currentStage, 10f));
        }
        void GameWin()
        {
            //게임 시간 저장
            PlayerPrefs.SetFloat("GameTime", LapTimeUI.Instance.getGameTime());
            saveSystem.Save();

            m_displayMessage.message = "<color=green>Mission CLEAR</color>\n<size=20>The End</size>";
            DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);
            PickupSpawner.Instance.InactiveBuffAll();

            m_EndGameUI.ShowResult(true);
        }

        public void OnRestartGame()
        {
            ObjectiveManager.Instance.ResetGameObjective();
            DisplayObjectiveManager.Instance.UnregisterAllObjectives();
            saveSystem.stageTimeClear();

            m_buffManager.ResetBuff();
            m_readyCountUI.firstStage = true;
            m_readyCountUI.countDownStart();
            m_platoonManager.CommandRush(false);
            m_platoonManager.Withdraw();
            m_CharacterStat.CurrentHp = m_CharacterStat.MaxHp;
            m_CharacterStat.gameObject.SetActive(true);
            m_CharacterStat.gameObject.transform.position = charaP;

            currentStage = 0;
            f_marker.SetActive(false);
            f_marker1.SetActive(false);
            StartCoroutine(StartStage(currentStage));   
        }

    }
}