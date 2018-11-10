using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text m_scoreLabel;

    [SerializeField]
    private Animator m_skewerOverflowAnimator;

    private float m_topAccelerationLevel = 15f;

    private float m_accelerationCoefficient = 2f;

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
        m_scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }

    private void OnSkewerOverflow()
    {
        GlobalData.Score += 1;

        m_skewerOverflowAnimator.SetTrigger("DoAnimation");

        //its absolutely random algorithm lol
        if(GlobalData.Acceleration < m_topAccelerationLevel)
        {
            GlobalData.Acceleration += GlobalData.Acceleration * m_accelerationCoefficient * Time.deltaTime;
        }

        m_scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }
}
