using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 latestCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        latestCheckpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.position = latestCheckpoint;
        }
    }
    
    public void SetNewCheckpoint (Vector3 _checkpoint)
    {
        latestCheckpoint = _checkpoint;
    }
}
