using UnityEngine;

public class PlayerSettings
{
    //0 - Off, 1 - On.
    public static int Vibration;
    public static int Mute;

    public static void LoadSettings()
    {
        LoadPref(GameSettings.VIBRATION, ref Vibration);
        LoadPref(GameSettings.MUTE, ref Mute);
    }

    private static void LoadPref(string key, ref int value)
    {
        if (PlayerPrefs.HasKey(key))
            value = PlayerPrefs.GetInt(key);
        else
        {
            PlayerPrefs.SetInt(key, 1);
            value = 1;
        }
    }

    public static bool PrefEnable(int value)
    {
        return value == 1 ? true : false;
    }

    public static void PrefInvert(string key, ref int value)
    {
        value = value == 1 ? 0 : 1;

        PlayerPrefs.SetInt(key, value);
    }
}