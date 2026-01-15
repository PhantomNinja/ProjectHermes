using UnityEngine;

public class MovingObject : HermesObject
{
    public bool ObjectIsOn = true;
    [HideInInspector]
    public bool objectIsOn = true;
    [Header("Movement")]
    [Tooltip("The direction you want the platform moving")]
    public Vector2 MoveDirection;
    [HideInInspector]
    public Vector2 moveDirection;
    [Tooltip("The speed you want the platform moving")]
    public float MoveSpeed;
    [Tooltip("How long you want the platform moving")]
    public float MoveTime;
    private float moveTime;

    public override void StartObject()
    {
        base.StartObject();
        moveDirection = MoveDirection;
        objectIsOn = ObjectIsOn;
    }

    public override void FixedUpdateObject()
    {
        if (objectIsOn == false) return;

        moveTime += Time.deltaTime;

        if(moveTime > MoveTime)
        {
            moveTime = 0;
            moveDirection *= -1;
        }

        Vector3 newPos = transform.position;
        newPos.x += moveDirection.x * MoveSpeed * Time.deltaTime;
        newPos.y += moveDirection.y * MoveSpeed * Time.deltaTime;
        transform.position = newPos;
    }

    public override void ResetObject()
    {
        base.ResetObject();
        objectIsOn = ObjectIsOn;
        moveTime = 0;
        moveDirection = MoveDirection;
    }
}
