using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lecture.RigidBody.Tutorial04;

[CreateAssetMenu(fileName = "MiniGameResult", menuName ="ScriptableObjects/Mini Game Result", order = 10)]
public class MinigameResult : ScriptableObject
{
    public float[] healReward = new float[2];
    public float[] offecseReward = new float[2];
    public float[] defenceReward = new float[2];

    public int[] rewardCount = new int[(int)BuffType.MAX];

    public string gameName;
    [Tooltip("0(Not Set), 1(½Â), 2(ÆÐ), 3(¹«½ÂºÎ)")]
    public int result;
}
