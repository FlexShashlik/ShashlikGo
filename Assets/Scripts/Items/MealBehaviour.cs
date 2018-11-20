using UnityEngine;

public class MealBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    private float m_SpeedFactor;

    private float m_Bound = -9f;

    private bool m_HasExploded = false;

    private float m_PosOnSkewer;

    [SerializeField]
    private GameObject m_ExplosionEffect;

    private GameObject m_Skewer;

	void Update()
    {
        if(!m_HasExploded)
        {
            Move();
        }
        else
        {
            FollowTheSkewer();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Skewer") && !m_HasExploded)
        {
            m_Skewer = collision.gameObject;

            Explode();
        }
    }

    private void Move()
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
        
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());

        m_PosOnSkewer = GlobalData.FirstItemPos + (GlobalData.ItemsOnSkewer.Count * GlobalData.ItemDistance);

        FollowTheSkewer();

        GlobalData.ItemsOnSkewer.Add(gameObject);
        InputHandler.SpeedFactor -= InputHandler.SPEED_FACTOR_DECREASE_COEF;

        m_HasExploded = true;
    }

    private void FollowTheSkewer()
    {
        if (gameObject != null && m_Skewer != null)
        {
            gameObject.transform.position = new Vector3
                (
                    m_Skewer.transform.position.x,
                    m_Skewer.transform.position.y,
                    m_PosOnSkewer
                );
        }
    }
}
