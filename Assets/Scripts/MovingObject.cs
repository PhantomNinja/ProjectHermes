using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool ObjectIsOn = true;
    [Header("Movement")]
    [Tooltip("The direction you want the platform moving")]
    public Vector2 MoveDirection;
    [Tooltip("The speed you want the platform moving")]
    public float MoveSpeed;
    [Tooltip("How long you want the platform moving")]
    public float MoveTime;
    private float moveTime;

    private void FixedUpdate()
    {
        if (ObjectIsOn == false) return;

        moveTime += Time.deltaTime;

        if(moveTime > MoveTime)
        {
            moveTime = 0;
            MoveDirection *= -1;
        }

        Vector3 newPos = transform.position;
        newPos.z += MoveDirection.x * MoveSpeed * Time.deltaTime;
        newPos.y += MoveDirection.y * MoveSpeed * Time.deltaTime;
        transform.position = newPos;
    }
}
