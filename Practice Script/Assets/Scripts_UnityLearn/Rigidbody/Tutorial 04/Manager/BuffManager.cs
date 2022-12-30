using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public enum BuffType
    {
        HEAL,
        DEFENCE,
        OFFENSE,
        MAX,
    }
    public struct BuffData
    {
        public BuffType type;
        public float time;
        public float value;
    }
    public class BuffManager : MonoBehaviour
    {
        CharacterStat character;

        public Image[] cooltimeUI = new Image[(int)BuffType.MAX];
        public Text[] cooltimeText = new Text[(int)BuffType.MAX];
        public Text[] buffstackText = new Text[(int)BuffType.MAX];
 
        static Queue<BuffData>[] buffQueue = new Queue<BuffData>[(int)BuffType.MAX];

        float[] cooltime = new float[(int)BuffType.MAX];
        float[] cooltimeMax = new float[(int)BuffType.MAX];

        StageManager stageManager;

        private void Start()
        {
            for(int i = 0; i < (int)BuffType.MAX; ++i)
            {
                cooltime[i] = -1f;
                cooltimeUI[i].fillAmount = 1f;
                buffQueue[i] = new Queue<BuffData>();
                buffstackText[i].text = "";
                cooltimeText[i].text = "";
            }
            character = GameObject.Find("Cannon").GetComponent<CharacterStat>();
            stageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
        }
        static public void AddBuffData(BuffData _buffData)
        {
            buffQueue[(int)(_buffData.type)].Enqueue(_buffData);
        }
        public BuffData SetBuffdata(BuffType _type, float _time, float _value)
        {
            BuffData buffData;
            buffData.type = _type;
            buffData.time = _time;
            buffData.value = _value;
            return buffData;
        }
        public void RewardSetBuff()
        {
            for(int i = 0; i < stageManager.ticTacToeResult.rewardCount[0]; i++)
            {
                AddBuffData(SetBuffdata(BuffType.HEAL, stageManager.ticTacToeResult.healReward[0], stageManager.ticTacToeResult.healReward[1]));
            }
            for (int i = 0; i < stageManager.ticTacToeResult.rewardCount[1]; i++)
            {
                AddBuffData(SetBuffdata(BuffType.OFFENSE, stageManager.ticTacToeResult.offecseReward[0], stageManager.ticTacToeResult.offecseReward[1]));
            }
            for (int i = 0; i < stageManager.ticTacToeResult.rewardCount[2]; i++)
            {
                AddBuffData(SetBuffdata(BuffType.DEFENCE, stageManager.ticTacToeResult.defenceReward[0], stageManager.ticTacToeResult.defenceReward[1]));
            }
        }
        private void Update()
        {
            for(int i = 0; i < (int)BuffType.MAX; ++i)
            {
                if(buffQueue[i].Count > 0 || buffQueue[i].Count == 0)
                {
                    if(buffQueue[i].Count == 0)
                    {
                        buffstackText[i].text = "";
                    }
                    else
                    {
                        buffstackText[i].text = buffQueue[i].Count.ToString();
                    }
                }
                // Å¸ÀÌ¸Ó
                if (StageManager.playstate == PlayState.Run)
                {
                    if (cooltime[i] > 0f)
                    {
                        cooltime[i] -= Time.deltaTime;
                        cooltimeUI[i].fillAmount = cooltime[i] < 0f ? 1f : 1f - cooltime[i] / cooltimeMax[i];
                        cooltimeText[i].text = (Mathf.CeilToInt(cooltime[i])).ToString();

                        if (cooltime[i] < 0 || cooltime[i] == 0)
                        {
                            if (buffQueue[i].Count > 0)
                            {
                                BuffData data = buffQueue[i].Dequeue();
                                cooltimeMax[i] = data.time;
                                cooltime[i] = data.time;
                                cooltimeUI[i].fillAmount = 0f;

                                character.ActiveBuff(data.type, data.value);
                            }
                            else
                            {
                                cooltime[i] = -1f;
                                cooltimeUI[i].fillAmount = 1f;
                                cooltimeText[i].text = "";

                                character.InactiveBuff((BuffType)i);
                            }
                        }
                    }
                    else if (buffQueue[i].Count > 0)
                    {
                        BuffData data = buffQueue[i].Dequeue();
                        cooltimeMax[i] = data.time;
                        cooltime[i] = data.time;
                        cooltimeUI[i].fillAmount = 0f;

                        character.ActiveBuff(data.type, data.value);
                    }
                }

            }
        }
        public void ResetBuff()
        {
            for (int i = 0; i < (int)BuffType.MAX; ++i)
            {
                cooltime[i] = -1f;
                cooltimeUI[i].fillAmount = 1f;
                buffstackText[i].text = "";
                cooltimeText[i].text = "";
                buffQueue[i].Clear();
            }
        }


    }
}