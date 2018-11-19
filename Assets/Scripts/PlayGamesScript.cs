using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

class PlayGamesScript : MonoBehaviour
{
    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       .Build();

    private static bool m_SuccessAuth = false;

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(config);

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(success => { if (success) m_SuccessAuth = true; });
    }

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (m_SuccessAuth) Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        if (m_SuccessAuth) Social.ShowLeaderboardUI();
    }

    public static void GetUserMaxScore()
    {
        if (m_SuccessAuth)
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
