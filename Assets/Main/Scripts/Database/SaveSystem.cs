﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem 
{
    // Start is called before the first frame update
    public static readonly string SAVE_FOLDER = Path.Combine(Application.dataPath, "Save");
    public static void Init()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void Save(string saveString)
    {
        File.WriteAllText(Path.Combine(SAVE_FOLDER, "save_json"),saveString);
        
    }
    public static bool Savefound()
    {
        if(File.Exists(Path.Combine(SAVE_FOLDER, "save_json")))
        {
            return true;
        }
        return false;
    }
    public static string Load()
    {
        if(Savefound())
        {
            string saveString= File.ReadAllText(Path.Combine(SAVE_FOLDER, "save_json"));
            return saveString;
        }
        else{
            return null;
        }
    }
}
