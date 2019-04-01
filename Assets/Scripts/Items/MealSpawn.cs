using UnityEngine;

public class MealSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Meal;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float m_NewMealTime;

    private float m_CurrentMealTime;

    void Start() => m_CurrentMealTime = m_NewMealTime;
	
	void Update()
    {
        m_CurrentMealTime -= GlobalData.Acceleration * Time.deltaTime;

        if(m_CurrentMealTime <= 0f)
        {
            m_CurrentMealTime = m_NewMealTime;

            Instantiate(m_Meal, new Vector3(Random.Range(-2.7f, 2.8f), m_Meal.transform.position.y, 9f), Quaternion.identity);
        }
	}
}
