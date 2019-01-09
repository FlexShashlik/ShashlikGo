using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

class PlayGamesScript : MonoBehaviour
{
    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       .Build();

    public static bool SuccessAuth = false;

    void Start()
    {
        #if UNITY_ANDROID
                string appId = "ca-app-pub-3940256099942544~3347511713";
        #elif UNITY_IPHONE
                string appId = "unexpected_platform";
        #else
                string appId = "unexpected_platform";
        #endif

        PlayGamesPlatform.InitializeInstance(config);

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        //Activate Google AdMob
        MobileAds.Initialize(appId);

        Auth();
    }

    public static void Auth()
    {
        Social.localUser.Authenticate
            (success => { if (success) { SuccessAuth = true; GetUserMaxScore(); }});
    }

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (SuccessAuth)
            Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        if (SuccessAuth)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Auth();
        }
    }

    public static void GetUserMaxScore()
    {
        if (SuccessAuth)
        {
            PlayGamesPlatform.Instance.LoadScores
                (
                    GPGSIds.leaderboard_score,
                    LeaderboardStart.PlayerCentered,
                    1,
                    LeaderboardCollection.Public,
                    LeaderboardTimeSpan.AllTime,
                    data =>
                    {
                        GlobalData.MaxScore = data.PlayerScore.value;
                    }
                );
        }
    }
}
