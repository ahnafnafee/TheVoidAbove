using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace _Project.Scripts
{
    public static class SaveSystem
    {
        public static void SaveGame(Player Player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Void_save";
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveData data = new SaveData(Player);

            formatter.Serialize(stream, data);
            stream.Close();


        }

        public static SaveData LoadGame()
        {
            string path = Application.persistentDataPath + "/Void_save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();

                return data;

            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
