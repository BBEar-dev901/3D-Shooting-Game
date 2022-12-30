using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Minigame.TicTacToe
{
    public class GridSpace : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI buttonText;
        private GameManager gameManager;

        private void Start()
        {
            button.onClick.AddListener(SetSpace);
        }
        public void SetGameManagerReference(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }
        private void SetSpace()
        {
            if (gameManager.playerMove == true)
            {
                buttonText.text = gameManager.GetPlayerSide();
                button.interactable = false;
                gameManager.EndTurn();
            }
        }
    }
}