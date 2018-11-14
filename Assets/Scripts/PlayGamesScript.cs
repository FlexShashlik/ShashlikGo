using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;

class PlayGamesScript : MonoBehaviour
{
    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // requests an ID token be generated.  This OAuth token can be used to
       //  identify the player to other services such as Firebase.
       .RequestIdToken()
       .Build();

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(config);
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        
        PlayGamesPlatform.Instance.Authenticate(success => { });
    }

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
}
