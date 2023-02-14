using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInput : MonoBehaviour
{
    private Vector2 moveInput;

    public UnityEvent<Vector2> InputEvent;

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveInput += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveInput += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            moveInput += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveInput += Vector2.right;

        InputEvent?.Invoke(moveInput);
    }
}
