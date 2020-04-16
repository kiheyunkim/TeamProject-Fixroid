using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    public static void SaveAllInformation()
    {
        //save stages
        PlayerPrefs.SetInt("Stage1", MainMenu.StageLocked[0] ? 1 : 0);
        PlayerPrefs.SetInt("Stage2", MainMenu.StageLocked[1] ? 1 : 0);
        PlayerPrefs.SetInt("Stage3", MainMenu.StageLocked[2] ? 1 : 0);
        PlayerPrefs.SetInt("Stage4", MainMenu.StageLocked[3] ? 1 : 0);

        PlayerPrefs.SetInt("StageEnd1", MainMenu.StageEnd[0] ? 1 : 0);
        PlayerPrefs.SetInt("StageEnd2", MainMenu.StageEnd[1] ? 1 : 0);
        PlayerPrefs.SetInt("StageEnd3", MainMenu.StageEnd[2] ? 1 : 0);
        PlayerPrefs.SetInt("StageEnd4", MainMenu.StageEnd[3] ? 1 : 0);
        
        PlayerPrefs.SetInt("StageNoEnd1", MainMenu.StageNoEnd[0] ? 1 : 0);
        PlayerPrefs.SetInt("StageNoEnd2", MainMenu.StageNoEnd[1] ? 1 : 0);
        PlayerPrefs.SetInt("StageNoEnd3", MainMenu.StageNoEnd[2] ? 1 : 0);
        PlayerPrefs.SetInt("StageNoEnd4", MainMenu.StageNoEnd[3] ? 1 : 0);

        //save acheivement
        PlayerPrefs.SetInt("Acheive1", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive2", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive3", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive4", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive5", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive6", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive7", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive8", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive9", MainMenu.Acheive[0] ? 1 : 0);
        PlayerPrefs.SetInt("Acheive10", MainMenu.Acheive[0] ? 1 : 0);
    }
}
