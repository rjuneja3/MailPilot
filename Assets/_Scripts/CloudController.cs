/*
Name: Rohan Juneja
Student ID : 300987725
Last Modified by: Rohan Juneja
Last Modified Date: October 19th, 2019
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;

public class CloudController : MonoBehaviour
{
    [Header("Speed Values")]
    [SerializeField]
    public Speed horizontalSpeedRange;

    [SerializeField]
    public Speed verticalSpeedRange;

    public float verticalSpeed;
    public float horizontalSpeed;

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the nanme of the scene and player controls changes according to the scene
        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                Move();
                CheckBounds();
                break;
            case "Main":
                Move();
                CheckBounds();
                break;
            case "Level2":
                MoveLeftRight();
                CheckBoundsLeftRight();
                break;
            case "End":
                Move();
                CheckBounds();
                break;
        }
    }

    /// <summary>
    /// This method moves the cloud down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(horizontalSpeed, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// This method moves the cloud right to left the screen by horizontalSpeed

    void MoveLeftRight()
    {
        Vector2 newPosition = new Vector2(verticalSpeed, horizontalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }


    /// <summary>
    /// This method resets the cloud to the resetPosition
    /// </summary>
    void Reset()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
                verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);

                float randomPosition = Random.Range(boundary.Left, boundary.Right);
                transform.position = new Vector2(randomPosition, Random.Range(boundary.Top, boundary.Top + 2.0f));
                break;
            case "Main":

                horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
                verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);

                float randomXPosition = Random.Range(boundary.Left, boundary.Right);
                transform.position = new Vector2(randomXPosition, Random.Range(boundary.Top, boundary.Top + 2.0f));
                break;
            case "Level2":

                verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);
                horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);

                float randomYPosition = Random.Range(boundary.Bottom, boundary.Top);
                transform.position = new Vector2(Random.Range(boundary.Right, boundary.Right + 2.0f), randomYPosition);
                break;
        }
    }

    /// <summary>
    /// This method checks if the cloud reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.y <= boundary.Bottom)
        {
            Reset();
        }
    }

    /// This method checks if the cloud reaches the left boundary
    /// and then it Resets it
    void CheckBoundsLeftRight()
    {
        if (transform.position.x <= boundary.Left)
        {
            Reset();
        }
    }
}
