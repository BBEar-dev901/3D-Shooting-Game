using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float moveDamping = 2f;
    [SerializeField] float rotationDamping = 180f;
    [SerializeField] CameraDataSet cameraMove;

    CameraType currentCamera;

    private void Awake()
    {
        currentCamera = CameraType.NormalCamera;
    }
    public void SetCameraType(CameraType _type)
    {
        currentCamera = _type;
    }
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        float wantedRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //Vector3 wantedPosition = target.position + currentRotation * cameraOffset;
        //Vector3 wantedPosition = target.position + cameraOffset;
        Vector3 wantedPosition = target.position + currentRotation * cameraMove.cameraSets[(int)currentCamera].cameraOffset;
        Vector3 currentPosition = Vector3.Lerp(transform.position, wantedPosition, moveDamping * Time.deltaTime);
        transform.position = currentPosition;
        transform.LookAt(target);
    }
}
