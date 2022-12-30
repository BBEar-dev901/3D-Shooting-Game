using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static Vector3 GetRelativeY(this Transform transform, float y) // y축 좌표 변경
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        return newPosition;
    }
    public static Vector3 GetRelativeX(this Transform transform, float x) // x축 좌표 변경
    {
        Vector3 newPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        return newPosition;
    }
    public static Vector3 GetRelativeZ(this Transform transform, float z) // z축 좌표 변경
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
        return newPosition;
    }
    public static void MovePositionX(this Transform transform, float x) // x축으로 움직이기
    {
        Vector3 newPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    public static void MovePositionY(this Transform transform, float y) // y축으로 움직이기
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        transform.position = newPosition;
    }
    public static void MovePositionZ(this Transform transform, float z) // z축으로 움직이기
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
        transform.position = newPosition;
    }
}
