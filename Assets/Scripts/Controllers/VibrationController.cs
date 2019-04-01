using UnityEngine;

public static class VibrationController
{
#if UNITY_ANDROID && !UNITY_EDITOR
        public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
        public static AndroidJavaClass unityPlayer;
        public static AndroidJavaObject currentActivity;
        public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long milliseconds)
    {
        if (PlayerSettings.PrefEnable(PlayerSettings.Vibration))
        {
            if (isAndroid())
            {
                vibrator.Call("vibrate", milliseconds);
            }
            else
            {
                Handheld.Vibrate();
            }
        }
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	        return true;
#else
            return false;
#endif
    }
}
