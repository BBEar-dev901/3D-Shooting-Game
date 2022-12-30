using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class PlayerDamagePooler : MonoBehaviour
    {
        [Header("Player Damage Canvas")]
        [SerializeField] GameObject DamageCanvas;
        [SerializeField] int countCanvasPool;

        List<GameObject> DamagePool;

        public static PlayerDamagePooler Instance;
        
        void Awake()
        {
            Instance = this;
            DamagePool = new List<GameObject>();

            for(int i = 0; i < countCanvasPool; i++)
            {
                GameObject _fx = (GameObject)Instantiate(DamageCanvas);
                _fx.SetActive(false);
                _fx.transform.SetParent(transform);
                DamagePool.Add(_fx);
            }
        }

        public GameObject GetDamageCanvas()
        {
            if (DamagePool == null)
            {
                return null;
            }
            for (int i = 0; i < DamagePool.Count; ++i)
            {
                if (!DamagePool[i].activeInHierarchy)
                {
                    return DamagePool[i];
                }
            }
            return null;
        }
        public void InactiveDamageBar()
        {
            for(int i = 0; i < DamagePool.Count; ++i)
            {
                DamagePool[i].transform.SetParent(transform);
                DamagePool[i].SetActive(false);
            }
        }
    }
}