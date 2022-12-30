using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    Renderer m_renderer;
    Color color = Color.green;
    float timeScale = 3f;
    float value;

    float t_Lerp = 0f;
    float sign_Lerp = 1f;

    void Start()
    {
        m_renderer = GetComponentInChildren<Renderer>();
        m_renderer.material.color = color;
        value = 0;
    }

    void Update()
    {
        value = Mathf.Lerp(0f, 1f, Time.time / timeScale);
        //color.r = 1f - value;
        color.r = value;
        color.g = 1f - value;
        m_renderer.material.color = color;
        t_Lerp += sign_Lerp * Time.deltaTime / timeScale;

        if(t_Lerp >= 1f)
        {
            t_Lerp = 1f;
            sign_Lerp = -1f;
        }
        else if(t_Lerp <= 0f)
        {
            t_Lerp = 0f;
            sign_Lerp = 1f;
        }
        value = Mathf.Lerp(0f, 1f, t_Lerp);

        // cos()
        // value = Mathf.Lerp(0f, 1f, 0.5f - Mathf.Cos(Time.time * Mathf.PI / timeScale) * 0.5f);

        transform.position = new Vector3(transform.position.x, value * 5f, transform.position.z);
    }
}
