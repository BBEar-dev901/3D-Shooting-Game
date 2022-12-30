using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpongColor : MonoBehaviour
{
    Renderer m_renderer;
    Color color = Color.white;
    float timeScale = 3f;
    float value;

    //bool running = true;

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
        value = Mathf.PingPong(Time.time / timeScale, 1f);
        color.r = 1f - value;
        color.b = 1f - value;
        m_renderer.material.color = color;

        transform.position = new Vector3(transform.position.x, value * 5f, transform.position.z);
    }
}
