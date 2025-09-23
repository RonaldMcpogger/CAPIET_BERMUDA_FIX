using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControllerScan : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Maps")]
    [SerializeField] private string actionMapName = "Gameplay";

    [Header("Actions")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string cam = "Camera";
    [SerializeField] private string grabLeft = "Grab Left";
    [SerializeField] private string grabRight = "Grab Right";
    [SerializeField] private string useLeft = "Use Left";
    [SerializeField] private string useRight = "Use Right";
    [SerializeField] private string jump = "Jump";

    private InputAction moveAction;
    private InputAction camAction;
    private InputAction grabLeftAction;
    private InputAction grabRightAction;
    private InputAction useLeftAction;
    private InputAction useRightAction;
    private InputAction jumpAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 camInput { get; private set; }
    public bool grabbedLeft { get; private set; }
    public bool grabbedRight { get; private set; }
    public bool usedLeft { get; private set; }
    public bool usedRight { get; private set; }
    public bool jumped { get; private set; }

    public static ControllerScan Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(movement);
        camAction = playerControls.FindActionMap(actionMapName).FindAction(cam);
        grabLeftAction = playerControls.FindActionMap(actionMapName).FindAction(grabLeft);
        grabRightAction = playerControls.FindActionMap(actionMapName).FindAction(grabRight);
        useLeftAction = playerControls.FindActionMap(actionMapName).FindAction(useLeft);
        useRightAction = playerControls.FindActionMap(actionMapName).FindAction(useRight);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;
        camAction.performed += context => camInput = context.ReadValue<Vector2>();
        camAction.canceled += context => camInput = Vector2.zero;

        grabLeftAction.performed += context => grabbedLeft = true;
        grabLeftAction.canceled += context => grabbedLeft = false;
        grabRightAction.performed += context => grabbedRight = true;
        grabRightAction.canceled += context => grabbedRight = false;

        useLeftAction.performed += context => usedLeft = true;
        useLeftAction.canceled += context => usedLeft = false;
        useRightAction.performed += context => usedRight = true;
        useRightAction.canceled += context => usedRight = false;

        jumpAction.performed += context => jumped = true;
        jumpAction.canceled += context => jumped = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        camAction.Enable();
        grabLeftAction.Enable();
        grabRightAction.Enable();
        useLeftAction.Enable();
        useRightAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        camAction.Disable();
        grabLeftAction.Disable();
        grabRightAction.Disable();
        useLeftAction.Disable();
        useRightAction.Disable();
        jumpAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Gamepad.current != null)
            Debug.Log("Connected");
        else Debug.Log("Error: No Gamepad");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
