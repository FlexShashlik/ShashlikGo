using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CoinPrefab;

    private float m_CoinLifeTime;

    [SerializeField]
    private float m_MinCoinLifeTime, m_MaxCoinLifeTime = 0f;

    void Start()
    {
        m_CoinLifeTime = Random.Range(m_MinCoinLifeTime, m_MaxCoinLifeTime);
    }

    void Update()
    {
        m_CoinLifeTime -= GlobalData.Acceleration * Time.deltaTime;

        if (m_CoinLifeTime <= 0f)
        {
            m_CoinLifeTime = Random.Range(m_MinCoinLifeTime, m_MaxCoinLifeTime);

            Instantiate(m_CoinPrefab, new Vector3(Random.Range(-3f, 3f), m_CoinPrefab.transform.position.y, 9f), Quaternion.identity);
        }
    }
}