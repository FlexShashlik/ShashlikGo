using GoogleMobileAds.Api;
using System;
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
    private Sprite m_SpritePause, m_SpritePlay = null;

    [SerializeField]
    private AdPopup m_AdPopup;

    private bool m_GameOnPause = false;

    private float m_TopAccelerationLevel = 10f;

    private float m_AccelerationCoefficient = 1.1f;

    private RewardBasedVideoAd rewardBasedVideo;

    private bool m_AdWasShown = false;

    void Awake()
    {
        Messenger.AddListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.AddListener(GameEvent.INEDIBLE_ITEM_PICKUP, OnInedibleItemPickup);
        Messenger.AddListener(GameEvent.AD_REQUEST, RequestRewardBasedVideo);

        PlayGamesScript.GetUserMaxScore();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SKEWER_OVERFLOW, OnSkewerOverflow);
        Messenger.RemoveListener(GameEvent.INEDIBLE_ITEM_PICKUP, OnInedibleItemPickup);
        Messenger.RemoveListener(GameEvent.AD_REQUEST, RequestRewardBasedVideo);
    }

    void Start()
    {
        m_TextLives.text = $"Lives: {GlobalData.Lives}";
        m_TextScore.text = $"Score: {GlobalData.Score}";

        // Get singleton reward based video ad reference.
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        #region VideoEvents
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        #endregion
    }

    #region VideoHandlers
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        rewardBasedVideo.Show();
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        OnPause();
        LevelChanger.FadeToLevel(2); //load the end scene
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        if(type != null)
            MonoBehaviour.print(
                "HandleRewardBasedVideoRewarded event received for "
                + amount.ToString() + " " + type);

        GlobalData.Lives = 3;
        m_TextLives.text = $"Lives: {GlobalData.Lives}";
        m_AdWasShown = true;
        m_AdPopup.gameObject.SetActive(false);
        m_ButtonPause.interactable = true;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    #endregion

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

            //AdRequest
            if (!m_AdWasShown)
            {
                OnPause();
                m_AdPopup.Open();
            }
            else
                LevelChanger.FadeToLevel(GameLevels.THE_END); 
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

    private void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-2456669905266460/1795119596";
        #elif UNITY_IPHONE
                string adUnitId = "unexpected_platform";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);
    }
}
