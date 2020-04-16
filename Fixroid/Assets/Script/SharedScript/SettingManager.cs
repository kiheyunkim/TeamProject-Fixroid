using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance = null;
    public SettingSaveTile SettingTile { get; set; }

    public static SettingManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                GameObject instanceObj = new GameObject { name = "SettingManager Object" };
                instance = instanceObj.AddComponent<SettingManager>();

#if UNITY_EDITOR
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "/Setting");
#elif UNITY_ANDROID
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.persistentDataPath + "/Setting");
#endif

                if (!di.Exists)
                    di.Create();

#if UNITY_EDITOR
                System.IO.FileInfo fi = new System.IO.FileInfo(Application.dataPath + "/Setting/setting.json");
#elif UNITY_ANDROID
                System.IO.FileInfo fi = new System.IO.FileInfo(Application.persistentDataPath + "/Setting/setting.json");
#endif

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                    instance.SettingTile = new SettingSaveTile();
                    string saveString = JsonUtility.ToJson(instance.SettingTile);

#if UNITY_EDITOR
                    System.IO.File.WriteAllText(Application.dataPath + "/Setting/setting.json", saveString);
#elif UNITY_ANDROID
                    System.IO.File.WriteAllText(Application.persistentDataPath + "/Setting/setting.json", saveString);
#endif
                }
                else
                {
#if UNITY_EDITOR
                    string loadString = System.IO.File.ReadAllText(Application.dataPath + "/Setting/setting.json");
#elif UNITY_ANDROID
                    string loadString = System.IO.File.ReadAllText(Application.persistentDataPath + "/Setting/setting.json");
#endif

                    instance.SettingTile = JsonUtility.FromJson<SettingSaveTile>(loadString);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);    
    }

    private void OnApplicationQuit()
    {
        string saveString = JsonUtility.ToJson(SettingTile);

#if UNITY_EDITOR
        System.IO.File.WriteAllText(Application.dataPath + "/Setting/setting.json", saveString);
#elif UNITY_ANDROID
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Setting/setting.json", saveString);
#endif
    }

    public void NotWarning()
    {

    }
}

