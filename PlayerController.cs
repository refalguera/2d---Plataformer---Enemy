using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]

public class PlayerController : MonoBehaviour {

    Rigidbody2D myRigidBody2D;
    Vector2 velocity;
    float moveSpeed = 2;
	// Use this for initialization
	void Start () {
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	public void Move(Vector2 _velocity)
    {
        velocity = _velocity;
    }

    public void FixedUpdate()
    {
        myRigidBody2D.MovePosition(myRigidBody2D.position + velocity * Time.fixedDeltaTime);
    }

    public void Force()
    {
        myRigidBody2D.AddForce(myRigidBody2D.position * moveSpeed);
    }
}
