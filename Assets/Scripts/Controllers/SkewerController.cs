using System;
using UnityEngine;

public class SkewerController : MonoBehaviour
{
    void Update()
    {
        if(GlobalData.ItemsOnSkewer.Count > 6)
        {
            foreach(GameObject item in GlobalData.ItemsOnSkewer)
            {
                Destroy(item);
            }

            GlobalData.ItemsOnSkewer.Clear();
            Messenger.Broadcast(GameEvent.SKEWER_OVERFLOW);
        }
    }
}