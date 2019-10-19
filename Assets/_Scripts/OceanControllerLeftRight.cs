using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanControllerLeftRight : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float resetPosition = 5.6f;
    public float resetPoint = -21.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    /// <summary>
    /// This method moves the ocean down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(horizontalSpeed, 0.0f);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// This method resets the ocean to the resetPosition
    /// </summary>
    void Reset()
    {
        transform.position = new Vector2(resetPosition, 0.0f);
    }

    /// <summary>
    /// This method checks if the ocean reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.x <= resetPoint)
        {
            Reset();
        }
    }
}
