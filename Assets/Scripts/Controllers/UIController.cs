using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text m_TextLives;

    [SerializeField]
    private Text m_TextScore;

    [SerializeField]
    private Animator m_SkewerOverflowAnimator;

    [SerializeField]
    private Button m_ButtonPause;

    [SerializeField]
    private Sprite m_SpritePause, m_SpritePlay;

    private bool m_GameOnPause = false;

    private float m_TopAccelerationLevel = 10f;

    private float m_AccelerationCoefficient = 1.1f;

    void Awake()
    {
        Messenger.AddListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.AddListener(GameEvent.INEDIBLE_ITEM_PICKUP, OnInedibleItemPickup);

        PlayGamesScript.GetUserMaxScore();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.RemoveListener(GameEvent.INEDIBLE_ITEM_PICKUP, OnInedibleItemPickup);
    }

    void Start()
    {
        m_TextLives.text = $"Lives: {GlobalData.Lives}";
        m_TextScore.text = $"Score: {GlobalData.Score}";
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

        m_TextScore.text = $"Score: {GlobalData.Score}";
    }

    private void OnInedibleItemPickup()
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
            m_TextLives.text = $"Lives: {GlobalData.Lives}";
        }
    }

    public void OnPause()
    {
        if (!m_GameOnPause)
        {
            Time.timeScale = 0;
            m_ButtonPause.image.sprite = m_SpritePlay;
        }
        else
        {
            Time.timeScale = 1;
            m_ButtonPause.image.sprite = m_SpritePause;
        }

        m_GameOnPause = !m_GameOnPause;
    }
}
