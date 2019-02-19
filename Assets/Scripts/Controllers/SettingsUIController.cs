using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField]
    private Button m_ButtonVibration;

    [SerializeField]
    private Sprite m_SpriteVibrationOn, m_SpriteVibrationOff = null;

    [SerializeField]
    private Button m_ButtonAudio;

    [SerializeField]
    private Sprite m_SpriteAudioOff, m_SpriteAudioOn = null;

    void Awake()
    {
        PlayerSettings.LoadSettings();

        if (PlayerSettings.PrefEnable(PlayerSettings.Vibration))
            m_ButtonVibration.image.sprite = m_SpriteVibrationOn;
        else
            m_ButtonVibration.image.sprite = m_SpriteVibrationOff;

        if (PlayerSettings.PrefEnable(PlayerSettings.Mute))
            m_ButtonAudio.image.sprite = m_SpriteAudioOff;
        else
            m_ButtonAudio.image.sprite = m_SpriteAudioOn;

    }

    public void OnBack()
    {
        LevelChanger.FadeToLevel(GameLevels.MAIN_MENU);
    }

    public void OnVibration()
    {
        PlayerSettings.PrefInvert(GameSettings.VIBRATION, ref PlayerSettings.Vibration);

        m_ButtonVibration.image.sprite =
            PlayerSettings.PrefEnable(PlayerSettings.Vibration) ?
            m_SpriteVibrationOn : m_SpriteVibrationOff;
    }

    public void OnAudio()
    {
        PlayerSettings.PrefInvert(GameSettings.MUTE, ref PlayerSettings.Mute);

        m_ButtonAudio.image.sprite =
            PlayerSettings.PrefEnable(PlayerSettings.Mute) ?
            m_SpriteAudioOff : m_SpriteAudioOn;
    }
}
