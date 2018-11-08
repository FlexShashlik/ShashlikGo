using System.Collections.Generic;
using UnityEngine;

public class MealSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_meal;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float m_newMealTime = 0.5f;

    private float m_currentMealTime;

    void Start()
    {
        m_currentMealTime = m_newMealTime;
    }
	
	void Update()
    {
        m_currentMealTime -= Time.deltaTime;

        if(m_currentMealTime <= 0f)
        {
            m_currentMealTime = m_newMealTime;

            Instantiate(m_meal, new Vector3(Random.Range(-2.7f, 2.8f), m_meal.transform.position.y, 9f), Quaternion.identity);
        }
	}
}
