using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class ReadyCountUI : MonoBehaviour
    {
        [SerializeField] Text _readyCountText;
        float _readyCount = 6f;
        int _countDown;
        public bool firstStage = true;

        void Update()
        {
            _readyCount -= Time.deltaTime;
            _countDown = Mathf.FloorToInt(_readyCount);
            _readyCountText.text = _countDown.ToString();

            if (_countDown > 0)
            {
                _readyCountText.text = _countDown.ToString();
            }
            else if (_countDown == 0)
            {
                _readyCountText.text = "Start";
            }
            else if(_countDown < 0)
            {
                _readyCountText.gameObject.SetActive(false);
                firstStage = false;
            }
        }
        public void countDownStart()
        {
            _readyCountText.gameObject.SetActive(true);
            if(firstStage == true)
            {
                _readyCount = 6f;
            }
            else
            {
                _readyCount = 11f;
            }
        }

    }
}