using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerController))]

public class EnemyController : MonoBehaviour {

     bool currentState = false;

    Rigidbody2D myRigidBody2D;
    PlayerController player;
    Vector2 position;
    float speed = 1;
    public float distance;
    private float xStartPosition;
    public bool foundplayer;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        xStartPosition = transform.position.x;
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    public void StateEnemy(bool enemy) {

        currentState = enemy;
    }


    public void Update()
    {
        Vector2 position;

        if (foundplayer == false)
        {
            if ((speed < 0 && transform.position.x < xStartPosition) || (speed > 0 && transform.position.x > xStartPosition + distance))
            {
                speed *= -1;
            }
            position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            enemy.GetComponent<Rigidbody2D>().MovePosition(position);
        }
        
    }


}
