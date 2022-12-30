using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create �޴����� ��ü�� �ٷ� ���� �� �ְ� �޴� �����ϱ�
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 2)]

// MonoBehaviour �� �ƴ� ScriptableObject�� ��ӹ޾ƾ� ��
public class SpawnManagerScriptableObject : ScriptableObject
{
    // �������� �̸� ���ϱ� ( �ҷ��� �� �̸��� ���� �� �ִ� ���� ����)
    public string prefabName;
    // �������� �� �� �������� ���� �� �ִ� ���� ����
    public int numberOfPrefabsToCreate;
    // ��� ����� ���� ��ǥ�� ������ �迭 ����
    public Vector3[] spawnPoints;
}
