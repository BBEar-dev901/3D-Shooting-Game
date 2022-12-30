using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerPatrolController : MonoBehaviour
{
    enum PatrolStatus
    {
        PointChecking,
        PatrolMoving,
    }
    enum PursuerState
    {
        Patrol,
        Follow,
        Catch,
    }

    public Transform[] pathNode;
    float patrolSpeed = 2f;
    int nowPathNodeIndex;
    PatrolStatus epatrolStatus;
    float checkingTime = 1f;
    float timer_checking_running;

    Vector3 targetNormalVector;
    float towardFactor = Mathf.PI;

    //
    public Transform target;
    Vector3 patrolPoint;
    float pursuerDistance = 10f;
    float stoppingDistance = 1.5f;
    [SerializeField] float moveFactor = 5f;
    PursuerState eStateBehavior = PursuerState.Patrol;
    float P_towardFactor = Mathf.PI * 2;
    float fieldOfVision = Mathf.PI * 0.5f;
    float visionDecisionValue;

    void Start()
    {
        transform.position = pathNode[0].position;
        epatrolStatus = PatrolStatus.PointChecking;
        timer_checking_running = 0f;
        nowPathNodeIndex = 0;
        patrolPoint = pathNode[nowPathNodeIndex].position;

        visionDecisionValue = Mathf.Cos(fieldOfVision * 0.5f);
    }

    void Update()
    {

        Vector3 followDirection = (target.position - transform.position).normalized;
        float currentDistance = (target.position - transform.position).magnitude;

        if (currentDistance > pursuerDistance)
        {
            eStateBehavior = PursuerState.Patrol;
        }
        else if (currentDistance > stoppingDistance)
        {
            if (eStateBehavior == PursuerState.Patrol)
            {
                if (Vector3.Dot(transform.forward, followDirection) > visionDecisionValue)
                {
                    eStateBehavior = PursuerState.Follow;
                }
            }
        }
        else
        {
            eStateBehavior = PursuerState.Catch;
        }
        switch (eStateBehavior)
        {
            case PursuerState.Follow:
                transform.position += followDirection * Time.deltaTime * moveFactor;
                float P_singleStep = P_towardFactor * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, followDirection, P_singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                break;
            case PursuerState.Patrol:

                if (epatrolStatus == PatrolStatus.PointChecking)
                {
                    timer_checking_running += Time.deltaTime;

                    if (timer_checking_running > checkingTime) 
                    {
                        return;
                    }
                    else
                    {
                        timer_checking_running = 0f;
                        epatrolStatus = PatrolStatus.PatrolMoving;
                        nowPathNodeIndex = (nowPathNodeIndex + 1) % pathNode.Length;
                        targetNormalVector = (pathNode[nowPathNodeIndex].position - transform.position).normalized;
                    }
                }
                float singleStep = towardFactor * Time.deltaTime;
                Vector3 newLookAtPosition = Vector3.RotateTowards(transform.forward, targetNormalVector, singleStep, 0f);
                transform.LookAt(transform.position + newLookAtPosition);
                transform.position = Vector3.MoveTowards(transform.position,
                                                        pathNode[nowPathNodeIndex].position,
                                                        Time.deltaTime * patrolSpeed);
                if (transform.position == pathNode[nowPathNodeIndex].position)
                {
                    epatrolStatus = PatrolStatus.PointChecking;
                }

                break;
            case PursuerState.Catch:
                break;

            default:
                break;
        }
        
        Debug.DrawLine(transform.position,
            transform.position + Vector3.RotateTowards(transform.forward, transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.red);

        Debug.DrawRay(transform.position,
            Vector3.RotateTowards(transform.forward, -transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.blue);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pursuerDistance);
    }
}
