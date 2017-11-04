using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    private const string horizontalAxisId = "Horizontal";
    private const string verticalAxisId = "Vertical";
    private const string rotateId = "Rotate";

    [SerializeField]
    private float inputTransform = 1;
    [SerializeField]
	private Vector2 horizontalMoveVersor = new Vector3(1, 0, 0);
	[SerializeField]
	private Vector2 verticalMoveVersor = new Vector3(0, 1, 0);
    [SerializeField]
    private float rotationStep = 1.0f;

    private Rigidbody2D rigidBody;
    private IMoveRestrictor moveRestrictor;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        moveRestrictor = GetComponent<IMoveRestrictor>();
    }

	private void Update()
    {
		var moveVector = Input.GetAxis(horizontalAxisId) * inputTransform * horizontalMoveVersor;
		moveVector += Input.GetAxis(verticalAxisId) * inputTransform * verticalMoveVersor;
        var newPosition = rigidBody.position + moveVector;
        newPosition = moveRestrictor.Restrict(newPosition, rigidBody.position);
        rigidBody.MovePosition(newPosition);
        
        var rotation = Input.GetAxis(rotateId) * inputTransform * rotationStep;
        rigidBody.MoveRotation(rigidBody.rotation + rotation);
    }
}
