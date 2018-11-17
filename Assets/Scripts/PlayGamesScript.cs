using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

class PlayGamesScript : MonoBehaviour
{
    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       .Build();

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(config);
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(success => { });
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
