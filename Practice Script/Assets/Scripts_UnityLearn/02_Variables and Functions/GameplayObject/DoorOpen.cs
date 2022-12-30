using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Vector3 orginPosition;
    Vector3 MovePosition;

    bool m_OpenDoor = false; // 문을 열 건지 닫을 건지
    bool m_DoorMoveUpdate = false; // 문이 움직이는지

    float jumpUpHeight = 2.5f; // 최대 높이
    float moveTime = 0.5f; // 움직일 시간
    float midValue = 0f; // 중간 값

    void Start()
    {
        orginPosition = transform.position; // 현재 포지션 좌표
        MovePosition = transform.GetRelativeY(2.5f); // 트리거가 작동 되었을 때 움직일 좌표
    }
    private void OnTriggerEnter(Collider other) // 트리거가 발동 되었을 때
    {
        // 부딪힌 객체가 플레이어인지 확인
        if(other.tag == "Player")
        {
            // 플레이어 객체가 스크립트를 가지고 있는지 확인
            Player controller = other.GetComponentInParent<Player>();
            if(controller == null)
            {
                // 가지고 있지 않다면 돌려보내기
                return;
            }
            else
            {
                if(m_DoorMoveUpdate == true)
                {
                    if(m_OpenDoor == false)
                    {
                        midValue = 1f - midValue;
                    }
                }    
                else
                {
                    m_DoorMoveUpdate = true;
                    midValue = 0f;
                }
                
                m_OpenDoor = true;
                

                // 가지고 있다면 트리거가 작동 되었을 때 설정된 좌표로 이동
                //transform.GetChild(0).position = MovePosition;
            }
            
        }
    }//OnTriggerEnter

    private void OnTriggerExit(Collider other) // 트리거가 작동이 멈췄을 때
    {
        // 부딪힌 객체가 플레이어인지 확인
        if (other.tag == "Player")
        {
            if(m_DoorMoveUpdate == true)
            {
                if(m_OpenDoor == true)
                {
                    midValue = 1f - midValue;
                }
            }
            else
            {
                m_DoorMoveUpdate = true;
                midValue = 0f;
            }
            m_OpenDoor = false;
            

            // 태그가 Door인 객체를 찾아 다시 원래 좌표로 이동시킴
            //transform.Find("Door").position = orginPosition;
        }
    }//OnTriggerExit
    void Update()
    {
        float movedDistanceY;
        
        if(m_DoorMoveUpdate == false) // 문이 움직이지 않을 때
        {
            // 돌려보내기
            return;
        }
        else // 문이 움직일 때
        {
            // midValue에 움직일 시간, 속도 정하기
            midValue += Time.deltaTime / moveTime;
            if(midValue > 1.0f) // midValue가 1이 넘을 때
            {
                // midValue를 1로 설정하고 문의 움직임을 멈추기
                midValue = 1.0f;
                m_DoorMoveUpdate = false;
            }
        }
        // Mathf.Lerp( 시작하는 높이, 최고 높이, 속도/움직일 시간)
        movedDistanceY = Mathf.Lerp(0f, jumpUpHeight, midValue);

        // 문이 열릴 때
        if(m_OpenDoor == true)
        {
            // 천천히 올라가기
            transform.GetChild(0).position = transform.GetRelativeY(movedDistanceY);
        }
        else // 문이 닫힐 때
        {
            // 최고 높이에서 지금 있는 높이를 빼면서 내려오기
            transform.GetChild(0).position = transform.GetRelativeY(jumpUpHeight - movedDistanceY);
        }
        transform.GetChild(0).localEulerAngles = new Vector3(0f, midValue * 360f, 0f);
    }
}
