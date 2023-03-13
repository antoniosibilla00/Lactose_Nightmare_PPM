using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public static class SaveSystem
{
    public static void SavePlayer(PlayerScript player)
    {
        BinaryFormatter _formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        _formatter.Serialize(stream, data);

        for (int i = 0; i < player.indexSrollsCollected.Count; i++)
        {
            Debug.Log("//// SaveLista " + player.indexSrollsCollected[i]);
        }
        
        
        stream.Close();
    }


    public static PlayerData LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = _formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;

        }
        else
        {
            return null;
        }


        
    }
}