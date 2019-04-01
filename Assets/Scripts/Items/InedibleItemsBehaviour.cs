using UnityEngine;

public class InedibleItemsBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    private float m_SpeedFactor;

    private float m_Bound = -9f;

    [SerializeField]
    private GameObject m_ExplosionEffect;

    void Update() => Fall();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Skewer"))
            Explode();
    }

    private void Fall()
    {
        if (transform.position.z < m_Bound)
        {
            Destroy(gameObject);
        }

        Vector3 movement = Vector3.zero;
        movement.z = m_SpeedFactor * GlobalData.Acceleration * Time.deltaTime;

        gameObject.transform.position -= movement;
    }

    private void Explode()
    {
        Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);

        VibrationController.Vibrate(200);

        Messenger.Broadcast(GameEvent.INEDIBLE_ITEM_PICKUP);

        Destroy(gameObject);
    }
}
