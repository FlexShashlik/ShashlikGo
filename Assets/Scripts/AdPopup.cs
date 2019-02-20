using UnityEngine;
using UnityEngine.UI;

public class AdPopup : MonoBehaviour
{
    [SerializeField] Button m_ButtonPause;
    [SerializeField] Animator m_PopupAnimation;

    public void Open()
    {
        gameObject.SetActive(true);
        m_ButtonPause.interactable = false;
    }

    public void Close()
    {
        m_PopupAnimation.SetTrigger("Close");
        LevelChanger.FadeToLevel(GameLevels.THE_END);
    }

    public void AdRequest()
    {
        Messenger.Broadcast(GameEvent.AD_REQUEST);
    }
}
