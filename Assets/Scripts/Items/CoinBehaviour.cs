using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_RoundSpeedFactor; 

    void Update()
    {
        gameObject.transform.Rotate
            (
                new Vector3(0f, 0f, m_RoundSpeedFactor * Time.deltaTime)
            );
    }
}
