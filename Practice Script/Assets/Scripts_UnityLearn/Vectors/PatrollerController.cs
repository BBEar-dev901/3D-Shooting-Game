using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerController : MonoBehaviour
{
    // 패트롤의 상태 지정해놓기
    enum PatrolStatus
    {
        PointChecking,
        PatrolMoving,
        Follow,
        Catch,
    }

    public Transform target;
    public PatrollerInfo data;
    // 패스 노드의 인덱스를 넣을 변수
    int nowPathNodeIndex;
    // 패트롤의 상태 지정 변수
    PatrolStatus epatrolStatus = PatrolStatus.PointChecking;
    // 패트롤의 순찰 시간을 비교할 타이머 생성
    float timer_checking_running;
    // 전환할 방향 벡터
    Vector3 patroltargetNormalVector;
    Vector3 FollowtargetNormalVector;
    // 각 패트롤의 기본 포지션
    Vector3 pivotPos;
    // 순찰 지점 저장
    Vector3 patrolPoint;
    // 시야각
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
        // 방향 벡터 만들기 , 거리 벡터 만들기
        followDirection = (target.position - transform.position).normalized; // 방향 벡터
        currentDistance = (target.position - transform.position).magnitude; // 거리 벡터
        // 쫓아갈 거리 계산에 따른 행동 양식 정해주기
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
                // 패트롤의 상태가 도착해서 순찰하는 중일때
                // 타이머에 시간 축적시키기
                timer_checking_running += Time.deltaTime;

                if (timer_checking_running > data.checkingTime)
                {   // 타이머가 지정해둔 순찰 시간을 넘지 않았을 때, 업데이트 함수를 종료시켜 계속 시간 축적시켜주기
                    return;
                }
                else
                {   // 타이머가 지정해둔 순찰 시간을 넘었을 때
                    // 타이머 초기화 시키기
                    timer_checking_running = 0f;
                    // 상태 움직임으로 바꿔주기
                    epatrolStatus = PatrolStatus.PatrolMoving;
                    // 다음 노드로 바꿔주기
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

        // 방향 전환 속도 * 프레임사이 간격 시간
        float singleStep = data.towardFactor * Time.deltaTime;
        // 다음 노드를 바라보며 조금씩 회전하게 만들기 ( 패트롤의 z축, 타겟의 방향, 회전속도, 이건 잘 모름)
        Vector3 newLookAtPosition = Vector3.RotateTowards(transform.forward, patroltargetNormalVector, singleStep, 0f);

        patroltargetNormalVector = (data.pathNode[nowPathNodeIndex] + pivotPos - transform.position).normalized;
        // 다음 노드를 바라보게 하는 LookAt 함수
        transform.LookAt(transform.position + newLookAtPosition);

        // 패트롤이 노드를 바라보며 조금씩 회전한 후 움직일 때
        transform.position = Vector3.MoveTowards(transform.position,
                                                data.pathNode[nowPathNodeIndex] + pivotPos,
                                                Time.deltaTime * data.patrolSpeed);

        // 패트롤이 다음 노드의 위치에 도착 했을 때
        if (transform.position == data.pathNode[nowPathNodeIndex] + pivotPos)
        {
            // 패트롤의 상태를 순찰 중으로 지정해주기
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
