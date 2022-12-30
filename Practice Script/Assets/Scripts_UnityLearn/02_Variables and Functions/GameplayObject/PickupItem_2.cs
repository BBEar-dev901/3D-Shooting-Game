using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_2 : MonoBehaviour
{   
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
           // Debug.Log = �α� ����
           // Destroy(gameObject) = ���ӿ�����Ʈ ����, �ı�
           Debug.Log("Pick up item");
           Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {   
        // Vector3.up = y��
        // Time.deltaTime = ������ ������ ������ �ð�
        // Space.Self = �ڽ��� ������ �ִ� ��ǥ��
        transform.Rotate(Vector3.up, 360f * Time.deltaTime, Space.Self);

        // Time.time = ������ �ð�
        // 2f * Mathf.PI = 2f = �ѹ���
        // Time.time * 2f * Mathf.PI = �ѹ��� ���µ� 1��
        // 0.5f = �Ʒ� �� ��
        // 2.5f = �⺻ ����
        float bobbingAnimationPhase = (Mathf.Sin(Time.time * 2f * Mathf.PI) * 0.5f) + 0.25f;
        transform.position = startPosition + Vector3.up * bobbingAnimationPhase;
    }
}
