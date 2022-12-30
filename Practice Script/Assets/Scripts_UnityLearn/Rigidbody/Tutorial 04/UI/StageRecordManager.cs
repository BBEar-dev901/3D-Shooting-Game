using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class StageRecordManager : MonoBehaviour
    {
        public static StageRecordManager Instance;

        [SerializeField] GameObject m_recordBoardUIPrefab;
        List<GameObject> m_recordBoardList;

        private void Awake()
        {
            Instance = this;

            m_recordBoardList = new List<GameObject>();
        }

        // 점수판 레이아웃
        public GameObject RegisterRecordLayout(StageRecordMessage _display)
        {
            GameObject newObjective = Instantiate(m_recordBoardUIPrefab);
            newObjective.GetComponent<StageRecordLayout>().InitializeRecord(_display);
            newObjective.transform.SetParent(this.transform, false);
            m_recordBoardList.Add(newObjective);

            return newObjective;
        }
        public void UnregisterRecordBoard()
        {
            foreach (var item in m_recordBoardList)
            {
                Destroy(item);
            }
            m_recordBoardList.Clear();
        }
    }
}
