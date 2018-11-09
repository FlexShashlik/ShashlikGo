using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    private float m_speedFactor;

    private float m_bound = -9f;

    private float m_firstItemPos = -3.4f;
    private float m_itemDistance = 0.5f;

    private bool m_hasExploded = false;

    private float m_posOnSkewer;

    [SerializeField]
    private GameObject m_explosionEffect;

    private GameObject m_skewer;

	void Update()
    {
        if(!m_hasExploded)
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
        m_skewer = collision.gameObject;

        if(m_skewer.CompareTag("Skewer") && !m_hasExploded)
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
        
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());

        m_posOnSkewer = m_firstItemPos + (GlobalData.ItemsOnSkewer.Count * m_itemDistance);

        FollowTheSkewer();

        GlobalData.ItemsOnSkewer.Add(gameObject);
        InputHandler.SpeedFactor -= InputHandler.SPEED_FACTOR_DECREASE_COEF;

        m_hasExploded = true;
    }

    private void FollowTheSkewer()
    {
        if (gameObject != null && m_skewer != null)
        {
            gameObject.transform.position = new Vector3
                (
                    m_skewer.transform.position.x,
                    m_skewer.transform.position.y,
                    m_posOnSkewer
                );
        }
    }
}
