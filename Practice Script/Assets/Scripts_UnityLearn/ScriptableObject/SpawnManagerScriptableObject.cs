using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create 메뉴에서 객체를 바로 만들 수 있게 메뉴 생성하기
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 2)]

// MonoBehaviour 가 아닌 ScriptableObject를 상속받아야 함
public class SpawnManagerScriptableObject : ScriptableObject
{
    // 프리팹의 이름 정하기 ( 불러올 때 이름을 정할 수 있는 변수 생성)
    public string prefabName;
    // 프리팹을 몇 개 생성할지 정할 수 있는 변수 생성
    public int numberOfPrefabsToCreate;
    // 어디에 만들어 줄지 좌표를 저장할 배열 생성
    public Vector3[] spawnPoints;
}
