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
           // Debug.Log = 로그 띄우기
           // Destroy(gameObject) = 게임오브젝트 삭제, 파괴
           Debug.Log("Pick up item");
           Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {   
        // Vector3.up = y축
        // Time.deltaTime = 프레임 사이의 간격의 시간
        // Space.Self = 자신이 가지고 있는 좌표계
        transform.Rotate(Vector3.up, 360f * Time.deltaTime, Space.Self);

        // Time.time = 누적된 시간
        // 2f * Mathf.PI = 2f = 한바퀴
        // Time.time * 2f * Mathf.PI = 한바퀴 도는데 1초
        // 0.5f = 아래 위 폭
        // 2.5f = 기본 높이
        float bobbingAnimationPhase = (Mathf.Sin(Time.time * 2f * Mathf.PI) * 0.5f) + 0.25f;
        transform.position = startPosition + Vector3.up * bobbingAnimationPhase;
    }
}
