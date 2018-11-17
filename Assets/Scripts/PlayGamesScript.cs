using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

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
        //PlayGamesPlatform.Instance.Authenticate(success => { });
    }

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    public static void GetUserMaxScore(Text bestResult)
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
                    bestResult.text = "Best result: " + data.PlayerScore.formattedValue;
                }
            );
    }
}
