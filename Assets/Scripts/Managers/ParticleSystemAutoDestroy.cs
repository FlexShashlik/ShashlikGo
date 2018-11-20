using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem m_ps;

    void Start()
    {
        m_ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!m_ps.IsAlive())
            Destroy(gameObject);
    }
}