using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeAndStart : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake called.");
    }

    void Start()
    {
        Debug.Log("Start called.");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable called.");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable called.");
    }
    private void Update()
    {
        // Debug.Log("Update called.");
    }

}
