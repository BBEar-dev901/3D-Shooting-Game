using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 프리팹을 가져올 오브젝트 변수
    public GameObject entityToSpawn;

    // 몇 개 가져올 건지 저장할 변수
    public SpawnManagerScriptableObject spawnManagerValues;

    // 가져온 오브젝트 들의 이름에 붙힐 변수
    int instanceNumber = 1;

    void Start()
    {
        // 게임이 시작 될 때 생성 될 수 있게 Start 함수에서 불러오기
        SpawnEntities();
        // 스크립터블 오브젝트는 읽어오기만 되지 쓰기는 안되기 때문에 달라져야 할 게 있다면 파일에 써야한다.

        // 하나씩 더 생성하기
        if(spawnManagerValues.numberOfPrefabsToCreate < 8)
        {
            spawnManagerValues.numberOfPrefabsToCreate++;
        }

    }

    // 게임 오브젝트를 생성할 함수
    void SpawnEntities()
    {   
        int currentSpawnPointIndex = 0;

        // 생성해야할 프리팹의 갯수 만큼 반복하면서 생성
        for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
        {
            // 스크립트 오브젝트에 저장된 위치에 생성하기
            GameObject currentEntity = Instantiate(entityToSpawn, spawnManagerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

            // 저장된 위치에 생성하고 이름 바꾸기
            currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

            // 이름을 바꾸고 인덱스를 하나 늘리기
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

            instanceNumber++;
        }
    }
}
