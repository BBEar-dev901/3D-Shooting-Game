using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lecture.RigidBody.Tutorial04;


namespace Minigame.TicTacToe
{
    [System.Serializable]
    public class Player
    {
        public Image panel;
        public TextMeshProUGUI text;
        public Button button;
    }
    [System.Serializable]
    public class PlayerColor
    {
        public Color panelColor;
        public Color textColor;
    }
    public class GameManager : MonoBehaviour
    {
        public TextMeshProUGUI[] buttonList;
        public GameObject gameOverPanel;
        public TextMeshProUGUI gameOverText;
        public GameObject RestartButton;
        public GameObject startIndication;

        public Player playerX;
        public Player playerO;
        public PlayerColor activePlayerColor;
        public PlayerColor inactivePlayerColor;

        public MinigameResult gameResult;

        public bool playerMove;
        public float delay;
        int value;
        int moveCount;
        string playerside;
        string computerside;

        StageManager stageManager;
        UIPractice UiPractice;

        int _iResult = 0;


        private void Awake()
        {
            SetGameManagerReferenceOnButtons();
            RestartButton.SetActive(false);
            gameOverPanel.SetActive(false);
            moveCount = 0;
            playerMove = true;
        }
        private void Update()
        {
            if (playerMove == false)
            {
                delay += Time.deltaTime;
                if (delay > 1.5f)
                {
                    value = UnityEngine.Random.Range(0, buttonList.Length);
                    if (buttonList[value].GetComponentInParent<Button>().interactable == true)
                    {
                        buttonList[value].text = GetComputerSide();
                        buttonList[value].GetComponentInParent<Button>().interactable = false;
                        EndTurn();
                    }
                }
            }
        }
        void SetGameManagerReferenceOnButtons()
        {
            for (int i = 0; i < buttonList.Length; ++i)
            {
                buttonList[i].GetComponentInParent<GridSpace>().SetGameManagerReference(this);
            }
        }
        public string GetPlayerSide()
        {
            return playerside;
        }
        public void EndTurn()
        {
            moveCount++;
            //playerside
            if (buttonList[0].text == playerside && buttonList[1].text == playerside && buttonList[2].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[3].text == playerside && buttonList[4].text == playerside && buttonList[5].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[6].text == playerside && buttonList[7].text == playerside && buttonList[8].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[0].text == playerside && buttonList[3].text == playerside && buttonList[6].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[1].text == playerside && buttonList[4].text == playerside && buttonList[7].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[2].text == playerside && buttonList[5].text == playerside && buttonList[8].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[0].text == playerside && buttonList[4].text == playerside && buttonList[8].text == playerside)
            {
                GameOver();
            }
            else if (buttonList[2].text == playerside && buttonList[4].text == playerside && buttonList[6].text == playerside)
            {
                GameOver();
            }
            //computerside
            else if (buttonList[0].text == computerside && buttonList[1].text == computerside && buttonList[2].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[3].text == computerside && buttonList[4].text == computerside && buttonList[5].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[6].text == computerside && buttonList[7].text == computerside && buttonList[8].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[0].text == computerside && buttonList[3].text == computerside && buttonList[6].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[1].text == computerside && buttonList[4].text == computerside && buttonList[7].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[2].text == computerside && buttonList[5].text == computerside && buttonList[8].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[0].text == computerside && buttonList[4].text == computerside && buttonList[8].text == computerside)
            {
                GameOver();
            }
            else if (buttonList[2].text == computerside && buttonList[4].text == computerside && buttonList[6].text == computerside)
            {
                GameOver();
            }
            else if (moveCount >= 9)
            {
                GameOver("draw");
            }
            else
            {
                ChangeSides();
                delay = 0f;
            }
        }
        void ChangeSides()
        {
            playerMove = (playerMove == true) ? false : true;
            if (playerMove == true)
            {
                if (playerside == "X")
                {
                    SetPlayerColors(playerX, playerO);
                }
                else
                {
                    SetPlayerColors(playerO, playerX);
                }

            }
            else
            {
                if (playerside == "O")
                {
                    SetPlayerColors(playerX, playerO);
                }
                else
                {
                    SetPlayerColors(playerO, playerX);
                }
            }
        }
        void GameOver()
        {
            if (playerMove == true)
            {
                //gameResult.result = 1;
                _iResult = 1;
                GameOver(playerside);
            }
            else
            {
                //gameResult.result = 2;
                _iResult = 2;
                GameOver(computerside);
            }
        }
        void GameOver(string winningPlayer)
        {
            SetBoardInteractable(false);
            if (winningPlayer == "draw")
            {
                //gameResult.result = 3;
                _iResult = 3;
                SetGameOverText("Draw!");
                SetPlayerColorsInactive();
            }
            else
            {
                SetGameOverText(winningPlayer + " Wins!");
            }
            RestartButton.SetActive(true);
        }
        void SetBoardInteractable(bool bEnable)
        {
            for (int i = 0; i < buttonList.Length; i++)
            {
                buttonList[i].GetComponentInParent<Button>().interactable = bEnable;
            }
        }
        void SetGameOverText(string value)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = value;
        }
        public void SetStartingSide(string startingSide)
        {
            playerside = startingSide;
            if (playerside == "X")
            {
                computerside = "O";
                SetPlayerColors(playerX, playerO);
            }
            else
            {
                computerside = "X";
                SetPlayerColors(playerO, playerX);
            }
            StartGame();
        }
        public string GetComputerSide()
        {
            return computerside;
        }
        void StartGame()
        {
            SetBoardInteractable(true);
            SetPlayerButtons(false);
            startIndication.SetActive(false);
        }
        public void SetResetGame()
        {
            // Scene 추가
            //SceneManager.UnloadSceneAsync(1);
            //return;

            // Prefabs 1
            // StageManager stageManager = transform.parent.gameObject.GetComponent<StageManager>();
            // Prefabs 2
            //stageManager.OnMinigameEnded(_iResult);
            UiPractice.OnMinigameEnded(_iResult);
            Destroy(this.gameObject);
            return;

            // 개인적 실행
            moveCount = 0;
            playerMove = true;
            delay = 0f;
            _iResult = 0;

            gameOverPanel.SetActive(false);
            RestartButton.SetActive(false);
            SetPlayerButtons(true);
            SetPlayerColorsInactive();
            startIndication.SetActive(true);

            for (int i = 0; i < buttonList.Length; i++)
            {
                buttonList[i].text = "";
            }
        }
        public void SetStageManager(StageManager _stageManager)
        {
            stageManager = _stageManager;
        }
        public void SetUIPractice(UIPractice _ui)
        {
            UiPractice = _ui;
        }
        void SetPlayerButtons(bool bEnable)
        {
            playerX.button.interactable = bEnable;
            playerO.button.interactable = bEnable;
        }
        void SetPlayerColors(Player newPlayer, Player oldPlayer)
        {
            newPlayer.panel.color = activePlayerColor.panelColor;
            newPlayer.text.color = activePlayerColor.textColor;
            oldPlayer.panel.color = inactivePlayerColor.panelColor;
            oldPlayer.text.color = inactivePlayerColor.textColor;
        }
        void SetPlayerColorsInactive()
        {
            playerX.panel.color = inactivePlayerColor.panelColor;
            playerX.text.color = inactivePlayerColor.textColor;
            playerO.panel.color = inactivePlayerColor.panelColor;
            playerO.text.color = inactivePlayerColor.textColor;
        }
    }
}