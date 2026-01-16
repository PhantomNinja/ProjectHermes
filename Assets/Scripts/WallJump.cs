using Unity.Mathematics;
using UnityEngine;

public class WallJump : MonoBehaviour
{

    public GameObject wallCheck;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector3 wallCheckBoxSize;
    [SerializeField] float m_MaxDistance;
    bool m_HitDetect;
    RaycastHit m_Hit;
    PlayerController player;
    bool isWallSliding;
    [SerializeField] float wallSlidingSpeed;

    

    private void Start()
    {
        player = PlayerController.instance;
    }

    void FixedUpdate()
    {
        WallSlide();
    }
    bool isWall()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(wallCheck.transform.position, wallCheckBoxSize * 0.5f, wallCheck.transform.forward, out m_Hit, wallCheck.transform.rotation, m_MaxDistance, wallLayer);
        if (m_HitDetect) 
            Debug.Log("hit" + m_Hit.collider.name);
        return m_HitDetect;
    }
    private void WallSlide()
    {
        
        if (isWall() && !player.isGrounded && player.direction != 0.0f)
        {
            player.canMove = false;
            isWallSliding = true;
            player.rb.linearVelocity = new Vector3(0, Mathf.Clamp(player.rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue), player.rb.linearVelocity.z);
            player.gravityScale = 0.0f;
            Debug.Log("sliding");
            player.currentAnimation = PlayerController.animationEnum.climbing;
        }
        else
        {
            player.canMove = true;
            isWallSliding = false;
            player.gravityScale = 1.0f;
        }
    }
    
    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(wallCheck.transform.position, wallCheck.transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(wallCheck.transform.position + wallCheck.transform.forward * m_Hit.distance, wallCheckBoxSize);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(wallCheck.transform.position, wallCheck.transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(wallCheck.transform.position + wallCheck.transform.forward * m_MaxDistance, wallCheckBoxSize);
        }
    }
}
