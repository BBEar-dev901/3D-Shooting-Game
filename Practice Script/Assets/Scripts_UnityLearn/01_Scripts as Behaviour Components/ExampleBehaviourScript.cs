using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Update �Լ��� ȣ��Ǳ� �� �ѹ��� ȣ���
    }

    // Update is called once per frame
    void Update()
    {
        //�� �����Ӹ��� ȣ��Ǵ� �Լ�

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponentInChildren<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponentInChildren<Renderer>().material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponentInChildren<Renderer>().material.color = Color.blue;
        }
    }
}
