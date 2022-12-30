using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 파일을 오픈 하기 위한 네임 스페이스
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [System.Serializable]
    public struct DataStoreInfo
    {
        public ScriptableObject scriptableObject;
        public string fileName;
        public string fileType;
    }

    public List<DataStoreInfo> saveObject;

    private void OnEnable()
    {
        for (int i = 0; i < saveObject.Count; i++)
        {
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.{1}", saveObject[i].fileName, saveObject[i].fileType)))
            {
                using (var filestream = File.Open(Application.persistentDataPath + string.Format("/{0}.{1}", saveObject[i].fileName, saveObject[i].fileType), FileMode.Open))
                {
                    using (var reader = new BinaryReader(filestream))
                    {
                        JsonUtility.FromJsonOverwrite(reader.ReadString(), saveObject[i].scriptableObject);
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < saveObject.Count; i++)
        {
            FileStream fileStream = File.Create(Application.persistentDataPath + string.Format("/{0}.{1}", saveObject[i].fileName, saveObject[i].fileType));
            BinaryWriter writer = new BinaryWriter(fileStream);
            // 직렬화
            string json = JsonUtility.ToJson(saveObject[i].scriptableObject);
            writer.Write(json);
            // Create 에는 꼭 Close 해주기
            writer.Close();
            fileStream.Close();
        }
    }

}
