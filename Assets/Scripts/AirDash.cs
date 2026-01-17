using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;


public class AirDash : MonoBehaviour
{
    PlayerController player;

    [Header("Dash Values")]
    [SerializeField] float dashForce;
    public float dashingTime = 0.2f;
    public float dashCooldown = 1.0f;
    [Header("Dash variables")]
    private bool canDash = true;
    public bool isDashing { private set; get; }

    [Header("Input Actions")]
    public InputAction dashAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerController.instance;
        dashAction = InputSystem.actions.FindAction("AirDash");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (dashAction.WasCompletedThisFrame() && canDash)
        {
            StartCoroutine(Dash());
            Debug.Log("Player Dashed");
        }
    }
    
    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        player.canMove = false;
        player.currentAnimation = PlayerController.animationEnum.dashing;
        player.gravityScale = 0f;
        player.rb.linearVelocity = new Vector3(player.maxSpeed + dashForce * player.lastDirection, 0, 0);


        yield return new WaitForSeconds(dashingTime);
        player.gravityScale = 1.0f;
        isDashing=false;
        player.canMove = true;
        player.currentAnimation= PlayerController.animationEnum.idle;


        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
