using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager
{
    private static string path = Application.persistentDataPath + "/playerdata.dat";

    public static void SaveData(DataPlayer data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataPlayer LoadData()
    {
        string path = Application.persistentDataPath + "/playerdata.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlayer data = (DataPlayer)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Loi save " + path);
            return null;
        }
    }

    public static void DeleteData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Dữ liệu người chơi đã được xóa thành công!");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy tệp tin để xóa.");
        }
    }
}