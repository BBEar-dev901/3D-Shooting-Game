using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������ ���� �ϱ� ���� ���� �����̽�
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
            // ����ȭ
            string json = JsonUtility.ToJson(saveObject[i].scriptableObject);
            writer.Write(json);
            // Create ���� �� Close ���ֱ�
            writer.Close();
            fileStream.Close();
        }
    }

}
