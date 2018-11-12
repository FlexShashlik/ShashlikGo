using UnityEngine;

class InedibleItemsSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_inedibleItem;

    private float m_currentInedibleItemTime;

    void Start()
    {
        m_currentInedibleItemTime = Random.Range(0.5f, 5f);
    }

    void Update()
    {
        m_currentInedibleItemTime -= GlobalData.Acceleration * Time.deltaTime;

        if (m_currentInedibleItemTime <= 0f)
        {
            m_currentInedibleItemTime = Random.Range(0.5f, 5f);

            Instantiate(m_inedibleItem, new Vector3(Random.Range(-2.7f, 2.8f), m_inedibleItem.transform.position.y, 9f), Quaternion.identity);
        }
    }
}