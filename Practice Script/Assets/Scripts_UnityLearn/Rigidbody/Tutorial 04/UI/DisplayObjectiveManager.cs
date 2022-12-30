using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class DisplayObjectiveManager : MonoBehaviour
    {
        public static DisplayObjectiveManager Instance;
        
        [SerializeField] GameObject objectiveUIPrefab;
        List<GameObject> m_objectivesList;

        private void Awake()
        {
            Instance = this;

            m_objectivesList = new List<GameObject>();
        }
        public GameObject RegisterObjective(DisplayObjectiveMessage _display)
        {
            GameObject newObjective = Instantiate(objectiveUIPrefab);
            newObjective.GetComponent<DisplayObjectiveLayout>().InitializeObjective(_display);
            newObjective.transform.SetParent(this.transform, false);
            m_objectivesList.Add(newObjective);

            return newObjective;
        }
        public void UpdateObjective(GameObject _obj, string _CounterText, bool isComplted = false)
        {
            if(_obj == null)
            {
                return;
            }
            _obj.GetComponent<DisplayObjectiveLayout>().UpdateCounter(_CounterText);
            if(isComplted == true)
            {
                _obj.GetComponent<DisplayObjectiveLayout>().UpdateCompleted();
            }
        }
        public void UnregisterAllObjectives()
        {
            foreach (var item in m_objectivesList)
            {
                Destroy(item);
            }
            m_objectivesList.Clear();
        }

    }
}