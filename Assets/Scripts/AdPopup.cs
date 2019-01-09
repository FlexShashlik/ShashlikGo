using UnityEngine;

public class AdPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        LevelChanger.FadeToLevel(2); //load the end scene
        gameObject.SetActive(false);
    }

    public void AdRequest()
    {
        Messenger.Broadcast(GameEvent.AD_REQUEST);
    }
}
