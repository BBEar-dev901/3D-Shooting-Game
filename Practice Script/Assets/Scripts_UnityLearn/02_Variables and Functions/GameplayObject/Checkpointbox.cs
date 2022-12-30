using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointbox : MonoBehaviour
{
    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = transform.GetRelativeY(-.5f);
    }//Start
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player controller;

            controller = other.GetComponentInParent<Player>();
            if(controller == null)
            {
                return;
            }
            else
            {
                controller.SetNewCheckpoint(respawnPosition);
            }
        }
    }//OnTriggerEnter
}
