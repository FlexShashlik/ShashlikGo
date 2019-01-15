using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField]
    private Button m_ButtonVibration;

    [SerializeField]
    private Sprite m_SpriteVibrationOn, m_SpriteVibrationOff;

    [SerializeField]
    private Button m_ButtonMute;

    [SerializeField]
    private Sprite m_SpriteMuteOn, m_SpriteMuteOff;

    void Awake()
    {
        PlayerSettings.LoadSettings();

        if (PlayerSettings.PrefEnable(PlayerSettings.Vibration))
            m_ButtonVibration.image.sprite = m_SpriteVibrationOn;
        else
            m_ButtonVibration.image.sprite = m_SpriteVibrationOff;

        if (PlayerSettings.PrefEnable(PlayerSettings.Mute))
            m_ButtonMute.image.sprite = m_SpriteMuteOn;
        else
            m_ButtonMute.image.sprite = m_SpriteMuteOff;

    }

    public void OnGo()
    {
        LevelChanger.FadeToLevel(GameLevels.MAIN);
    }

    public void OnVibration()
    {
        PlayerSettings.PrefInvert(GameSettings.VIBRATION, ref PlayerSettings.Vibration);

        m_ButtonVibration.image.sprite =
            PlayerSettings.PrefEnable(PlayerSettings.Vibration) ?
            m_SpriteVibrationOn : m_SpriteVibrationOff;
    }

    public void OnMute()
    {
        PlayerSettings.PrefInvert(GameSettings.MUTE, ref PlayerSettings.Mute);

        m_ButtonMute.image.sprite =
            PlayerSettings.PrefEnable(PlayerSettings.Mute) ?
            m_SpriteMuteOn : m_SpriteMuteOff;
    }
}