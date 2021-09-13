using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean teleportAction;
    [SerializeField] private SteamVR_Action_Boolean turnAction;
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    [SerializeField] private SteamVR_Action_Vector2 turnInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField, Range(0.01f, 0.9f)] private float deadZone;
    [SerializeField] private bool constantTurnSpeed;

    private Transform _transform;
    private Transform _head;
    private CharacterController _characterController;

    private bool _readMovement;

    private void Reset()
    {
        moveSpeed = 2f;
        turnSpeed = 90f;
        deadZone = 0.25f;
    }

    private void Awake()
    {
        _transform = transform;
        _head = Player.instance.hmdTransform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // _readMovement = CanReadInput();
        // _readMovement = !Teleport.instance.IsTeleporting;

        if (!_readMovement)
            return;

        if (Mathf.Abs(turnInput.axis.x) < deadZone)
            return;

        float pitch = constantTurnSpeed ? Mathf.Sign(turnInput.axis.x) : turnInput.axis.x;
        _transform.Rotate(Vector3.up, pitch * turnSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _readMovement = !Teleport.instance.IsTeleporting;
        
        if (!_readMovement)
            return;
        
        Vector3 direction = _head.TransformDirection(moveInput.axis.x, 0f, moveInput.axis.y);
        Vector3 velocity = Vector3.ProjectOnPlane(direction, Vector3.up) * moveSpeed + Physics.gravity;
        _characterController.Move(velocity * Time.deltaTime);
    }

    // private bool CanReadInput()
    // {
    //     return !Teleport.instance.IsTeleporting;
    // }
}