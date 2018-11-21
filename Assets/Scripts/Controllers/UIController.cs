using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreLabel;

    [SerializeField]
    private Text m_LivesLabel;

    [SerializeField]
    private Animator m_SkewerOverflowAnimator;

    private float m_TopAccelerationLevel = 10f;

    private float m_AccelerationCoefficient = 1.1f;

    void Awake()
    {
        Messenger.AddListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.AddListener(GameEvent.PICKED_UP_INEDIBLE_ITEM, OnPickingUpInedibleItem);

        PlayGamesScript.GetUserMaxScore();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.RemoveListener(GameEvent.PICKED_UP_INEDIBLE_ITEM, OnPickingUpInedibleItem);
    }

    void Start()
    {
        m_ScoreLabel.text = "Score: " + GlobalData.Score.ToString();
        m_LivesLabel.text = "Lives: " + GlobalData.Lives.ToString();
    }

    private void OnSkewerOverflow()
    {
        GlobalData.Score++;

        m_SkewerOverflowAnimator.SetTrigger("DoAnimation");

        //its absolutely random algorithm lol
        if(GlobalData.Acceleration < m_TopAccelerationLevel)
        {
            GlobalData.Acceleration += GlobalData.Acceleration * m_AccelerationCoefficient * Time.deltaTime;
        }

        m_ScoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }

    private void OnPickingUpInedibleItem()
    {
        GlobalData.Lives--;

        if(GlobalData.Lives < 0)
        {
            if (PlayGamesScript.SuccessAuth)
            {
                PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_score, GlobalData.Score);
            }
            else
            {
                PlayGamesScript.Auth();
            }

            LevelChanger.FadeToLevel(2); //load the end scene
        }
        else
        {
            m_LivesLabel.text = "Lives: " + GlobalData.Lives.ToString();
        }
    }
}
