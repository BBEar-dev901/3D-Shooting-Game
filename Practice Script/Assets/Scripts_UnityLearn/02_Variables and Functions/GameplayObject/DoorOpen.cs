using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Vector3 orginPosition;
    Vector3 MovePosition;

    bool m_OpenDoor = false; // ���� �� ���� ���� ����
    bool m_DoorMoveUpdate = false; // ���� �����̴���

    float jumpUpHeight = 2.5f; // �ִ� ����
    float moveTime = 0.5f; // ������ �ð�
    float midValue = 0f; // �߰� ��

    void Start()
    {
        orginPosition = transform.position; // ���� ������ ��ǥ
        MovePosition = transform.GetRelativeY(2.5f); // Ʈ���Ű� �۵� �Ǿ��� �� ������ ��ǥ
    }
    private void OnTriggerEnter(Collider other) // Ʈ���Ű� �ߵ� �Ǿ��� ��
    {
        // �ε��� ��ü�� �÷��̾����� Ȯ��
        if(other.tag == "Player")
        {
            // �÷��̾� ��ü�� ��ũ��Ʈ�� ������ �ִ��� Ȯ��
            Player controller = other.GetComponentInParent<Player>();
            if(controller == null)
            {
                // ������ ���� �ʴٸ� ����������
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
                

                // ������ �ִٸ� Ʈ���Ű� �۵� �Ǿ��� �� ������ ��ǥ�� �̵�
                //transform.GetChild(0).position = MovePosition;
            }
            
        }
    }//OnTriggerEnter

    private void OnTriggerExit(Collider other) // Ʈ���Ű� �۵��� ������ ��
    {
        // �ε��� ��ü�� �÷��̾����� Ȯ��
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
            

            // �±װ� Door�� ��ü�� ã�� �ٽ� ���� ��ǥ�� �̵���Ŵ
            //transform.Find("Door").position = orginPosition;
        }
    }//OnTriggerExit
    void Update()
    {
        float movedDistanceY;
        
        if(m_DoorMoveUpdate == false) // ���� �������� ���� ��
        {
            // ����������
            return;
        }
        else // ���� ������ ��
        {
            // midValue�� ������ �ð�, �ӵ� ���ϱ�
            midValue += Time.deltaTime / moveTime;
            if(midValue > 1.0f) // midValue�� 1�� ���� ��
            {
                // midValue�� 1�� �����ϰ� ���� �������� ���߱�
                midValue = 1.0f;
                m_DoorMoveUpdate = false;
            }
        }
        // Mathf.Lerp( �����ϴ� ����, �ְ� ����, �ӵ�/������ �ð�)
        movedDistanceY = Mathf.Lerp(0f, jumpUpHeight, midValue);

        // ���� ���� ��
        if(m_OpenDoor == true)
        {
            // õõ�� �ö󰡱�
            transform.GetChild(0).position = transform.GetRelativeY(movedDistanceY);
        }
        else // ���� ���� ��
        {
            // �ְ� ���̿��� ���� �ִ� ���̸� ���鼭 ��������
            transform.GetChild(0).position = transform.GetRelativeY(jumpUpHeight - movedDistanceY);
        }
        transform.GetChild(0).localEulerAngles = new Vector3(0f, midValue * 360f, 0f);
    }
}
