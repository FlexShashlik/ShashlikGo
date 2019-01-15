using UnityEngine;
using UnityEngine.UI;

public class AdPopup : MonoBehaviour
{
    [SerializeField] Button m_ButtonPause;

    public void Open()
    {
        gameObject.SetActive(true);
        m_ButtonPause.interactable = false;
    }

    public void Close()
    {
        LevelChanger.FadeToLevel(GameLevels.THE_END);
        gameObject.SetActive(false);
    }

    public void AdRequest()
    {
        Messenger.Broadcast(GameEvent.AD_REQUEST);
    }
}
