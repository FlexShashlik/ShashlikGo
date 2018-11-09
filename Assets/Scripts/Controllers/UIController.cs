using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreLabel;

    private float m_topAccelerationLevel = 35f;

    private float m_accelerationCoefficient = 3f;

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
        if(GlobalData.Acceleration < m_topAccelerationLevel)
        {
            GlobalData.Acceleration += GlobalData.Acceleration * m_accelerationCoefficient * Time.deltaTime;
        }
        
        Debug.Log(GlobalData.Acceleration);

        scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }
}
