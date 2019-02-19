using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    public void OnGo()
    {
        LevelChanger.FadeToLevel(GameLevels.MAIN);
    }

    public void OnSettings()
    {
        LevelChanger.FadeToLevel(GameLevels.SETTINGS);
    }

    public void OnShop()
    {
        //TODO
    }

    public void OnAbout()
    {
        LevelChanger.FadeToLevel(GameLevels.ABOUT);
    }
}