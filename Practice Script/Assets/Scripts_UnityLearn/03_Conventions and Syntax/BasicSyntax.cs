using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSyntax : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 10f)] float positionY = .5f;
    // Start is called before the first frame update
    void Start()
    {
        // inspector에서 적용해도 함수 안에서 초기화 하면 inspector에서 적용한 값은 적용되지 않는다.
        // positionY = 0;

        transform.position = transform.GetRelativeY(positionY);
        
        Debug.Log(transform.position.x);
        
        if (transform.position.y >= 5f)
        {
            Debug.Log("I'm about to hit the ground!");
        }

        Debug.LogFormat("Y position is {0}", transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
