using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    float midValue = 0f;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player controller = other.GetComponentInParent<Player>();
            if(controller == null)
            {
                return;
            }
            else
            {
                // 게임 오브젝트 삭제
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        midValue += Time.deltaTime;
        transform.localEulerAngles = new Vector3(0f, midValue * 360f, 0f);

        float bobbingAnimationPhase = (Mathf.Sin(Time.time * 2f * Mathf.PI) * 0.5f) + 0.25f;
        transform.position = startPosition + Vector3.up * bobbingAnimationPhase;
    }
}
