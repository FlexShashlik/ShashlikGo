using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static float SpeedFactor = 7f;

    public const float SPEED_FACTOR_DECREASE_COEF = 0.2f;
    
    private Camera m_Camera;

    [SerializeField]
    [Range(0.01f, 0.8f)]
    private float m_TouchBoundX;

    private float m_TouchBoundY = 0.31f;

    void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount <= 0)
            return;

        Ray ray = m_Camera.ScreenPointToRay(Input.GetTouch(0).position);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0f;
        
        if (plane.Raycast(ray, out distance))
        {
            Vector3 movement = Vector3.zero;
            
            if (ray.GetPoint(distance).z < m_TouchBoundY)
            {
                float touchX = ray.GetPoint(distance).x;
                float skewerX = transform.position.x;

                movement.x = GetHorizontalDirection(touchX, skewerX) * Mathf.Abs(touchX - skewerX) * SpeedFactor * Time.deltaTime;

                gameObject.transform.position += movement;
            }
        }
	}

    float GetHorizontalDirection(float touchX, float skewerX)
    {
        if(touchX - skewerX > m_TouchBoundX)
        {
            return 1f;
        }
        else if (skewerX - touchX > m_TouchBoundX)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}
