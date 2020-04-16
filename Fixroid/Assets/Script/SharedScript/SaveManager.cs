using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private static SaveManager instance = null;
    public SaveTile SaveTile { get; set; }

    public static SaveManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                GameObject instanceObj = new GameObject { name = "SaveManager Object" };
                instance = instanceObj.AddComponent<SaveManager>();
#if UNITY_EDITOR
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "/Setting");
#elif UNITY_ANDROID
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.persistentDataPath + "/Setting");
#endif
                if (!di.Exists)
                    di.Create();

#if UNITY_EDITOR
                System.IO.FileInfo fi = new System.IO.FileInfo(Application.dataPath + "/Setting/Save.json");
#elif UNITY_ANDROID
                System.IO.FileInfo fi = new System.IO.FileInfo(Application.persistentDataPath + "/Setting/Save.json");
#endif
                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                    instance.SaveTile = new SaveTile();
                    string saveString = JsonUtility.ToJson(instance.SaveTile);
#if UNITY_EDITOR
                    System.IO.File.WriteAllText(Application.dataPath + "/Setting/Save.json", saveString);
#elif UNITY_ANDROID
                    System.IO.File.WriteAllText(Application.persistentDataPath + "/Setting/Save.json", saveString);
#endif
                }
                else
                {
#if UNITY_EDITOR
                    string loadString = System.IO.File.ReadAllText(Application.dataPath + "/Setting/Save.json");
#elif UNITY_ANDROID
                    string loadString = System.IO.File.ReadAllText(Application.persistentDataPath + "/Setting/Save.json");
#endif
                    instance.SaveTile = JsonUtility.FromJson<SaveTile>(loadString);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Save(int stage, float time)
    {
        SaveTile.stageTime[stage] = time;

        string saveString = JsonUtility.ToJson(instance.SaveTile);

#if UNITY_EDITOR
        System.IO.File.WriteAllText(Application.dataPath + "/Setting/Save.json", saveString);
#elif UNITY_ANDROID
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Setting/Save.json", saveString);
#endif
    }

    public void NotWarning()
    {

    }
}
