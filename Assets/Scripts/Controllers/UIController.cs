using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_score, GlobalData.Score);
            StartCoroutine(LoadAsynchrounously(1)); //The End scene loading
        }
        else
        {
            m_LivesLabel.text = "Lives: " + GlobalData.Lives.ToString();
        }
    }

    IEnumerator LoadAsynchrounously(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ShowLeaderboards()
    {
        PlayGamesScript.ShowLeaderboardUI();
    }
}
