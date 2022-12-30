using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerController : MonoBehaviour
{
    // ��Ʈ���� ���� �����س���
    enum PatrolStatus
    {
        PointChecking,
        PatrolMoving,
        Follow,
        Catch,
    }

    public Transform target;
    public PatrollerInfo data;
    // �н� ����� �ε����� ���� ����
    int nowPathNodeIndex;
    // ��Ʈ���� ���� ���� ����
    PatrolStatus epatrolStatus = PatrolStatus.PointChecking;
    // ��Ʈ���� ���� �ð��� ���� Ÿ�̸� ����
    float timer_checking_running;
    // ��ȯ�� ���� ����
    Vector3 patroltargetNormalVector;
    Vector3 FollowtargetNormalVector;
    // �� ��Ʈ���� �⺻ ������
    Vector3 pivotPos;
    // ���� ���� ����
    Vector3 patrolPoint;
    // �þ߰�
    float visionDecisionValue;

    Vector3 followDirection;
    float currentDistance;



    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        pivotPos = transform.position;
        epatrolStatus = PatrolStatus.PointChecking;
        timer_checking_running = 0f;
        nowPathNodeIndex = Random.Range(0,data.pathNode.Length);
        visionDecisionValue = Mathf.Cos(data.fieldOfVision * 0.5f);
    }

    void Update()
    {
        // ���� ���� ����� , �Ÿ� ���� �����
        followDirection = (target.position - transform.position).normalized; // ���� ����
        currentDistance = (target.position - transform.position).magnitude; // �Ÿ� ����
        // �Ѿư� �Ÿ� ��꿡 ���� �ൿ ��� �����ֱ�
        if (currentDistance > data.pursuerDistance)
        {
            if(epatrolStatus != PatrolStatus.PointChecking)
            {
                epatrolStatus = PatrolStatus.PatrolMoving;
            }
        }
        else if (currentDistance > data.stoppingDistance)
        {
            if (Vector3.Dot(transform.forward, followDirection) > visionDecisionValue)
            {
                epatrolStatus = PatrolStatus.Follow;
            }
        }
        else
        {
            epatrolStatus = PatrolStatus.Catch;
        }


        switch (epatrolStatus)
        {
            case PatrolStatus.PointChecking:
                // ��Ʈ���� ���°� �����ؼ� �����ϴ� ���϶�
                // Ÿ�̸ӿ� �ð� ������Ű��
                timer_checking_running += Time.deltaTime;

                if (timer_checking_running > data.checkingTime)
                {   // Ÿ�̸Ӱ� �����ص� ���� �ð��� ���� �ʾ��� ��, ������Ʈ �Լ��� ������� ��� �ð� ���������ֱ�
                    return;
                }
                else
                {   // Ÿ�̸Ӱ� �����ص� ���� �ð��� �Ѿ��� ��
                    // Ÿ�̸� �ʱ�ȭ ��Ű��
                    timer_checking_running = 0f;
                    // ���� ���������� �ٲ��ֱ�
                    epatrolStatus = PatrolStatus.PatrolMoving;
                    // ���� ���� �ٲ��ֱ�
                    nowPathNodeIndex = Random.Range(0, data.pathNode.Length);
                }
                
                break;

            case PatrolStatus.PatrolMoving:
                PatrolMove();
                break;

            case PatrolStatus.Follow:
                PatrolFollow();
                break;

            case PatrolStatus.Catch:
                break;

            default:
                break;
        }

        Debug.DrawLine(transform.position,
            transform.position + Vector3.RotateTowards(transform.forward, transform.right, data.fieldOfVision * 0.5f, 0f) * data.pursuerDistance,
            Color.red);

        Debug.DrawRay(transform.position,
            Vector3.RotateTowards(transform.forward, -transform.right, data.fieldOfVision * 0.5f, 0f) * data.pursuerDistance,
            Color.blue);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, data.pursuerDistance);
    }

    void PatrolMove() {

        // ���� ��ȯ �ӵ� * �����ӻ��� ���� �ð�
        float singleStep = data.towardFactor * Time.deltaTime;
        // ���� ��带 �ٶ󺸸� ���ݾ� ȸ���ϰ� ����� ( ��Ʈ���� z��, Ÿ���� ����, ȸ���ӵ�, �̰� �� ��)
        Vector3 newLookAtPosition = Vector3.RotateTowards(transform.forward, patroltargetNormalVector, singleStep, 0f);

        patroltargetNormalVector = (data.pathNode[nowPathNodeIndex] + pivotPos - transform.position).normalized;
        // ���� ��带 �ٶ󺸰� �ϴ� LookAt �Լ�
        transform.LookAt(transform.position + newLookAtPosition);

        // ��Ʈ���� ��带 �ٶ󺸸� ���ݾ� ȸ���� �� ������ ��
        transform.position = Vector3.MoveTowards(transform.position,
                                                data.pathNode[nowPathNodeIndex] + pivotPos,
                                                Time.deltaTime * data.patrolSpeed);

        // ��Ʈ���� ���� ����� ��ġ�� ���� ���� ��
        if (transform.position == data.pathNode[nowPathNodeIndex] + pivotPos)
        {
            // ��Ʈ���� ���¸� ���� ������ �������ֱ�
            epatrolStatus = PatrolStatus.PointChecking;
        }

    }

    void PatrolFollow()
    {
        transform.position += followDirection * Time.deltaTime * data.moveFactor;
        float P_singleStep = data.P_towardFactor * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, followDirection, P_singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
