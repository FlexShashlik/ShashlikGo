using UnityEngine;

public class InedibleItemsBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    private float m_speedFactor;

    private float m_bound = -9f;

    [SerializeField]
    private GameObject m_explosionEffect;

    void Update()
    {
        Move();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Skewer"))
        {
            Explode();
        }
    }

    private void Move()
    {
        if (transform.position.z < m_bound)
        {
            Destroy(gameObject);
        }

        Vector3 movement = Vector3.zero;
        movement.z = m_speedFactor * GlobalData.Acceleration * Time.deltaTime;

        gameObject.transform.position -= movement;
    }

    private void Explode()
    {
        Instantiate(m_explosionEffect, transform.position, Quaternion.identity);

        Messenger.Broadcast(GameEvent.PICKED_UP_INEDIBLE_ITEM);

        Destroy(gameObject);
    }
}
