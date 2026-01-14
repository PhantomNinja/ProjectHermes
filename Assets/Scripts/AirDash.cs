using System.Collections;
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
    private bool isDashing;
    

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
        if (dashAction.WasCompletedThisFrame())
        {
            StartCoroutine(Dash());
            Debug.Log("Player Dashed");
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = player.defaultGravity;
        player.defaultGravity = 0f;
        player.rb.linearVelocity = new Vector3(dashForce, 0, 0);
        yield return new WaitForSeconds(dashingTime);
        player.defaultGravity = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    //void Dash()
    //{
    //    Vector3 position = player.transform.position;
    //    player.rb.linearVelocity = Vector3.zero;
    //    Debug.Log(position + "player dash start position");
    //    player.rb.linearVelocity = new Vector3(dashSpeed, 0, 0);
    //    Debug.Log(position + "player dash end position");
    //    player.defaultGravity = 0;
    //}
}
