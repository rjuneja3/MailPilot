using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;

public class IslandController : MonoBehaviour
{
    public float verticalSpeed = 0.05f;
    public float horizontalSpeed = 0.05f;

    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                Move();
                CheckBounds();
                break;
            case "Level2":
                MoveLeftRight();
                CheckBoundsLeftRight();
                break;
        }
    }

    /// <summary>
    /// This method moves the ocean down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// This method moves the ocean down the screen by verticalSpeed
    /// </summary>
    void MoveLeftRight()
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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                float randomXPosition = Random.Range(boundary.Left, boundary.Right);
                transform.position = new Vector2(randomXPosition, boundary.Top);
                break;
            case "Level2":
                float randomYPosition = Random.Range(boundary.Bottom, boundary.Top);
                transform.position = new Vector2(boundary.Right, randomYPosition);
                break;
        }
        
    }

    /// <summary>
    /// This method checks if the ocean reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.y <= boundary.Bottom)
        {
            Reset();
        }
    }

    void CheckBoundsLeftRight()
    {
        if (transform.position.x <= boundary.Left)
        {
            Reset();
        }
    }
}
