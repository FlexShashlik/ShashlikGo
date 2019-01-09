using UnityEngine;

public class AdPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AdRequest()
    {
        Messenger.Broadcast(GameEvent.AD_REQUEST);
    }
}
