using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Update 함수가 호출되기 전 한번만 호출됨
    }

    // Update is called once per frame
    void Update()
    {
        //매 프레임마다 호출되는 함수

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
