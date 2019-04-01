using UnityEngine;

public class AboutUIController : MonoBehaviour
{
    public void OnBack() => LevelChanger.FadeToLevel(GameLevels.MAIN_MENU);
}
