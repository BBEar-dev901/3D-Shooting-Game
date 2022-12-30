using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoWhileLoops : MonoBehaviour
{
    void Start()
    {
        bool shouldContinue = false;

        do
        {
            Debug.Log("[3] Hello World");

        } while (shouldContinue == true);
    }
}
