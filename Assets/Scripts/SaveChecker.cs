using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
    public static bool CheckSaveFile()
    {
        if (SaveLoad.SaveExists("TimeHours") &&
            SaveLoad.SaveExists("TimeMinutes") &&
            SaveLoad.SaveExists("TimeSeconds") &&
            SaveLoad.SaveExists("PlayerLevel") &&
            SaveLoad.SaveExists("PlayerName") &&
            SaveLoad.SaveExists("PlayerMaxHP") &&
            SaveLoad.SaveExists("PlayerCurHP") &&
            SaveLoad.SaveExists("PlayerMaxXP") &&
            SaveLoad.SaveExists("PlayerCurXP") &&
            SaveLoad.SaveExists("PlayerLocationName") &&
            SaveLoad.SaveExists("PlayerPositionX") &&
            SaveLoad.SaveExists("PlayerPositionY") &&
            SaveLoad.SaveExists("CameraPriorities") &&
            SaveLoad.SaveExists("Inventory"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
