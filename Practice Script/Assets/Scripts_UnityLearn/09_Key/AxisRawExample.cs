using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRawExample : MonoBehaviour
{
    void Update()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * inputValue * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0.5f, 2f);
        }
        Debug.Log("AxisRaw  " + Time.deltaTime.ToString("F6") + " | " + inputValue);
    }
}
