using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
[RequireComponent (typeof(Enemy2))]

public class Player : MonoBehaviour {

    public float moveSpeed = 5;
    PlayerController controller;
    private bool enemy = false;

    public bool Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player is attacking enemy");
            Enemy = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Enemy = false;
        }
    }


}
