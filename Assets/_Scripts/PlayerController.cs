using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Speed speed;
    public Boundary boundary;

    public GameController gameController;

    // private instance variables
    private AudioSource _thunderSound;
    private AudioSource _yaySound;

    // Start is called before the first frame update
    void Start()
    {
        _thunderSound = gameController.audioSources[(int)SoundClip.THUNDER];
        _yaySound = gameController.audioSources[(int)SoundClip.YAY];
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the active game scene/level and then decides which player controls to use
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                Move();
                CheckBounds();
                break;
            case "Level2":
                MoveUpDown();
                CheckBoundsUpDown();
                break;
        }
        
           
        
        
    }
    

    public void Move()
    {
        Vector2 newPosition = transform.position;

        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            newPosition += new Vector2(speed.max, 0.0f);
        }

        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            newPosition += new Vector2(speed.min, 0.0f);
        }

        transform.position = newPosition;
    }

    public void CheckBounds()
    {
        // check right boundary
        if(transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }

        // check left boundary
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cloud":
                _thunderSound.Play();
                gameController.Lives -= 1;
                break;
            case "Island":
                _yaySound.Play();
                gameController.Score += 100;
                break;
        }
    }

    public void MoveUpDown()
    {
        //Gets  Vertical axis and make the player move.
        Vector2 currentPosition = transform.position;

        if (Input.GetAxisRaw("Vertical") > 0.0f)
        {
            currentPosition += new Vector2(0.0f, speed.max);
        }
        if (Input.GetAxisRaw("Vertical") < 0.0f)
        {
            currentPosition -= new Vector2(0.0f, speed.min);
           
        }

        transform.position = currentPosition;
    }

    public void CheckBoundsUpDown()
    {
        //Restricts the player in boundaries
     
        if (transform.position.y > boundary.Top)
        {
            transform.position = new Vector2(transform.position.x, boundary.Top);
        }
        if (transform.position.y < boundary.Bottom)
        {
            transform.position = new Vector2(transform.position.x, boundary.Bottom);
        }
    }

}
