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

    // �ʿ��� ������ ����

    public Transform target;
    // ���ƿ� ��ǥ ����
    Vector3 patrolPoint;
    // ���� �Ÿ���
    float pursuerDistance = 10f;
    // �־����� ������ ���� �Ÿ� ��
    float stoppingDistance = 1.5f;
    // �̵� �ӵ�
    [SerializeField] float moveFactor = 5f;
    // �⺻ �ൿ ��� ����
    PursuerState eStateBehavior = PursuerState.Patrol;

    float towardFactor = Mathf.PI * 2;

    // �þ߰� 90��
    float fieldOfVision = Mathf.PI * 0.5f;
    // �þ߰� �ȿ� �������� Ȯ��
    float visionDecisionValue;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoint = transform.position;
        // �þ߰��� cos ��
        visionDecisionValue = Mathf.Cos(fieldOfVision * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ�ٰ��� �Ÿ� ���
        Vector3 followDirection = (target.position - transform.position).normalized;
        float currentDistance = (target.position - transform.position).magnitude;

        // Ÿ�ٰ� �� ������ �Ÿ��� ���� �ൿ ����
        if( currentDistance > pursuerDistance)
        {
            eStateBehavior = PursuerState.Patrol;
        }
        else if( currentDistance > stoppingDistance )
        {   
            if (eStateBehavior == PursuerState.Patrol)
            {
                if (Vector3.Dot(transform.forward, followDirection) > visionDecisionValue)
                { // �ڽ��� z�� , Ÿ���� ������ ���� �þ߰����� Ŭ�� (�þ߰��� ���� ��) 
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

        // ���� ���� ���� �ൿ ����
        switch (eStateBehavior) 
        {
            case PursuerState.Follow:
                // ���� ���� * �̵� �ӵ��� �̵�
                transform.position += followDirection * Time.deltaTime * moveFactor;
                break;
            case PursuerState.Patrol:
                followDirection = patrolPoint - transform.position;
                // Vector3.MoveTowards()�� ����ϸ� ��Ȯ�� ��ġ�� �ٽ� �ǵ��ư���.
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

        // ����� ���� ���� ǥ���ϱ�
        Debug.DrawLine(transform.position, 
            transform.position + Vector3.RotateTowards(transform.forward, transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.red);

        Debug.DrawRay(transform.position,
            Vector3.RotateTowards(transform.forward, -transform.right, fieldOfVision * 0.5f, 0f) * pursuerDistance,
            Color.blue);
    }
    // Gizmo Debug���� �� �׷��� ���� Ȯ���ϱ� 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pursuerDistance);
    }
}
