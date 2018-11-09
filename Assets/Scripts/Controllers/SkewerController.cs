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
                InputHandler.SpeedFactor += InputHandler.SPEED_FACTOR_DECREASE_COEF;
            }

            GlobalData.ItemsOnSkewer.Clear();
            Messenger.Broadcast(GameEvent.SKEWER_OVERFLOW);
        }
    }
}