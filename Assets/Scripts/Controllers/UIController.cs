using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text m_scoreLabel;

    [SerializeField]
    private Text m_livesLabel;

    [SerializeField]
    private Animator m_skewerOverflowAnimator;

    private float m_topAccelerationLevel = 10f;

    private float m_accelerationCoefficient = 1.1f;

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
        m_scoreLabel.text = "Score: " + GlobalData.Score.ToString();
        m_livesLabel.text = "Lives: " + GlobalData.Lives.ToString();
    }

    private void OnSkewerOverflow()
    {
        GlobalData.Score++;

        m_skewerOverflowAnimator.SetTrigger("DoAnimation");

        //its absolutely random algorithm lol
        if(GlobalData.Acceleration < m_topAccelerationLevel)
        {
            GlobalData.Acceleration += GlobalData.Acceleration * m_accelerationCoefficient * Time.deltaTime;
        }

        m_scoreLabel.text = "Score: " + GlobalData.Score.ToString();
    }

    private void OnPickingUpInedibleItem()
    {
        GlobalData.Lives--;

        if(GlobalData.Lives < 0)
        {
            StartCoroutine(LoadAsynchrounously(1)); //The End scene loading
        }
        else
        {
            m_livesLabel.text = "Lives: " + GlobalData.Lives.ToString();
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
}
