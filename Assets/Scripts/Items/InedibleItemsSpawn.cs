using UnityEngine;

class InedibleItemsSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_InedibleItem;

    private float m_CurrentInedibleItemTime;

    void Start()
    {
        m_CurrentInedibleItemTime = Random.Range(0.5f, 5f);
    }

    void Update()
    {
        m_CurrentInedibleItemTime -= GlobalData.Acceleration * Time.deltaTime;

        if (m_CurrentInedibleItemTime <= 0f)
        {
            m_CurrentInedibleItemTime = Random.Range(0.5f, 5f);

            Instantiate(m_InedibleItem, new Vector3(Random.Range(-2.7f, 2.8f), m_InedibleItem.transform.position.y, 9f), Quaternion.identity);
        }
    }
}