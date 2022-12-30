using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    enum PursuerState
    {
        Patrol,
        Follow,
        Catch,
    }

    // 필요한 변수들 모음

    public Transform target;
    // 돌아올 좌표 저장
    Vector3 patrolPoint;
    // 인지 거리값
    float pursuerDistance = 10f;
    // 멀어지면 따라가지 않을 거리 값
    float stoppingDistance = 1.5f;
    // 이동 속도
    [SerializeField] float moveFactor = 5f;
    // 기본 행동 양식 지정
    PursuerState eStateBehavior = PursuerState.Patrol;

    float towardFactor = Mathf.PI * 2;

    // 시야각 90도
    float fieldOfVision = Mathf.PI * 0.5f;
    // 시야각 안에 들어오는지 확인
    float visionDecisionValue;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoint = transform.position;
        // 시야각의 cos 값
        visionDecisionValue = Mathf.Cos(fieldOfVision * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 타겟과의 거리 계산
        Vector3 followDirection = (target.position - transform.position).normalized;
        float currentDistance = (target.position - transform.position).magnitude;

        // 타겟과 나 사이의 거리에 따라 행동 지정
        if( currentDistance > pursuerDistance)
        {
            eStateBehavior = PursuerState.Patrol;
        }
        else if( currentDistance > stoppingDistance )
        {   
            if (eStateBehavior == PursuerState.Patrol)
            {
                if (Vector3.Dot(transform.forward, followDirection) > visionDecisionValue)
                { // 자신의 z축 , 타겟의 방향의 곱이 시야각보다 클때 (시야각에 들어올 때) 
                    eStateBehavior = PursuerState.Follow;
                }
                else
                {
                    eStateBehavior = PursuerState.Patrol;
                }
            }
            else
            {
                eStateBehavior = PursuerState.Follow;
            }
        }
        else
        {
            eStateBehavior=PursuerState.Catch;
        }

        // 상태 값에 따라 행동 정의
        switch (eStateBehavior) 
        {
            case PursuerState.Follow:
                // 방향 벡터 * 이동 속도로 이동
                transform.position += followDirection * Time.deltaTime * moveFactor;
                break;
            case PursuerState.Patrol:
                followDirection = patrolPoint - transform.position;
                // Vector3.MoveTowards()를 사용하면 정확한 위치로 다시 되돌아간다.
                transform.position = Vector3.MoveTowards(transform.position, patrolPoint, Time.deltaTime * moveFactor);
                followDirection = followDirection.normalized;

                break;
            case PursuerState.Catch:
                break;
            
            default:
                break;
        }

        float singleStep = towardFactor * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, followDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // 디버그 가시 범위 표시하기
        Debug.DrawLine(transform.position, 
            transform.position + Vector3.RotateTowards(transform.forward, transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.red);

        Debug.DrawRay(transform.position,
            Vector3.RotateTowards(transform.forward, -transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.blue);
    }
    // Gizmo Debug에서 원 그려서 범위 확인하기 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pursuerDistance);
    }
}
