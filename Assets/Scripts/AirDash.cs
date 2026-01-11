using UnityEngine;
using UnityEngine.InputSystem;


public class AirDash : MonoBehaviour
{
    CharacterController characterController;

    [Header("Dash Values")]
    [SerializeField] float dashSpeed;
    
    [Header("Input Actions")]
    public InputAction dashAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dashAction = InputSystem.actions.FindAction("AirDash");
        characterController = GetComponent<CharacterController>();
        
        // Currently every 10 units of speed translates to approximately 1 unit traveled in game
        dashSpeed = dashSpeed * 10; 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (dashAction.WasCompletedThisFrame())
        {
            Debug.Log("Player Dashed");
        }
    }
}
