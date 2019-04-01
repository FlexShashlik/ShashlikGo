using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static float SpeedFactor = 7f;

    public const float SPEED_FACTOR_DECREASE_COEF = 0.2f;
    
    private Camera m_Camera;
    
    private float m_TouchBoundX = 0.126f;

    private float m_TouchBoundY = 0.31f;

    void Start() => m_Camera = Camera.main;

    void Update()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount <= 0)
            return;

        Ray ray = m_Camera.ScreenPointToRay(Input.GetTouch(0).position);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0f;

        float touchX = ray.GetPoint(distance).x;
        float skewerX = transform.position.x;

        Vector3 movement = Vector3.zero;

        if (plane.Raycast(ray, out distance))
        {
            if (ray.GetPoint(distance).z < m_TouchBoundY)
            {
                touchX = ray.GetPoint(distance).x;
                skewerX = transform.position.x;

                movement.x = GetHorizontalDirection(touchX, skewerX) * SpeedFactor * Time.deltaTime;

                gameObject.transform.position += movement;
            }
        }
#else

#endif
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
