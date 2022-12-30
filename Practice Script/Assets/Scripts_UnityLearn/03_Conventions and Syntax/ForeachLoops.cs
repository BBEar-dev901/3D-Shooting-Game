using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeachLoops : MonoBehaviour
{
    void Start()
    {
        string[] strings = new string[3];

        strings[0] = "[4] First string";
        strings[1] = "[4] Second string";
        strings[2] = "[4] Third string";

        foreach (string item in strings)
        {
            Debug.Log(item);
        }
    }
}
