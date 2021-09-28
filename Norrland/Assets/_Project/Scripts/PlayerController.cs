using System.Collections;
using _Project.Scripts.EnumFlags;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input / Actions")]
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    [SerializeField] private SteamVR_Action_Vector2 turnInput;

    [Header("Controllers")]
    [SerializeField] private Hand[] hands;
    [SerializeField] private SteamVR_LaserPointer[] laserPointers;
    [SerializeField] private IcePick[] icePicks;

    [Header("Active Controllers")]
    [SerializeField] private ControllerType controllerType;
    
    [Header("Motion Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField, Range(0.01f, 0.9f)] private float deadZone;
    [SerializeField] private bool constantTurnSpeed;

    private Transform _transform;
    private Transform _head;
    private CharacterController _characterController;

    private void Reset()
    {
        moveSpeed = 2f;
        turnSpeed = 90f;
        deadZone = 0.25f;
    }

    private void Awake()
    {
        GetAllComponents();
    }

    private void GetAllComponents()
    {
        _transform = transform;
        _head = Player.instance.hmdTransform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        StartCoroutine(InitRoutine());
    }

    private IEnumerator InitRoutine()
    {
        while (true)
        {
            int handsReady = 0;
            
            foreach (Hand hand in hands)
            {
                if (!hand.isPoseValid)
                    break;
                
                handsReady++;
            }

            if (handsReady == hands.Length)
            {
                InitControllers();
                break;
            }
            
            yield return null;
        }
    }

    private void InitControllers()
    {
        SetLaserPointers(controllerType.HasFlag(ControllerType.LaserPointers));
        SetTeleporting(controllerType.HasFlag(ControllerType.Teleporting));
        SetMotionMovement(controllerType.HasFlag(ControllerType.MotionMovement));
        SetClimbing(controllerType.HasFlag(ControllerType.Climbing));
    }
    
    private void Update()
    {
        UpdateMotionRotation();

#if UNITY_EDITOR
        ReadDebugInput();
#endif
    }

    private void FixedUpdate()
    {
        UpdateMotionMovement();
    }

    private bool CanReadInput()
    {
        if (!controllerType.HasFlag(ControllerType.Teleporting))
            return true;
        
        if (Teleport.instance == null)
            return true;
        
        return !Teleport.instance.IsTeleporting;
    }

    private void UpdateMotionRotation()
    {
        if (!controllerType.HasFlag(ControllerType.MotionMovement))
            return;
        
        if (!CanReadInput())
            return;

        if (Mathf.Abs(turnInput.axis.x) < deadZone)
            return;

        float pitch = constantTurnSpeed ? Mathf.Sign(turnInput.axis.x) : turnInput.axis.x;
        _transform.Rotate(Vector3.up, pitch * turnSpeed * Time.deltaTime);
    }

    private void UpdateMotionMovement()
    {
        if (!controllerType.HasFlag(ControllerType.MotionMovement))
            return;
        
        if (!CanReadInput())
            return;
        
        Vector3 direction = _head.TransformDirection(moveInput.axis.x, 0f, moveInput.axis.y);
        Vector3 velocity = Vector3.ProjectOnPlane(direction, Vector3.up) * moveSpeed + Physics.gravity;
        _characterController.Move(velocity * Time.deltaTime);
    }

    #region Controllers
    
    private void SetLaserPointers(bool active)
    {
        foreach (SteamVR_LaserPointer pointer in laserPointers)
        {
            pointer.active = active;
            pointer.pointer.SetActive(active);
        }

        controllerType = controllerType.SetFlag(ControllerType.LaserPointers, active);
    }
    
    public void EnableLaserPointers() => SetLaserPointers(true);
    public void DisableLaserPointers() => SetLaserPointers(false);

    private void SetTeleporting(bool active)
    {
        Teleport.instance.enabled = active;
        controllerType = controllerType.SetFlag(ControllerType.Teleporting, active);
    }

    public void EnableTeleporting() => SetTeleporting(true);
    public void DisableTeleporting() => SetTeleporting(false);
    
    private void SetMotionMovement(bool active)
    {
        _characterController.enabled = active;
        
        controllerType = controllerType.SetFlag(ControllerType.MotionMovement, active);
    }

    public void EnableMotionMovement() => SetMotionMovement(true);
    public void DisableMotionMovement() => SetMotionMovement(false);
    
    private void SetClimbing(bool active)
    {
        foreach (IcePick icePick in icePicks)
        {
            if (active)
                icePick.Attach();
            else
                icePick.Detach();
        }

        _characterController.enabled = active;

        controllerType = controllerType.SetFlag(ControllerType.Climbing, active);
    }

    public void EnableClimbing() => SetClimbing(true);
    public void DisableClimbing() => SetClimbing(false);
    
    #endregion

    #region Debug
    
#if UNITY_EDITOR
    
    private void ReadDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetLaserPointers(!controllerType.HasFlag(ControllerType.LaserPointers));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTeleporting(!controllerType.HasFlag(ControllerType.Teleporting));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMotionMovement(!controllerType.HasFlag(ControllerType.MotionMovement));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetClimbing(!controllerType.HasFlag(ControllerType.Climbing));
        }
    }
    
#endif

    #endregion
}