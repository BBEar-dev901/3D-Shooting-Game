using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisExample : MonoBehaviour
{
    void Update()
    {
        float inputValue = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * inputValue * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0.5f, 0f);
        }
        Debug.Log("Axis  " + Time.deltaTime.ToString("F6") + " | " + inputValue);
    }
}
