using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampColor : MonoBehaviour
{
    Renderer m_renderer;
    Color color = Color.white;
    float timeScale = 3f;
    float value;

    float velocity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponentInChildren<Renderer>();
        m_renderer.material.color = color;
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.SmoothDamp(value, 1f, ref velocity , timeScale);
        //value = Mathf.Lerp(0f, 1f, 0.5f - Mathf.Cos(Time.time * Mathf.PI / timeScale) * 0.5f);
        
        color.b = 1f - value;
        color.g = 1f - value;
        m_renderer.material.color = color;

        transform.position = new Vector3(transform.position.x, value * 5f, transform.position.z);
    }
}
