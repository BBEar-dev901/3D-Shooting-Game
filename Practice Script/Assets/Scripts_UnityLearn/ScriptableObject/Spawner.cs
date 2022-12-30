using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �������� ������ ������Ʈ ����
    public GameObject entityToSpawn;

    // �� �� ������ ���� ������ ����
    public SpawnManagerScriptableObject spawnManagerValues;

    // ������ ������Ʈ ���� �̸��� ���� ����
    int instanceNumber = 1;

    void Start()
    {
        // ������ ���� �� �� ���� �� �� �ְ� Start �Լ����� �ҷ�����
        SpawnEntities();
        // ��ũ���ͺ� ������Ʈ�� �о���⸸ ���� ����� �ȵǱ� ������ �޶����� �� �� �ִٸ� ���Ͽ� ����Ѵ�.

        // �ϳ��� �� �����ϱ�
        if(spawnManagerValues.numberOfPrefabsToCreate < 8)
        {
            spawnManagerValues.numberOfPrefabsToCreate++;
        }

    }

    // ���� ������Ʈ�� ������ �Լ�
    void SpawnEntities()
    {   
        int currentSpawnPointIndex = 0;

        // �����ؾ��� �������� ���� ��ŭ �ݺ��ϸ鼭 ����
        for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
        {
            // ��ũ��Ʈ ������Ʈ�� ����� ��ġ�� �����ϱ�
            GameObject currentEntity = Instantiate(entityToSpawn, spawnManagerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

            // ����� ��ġ�� �����ϰ� �̸� �ٲٱ�
            currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

            // �̸��� �ٲٰ� �ε����� �ϳ� �ø���
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

            instanceNumber++;
        }
    }
}
