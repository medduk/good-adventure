using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void Save(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath,"Savefile.bin");  // �ſ� �߿���!! ������ ������ ��δ� persistentDataPath�� ���
        //Debug.Log(Application.persistentDataPath);
        FileStream stream = File.Create(path);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, "Savefile.bin");
            FileStream stream = File.OpenRead(path);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
    }
}
