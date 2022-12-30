using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Minigame.TicTacToe;

namespace Lecture.RigidBody.Tutorial04
{
    public class UIPractice : MonoBehaviour
    {
        [Header("UI Component")]
        [SerializeField] Image npcIconUI;
        [SerializeField] TextMeshProUGUI npcNameUI;
        [SerializeField] TextMeshProUGUI npcMessageUI;
        [SerializeField] GameObject[] rewardLayoutObject;
        [SerializeField] Image[] rewardIconsUI;
        [SerializeField] TextMeshProUGUI[] rewardCount;
        [SerializeField] Button endButtonUI;

        [Header("Data Sets")]
        [SerializeField] UiPracticScriptableObject data;
        [SerializeField] GameObject tictactoe;

        int textindex;
        bool bEntrance;
        string[] scriptText;
        float typingSpeed = 0.1f;

        private void Awake()
        {
            endButtonUI.gameObject.GetComponent<Button>().onClick.AddListener(OnEndbuttonClicked);
            bEntrance = true;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(npcMessageUI.text == scriptText[textindex])
                {
                    NextScript();
                }
                else
                {
                    StopAllCoroutines();
                    npcMessageUI.text = scriptText[textindex];
                    if(textindex == scriptText.Length -1)
                    {
                        endButtonUI.gameObject.SetActive(true);
                    }
                }
            }
        }
        void NextScript()
        {
            if(textindex < scriptText.Length - 1)
            {
                ++textindex;
                npcMessageUI.text = "";
                StartCoroutine(TypingScript());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }


        private void OnEnable()
        {
            DialogueInitialize();
            StartDialogue();
        }
        void StartDialogue()
        {
            textindex = 0;
            StartCoroutine(TypingScript());
        }
        IEnumerator TypingScript()
        {
            foreach(var item in scriptText[textindex])
            {
                npcMessageUI.text += item;
                yield return new WaitForSeconds(typingSpeed);
            }
            if(textindex == scriptText.Length - 1)
            {
                endButtonUI.gameObject.SetActive(true);
            }
        }


        private void OnDisable()
        {
            bEntrance = true;
            textindex = 0;
        }
        private void DialogueInitialize()
        {
            textindex = 0;
            npcIconUI.sprite = data.NPCIcon;
            npcNameUI.text = data.NPCName;
            int i = 0;
            for(; i<data.rewardList.Length; ++i)
            {
                rewardIconsUI[i].sprite = data.rewardList[i].RewardIcon;
                rewardCount[i].text = "X " + data.rewardList[i].rewardCount;
                rewardLayoutObject[i].SetActive(true);
            }
            for(; i < rewardLayoutObject.Length; ++i)
            {
                rewardLayoutObject[i].SetActive(false);
            }
            if(bEntrance == true)
            {
                scriptText = data.entranceMassages;
                endButtonUI.GetComponentInChildren<TextMeshProUGUI>().text = "Go";
            }
            else
            {
                scriptText = data.exitMessages;
                endButtonUI.GetComponentInChildren<TextMeshProUGUI>().text = "Bye";
            }
            npcMessageUI.text = "";

            if(scriptText.Length > 1)
            {
                endButtonUI.gameObject.SetActive(false);
            }
        }
        void OnEndbuttonClicked()
        {
            Debug.Log("EndButtonOnClicked");
            if(bEntrance == true)
            {
                MinigameStart();
            }
            else
            {

            }
            gameObject.SetActive(false);
        }
        void MinigameStart()
        {
            GameObject gameObject = Instantiate(tictactoe);
            gameObject.GetComponent<GameManager>().SetUIPractice(this);
        }
        public void OnMinigameEnded(int _iResult)
        {
            bEntrance = false;
            gameObject.SetActive(true);
            Debug.Log("Win!");
        }
    }
}