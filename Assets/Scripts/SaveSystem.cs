using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveBackPack(Dictionary<string, Material> bk)
    {
        foreach (KeyValuePair<string, Material> mat in bk)
        {
            mat.Value.SetSelected(0);
        }
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/bp.ahbl";
        FileStream stream = new FileStream(path, FileMode.Create);

        BackPackData data = new BackPackData(bk);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static BackPackData LoadBackPack()
    {
        string path = Application.persistentDataPath + "/bp.ahbl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BackPackData data = formatter.Deserialize(stream) as BackPackData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

    public static void SaveBook(Dictionary<string, Recipe> b)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/book.ahbl";
        FileStream stream = new FileStream(path, FileMode.Create);

        BookData data = new BookData(b);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static BookData LoadBook()
    {
        string path = Application.persistentDataPath + "/book.ahbl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BookData data = formatter.Deserialize(stream) as BookData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + $"/{player.GetName()}.ahbl";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + $"/{name}.ahbl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

}
