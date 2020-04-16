using UnityEngine;
using System.Collections;

public class LoadInformation : MonoBehaviour
{
    //Load Stage state
    static private int StageLocked1 = PlayerPrefs.GetInt("StageLocked1");
    static private int StageLocked2 = PlayerPrefs.GetInt("StageLocked2");
    static private int StageLocked3 = PlayerPrefs.GetInt("StageLocked3");
    static private int StageLocked4 = PlayerPrefs.GetInt("StageLocked4");

    static private int StageEnd1 = PlayerPrefs.GetInt("StageEnd1");
    static private int StageEnd2 = PlayerPrefs.GetInt("StageEnd2");
    static private int StageEnd3 = PlayerPrefs.GetInt("StageEnd3");
    static private int StageEnd4 = PlayerPrefs.GetInt("StageEnd4");

    static private int StageNoEnd1 = PlayerPrefs.GetInt("StageNoEnd1");
    static private int StageNoEnd2 = PlayerPrefs.GetInt("StageNoEnd2");
    static private int StageNoEnd3 = PlayerPrefs.GetInt("StageNoEnd3");
    static private int StageNoEnd4 = PlayerPrefs.GetInt("StageNoEnd4");

    //Load Acheive State
    static private int Acheive1 = PlayerPrefs.GetInt("Acheive1");
    static private int Acheive2 = PlayerPrefs.GetInt("Acheive2");
    static private int Acheive3 = PlayerPrefs.GetInt("Acheive3");
    static private int Acheive4 = PlayerPrefs.GetInt("Acheive4");
    static private int Acheive5 = PlayerPrefs.GetInt("Acheive5");
    static private int Acheive6 = PlayerPrefs.GetInt("Acheive6");

    public static void Load_First_Setting()//setting for first Playing
    {
        PlayerPrefs.SetString("Team6Setting", "Team6");//now this is not First Excute
        
        //initialization for Stages
        MainMenu.StageLocked[0] = false;
        for (int i = 1; i < 4; i++)
            MainMenu.StageLocked[i] = true;

        //Initialization for StageNoEnd confirmation
        MainMenu.StageNoEnd[0] = true;
        for (int i = 1; i < 4; i++)
            MainMenu.StageNoEnd[i] = false;

        //Initialization for StageEnd confirmation
        for (int i = 0; i < 4; i++)
            MainMenu.StageEnd[i] = false;

        //initialization for acheive
        for (int j = 0; j < 6; j++)
            MainMenu.Acheive[j] = false;


    }

    public static void Load_Previous_Setting()
    {
        //Load for StageLocked Processing
        if (StageLocked1 == 1)
            MainMenu.StageLocked[0] = true;
        else
            MainMenu.StageLocked[0] = false;

        if (StageLocked2 == 1)
            MainMenu.StageLocked[1] = true;
        else
            MainMenu.StageLocked[1] = false;

        if (StageLocked3 == 1)
            MainMenu.StageLocked[2] = true;
        else
            MainMenu.StageLocked[2] = false;

        if (StageLocked4 == 1)
            MainMenu.StageLocked[3] = true;
        else
            MainMenu.StageLocked[3] = false;


        //Initialization for StageNoEnd confirmation
        if (StageNoEnd1 == 1)
            MainMenu.StageNoEnd[0] = true;
        else
            MainMenu.StageNoEnd[0] = false;

        if (StageNoEnd2 == 1)
            MainMenu.StageNoEnd[1] = true;
        else
            MainMenu.StageNoEnd[1] = false;

        if (StageNoEnd3 == 1)
            MainMenu.StageNoEnd[2] = true;
        else
            MainMenu.StageNoEnd[2] = false;

        if (StageNoEnd4 == 1)
            MainMenu.StageNoEnd[3] = true;
        else
            MainMenu.StageNoEnd[3] = false;


        //Initialization for StageEnd confirmation
        if (StageEnd1 == 1)
            MainMenu.StageEnd[0] = true;
        else
            MainMenu.StageEnd[0] = false;

        if (StageEnd2 == 1)
            MainMenu.StageEnd[1] = true;
        else
            MainMenu.StageEnd[1] = false;

        if (StageEnd3 == 1)
            MainMenu.StageEnd[2] = true;
        else
            MainMenu.StageEnd[2] = false;

        if (StageEnd4 == 1)
            MainMenu.StageEnd[3] = true;
        else
            MainMenu.StageEnd[3] = false;


        //Load for Acheive

        if (Acheive1 == 1)
            MainMenu.Acheive[0] = true;
        else
            MainMenu.Acheive[0] = false;

        if (Acheive2 == 1)
            MainMenu.Acheive[1] = true;
        else
            MainMenu.Acheive[1] = false;

        if (Acheive3 == 1)
            MainMenu.Acheive[2] = true;
        else
            MainMenu.Acheive[2] = false;

        if (Acheive4 == 1)
            MainMenu.Acheive[3] = true;
        else
            MainMenu.Acheive[3] = false;

        if (Acheive5 == 1)
            MainMenu.Acheive[4] = true;
        else
            MainMenu.Acheive[4] = false;

        if (Acheive6 == 1)
            MainMenu.Acheive[5] = true;
        else
            MainMenu.Acheive[5] = false;
    }
}
