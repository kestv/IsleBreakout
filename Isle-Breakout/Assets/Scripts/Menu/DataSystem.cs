using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class DataSystem
{
    static PlayerData data;
    public static void Save(PlayerData player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = string.Format("{0}/{1}.save", Application.persistentDataPath, player.id);
        if (!File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, player);
        stream.Close();
    }

    public static PlayerData Load(int id)
    {
        string path = string.Format("{0}/{1}.save", Application.persistentDataPath, id);
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteSave(int id)
    {
        string path = string.Format("{0}/{1}.save", Application.persistentDataPath, id);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
