using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreLabel;

    void Awake()
    {
        Messenger.AddListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
    }

    void Start()
    {
        scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }

    private void OnSkewerOverflow()
    {
        GlobalData.Score += 1;

        //its absolutely random algorithm lol
        GlobalData.Acceleration += (GlobalData.Score * Time.deltaTime) / 2.5f;

        scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }
}
