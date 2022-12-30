using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Lecture.RigidBody.Tutorial04
{
    [System.Serializable]
    public class SpawnData
    {
        public int StageId;
        public float SpawnTime;
        public BuffType SpawnitemType;
        public Vector3 SpawnPosition;
    }
    public class JsonPickupDataManager : MonoBehaviour
    {
        public List<SpawnData> Data = new List<SpawnData>();

        void Start()
        {
            DataSave();
        }
        public void DataLoad()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                string path = Application.persistentDataPath + "/SaveData" + i.ToString() + ".json";
                string json = File.ReadAllText(path);
                Data[i] = JsonUtility.FromJson<SpawnData>(json);
            }
        }
        [ContextMenu("Save Json")]
        public void DataSave()
        {
            for(int i = 0; i < Data.Count; i++ )
            {
                string json = JsonUtility.ToJson(Data[i], true);
                string path = Application.persistentDataPath + "/SaveData" + i.ToString() + ".json";
                File.WriteAllText(path, json);
            }
        }
    }
}