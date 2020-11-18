// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Scripts/InputActions/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerStandard"",
            ""id"": ""88c386c5-fd8e-491d-b2f1-3265e64430a0"",
            ""actions"": [
                {
                    ""name"": ""LookY"",
                    ""type"": ""Value"",
                    ""id"": ""912fb961-0899-4e3c-b210-9ef9294af256"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LookX"",
                    ""type"": ""Value"",
                    ""id"": ""3a5de3dc-65b1-452c-a1d3-532c5c732a6f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gun"",
                    ""type"": ""Button"",
                    ""id"": ""cb7d3231-fa39-4311-907f-9822c9394f9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ThrustersX"",
                    ""type"": ""Value"",
                    ""id"": ""5f16383b-f210-40ec-97d3-ed2c3d02e658"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ThrustersZ"",
                    ""type"": ""Value"",
                    ""id"": ""511d6d2a-f29a-4572-8dab-e79c48f2ef08"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ThrustersY"",
                    ""type"": ""Value"",
                    ""id"": ""ba372ea1-4fad-4b30-a985-9afd40bbd7ce"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""aaed1b93-b7c6-4e80-923d-23644379e895"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""022a9da4-69e4-4330-bcec-45dbd039fa65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""PushBack"",
                    ""type"": ""Button"",
                    ""id"": ""0583a930-db09-4826-a685-60ca592e3205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0820ef19-9f03-4871-9196-b99165cc05d1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c217030c-608a-4e5c-a456-c3da485e94b2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f7f4b2b0-bf1d-4827-8a76-7b8b828cbbfd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""09128aaa-4b8d-4e99-b829-99cf14cb69e2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis Gamepad"",
                    ""id"": ""fdf0c270-7f71-4b23-9616-53986a2a9b9c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""dfda0eb2-b0a9-49e5-abe2-9654a62251d4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZ"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3fb57f3e-88c6-46a4-8654-c244edb970d5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0e116ad3-59c8-4a94-a3d2-f470e9c842af"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis Gamepad"",
                    ""id"": ""dcd28224-9f35-4ac6-8c48-43a7a75b298b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZ"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5c46e52-0a20-44ee-ae8d-8fcd5c93d3fc"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b06f6aeb-a4ef-40a2-9e02-27ac10cd5e10"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""96666299-3292-4522-8713-1e29f7d2ed6a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b9aed671-054a-4c1b-9ace-0fdeedc57e8a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0bd03380-ce5e-4b31-9556-95f09deeda60"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis Gamepad"",
                    ""id"": ""11b49996-08fe-4e3c-864b-e98a4c95c0eb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8161dc5-adeb-4e4a-af11-fb01a737e68d"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f48c8e5-6781-4573-9496-e18688c9d6ef"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eba35a9a-a837-4455-b998-2f9c371a1f7c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UserInterface"",
            ""id"": ""4a9c8d23-82b1-4118-8c24-0e5f0954bdda"",
            ""actions"": [
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""7ad259dc-2c97-4f1f-93b4-a17f47127467"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""5c287c59-f705-4c25-9928-9959f7685510"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Screenshot"",
                    ""type"": ""Button"",
                    ""id"": ""1817465e-0b78-4f4d-b5a1-3bb5e2463be6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""afbcf17d-6094-474e-8b5b-8cd9cba4ec8a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06ad26be-43d5-4d6a-932c-68127e0f7713"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a82d803-3d0e-4087-95f7-b7558fe3ebf2"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Screenshot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerStandard
        m_PlayerStandard = asset.FindActionMap("PlayerStandard", throwIfNotFound: true);
        m_PlayerStandard_LookY = m_PlayerStandard.FindAction("LookY", throwIfNotFound: true);
        m_PlayerStandard_LookX = m_PlayerStandard.FindAction("LookX", throwIfNotFound: true);
        m_PlayerStandard_Gun = m_PlayerStandard.FindAction("Gun", throwIfNotFound: true);
        m_PlayerStandard_ThrustersX = m_PlayerStandard.FindAction("ThrustersX", throwIfNotFound: true);
        m_PlayerStandard_ThrustersZ = m_PlayerStandard.FindAction("ThrustersZ", throwIfNotFound: true);
        m_PlayerStandard_ThrustersY = m_PlayerStandard.FindAction("ThrustersY", throwIfNotFound: true);
        m_PlayerStandard_Look = m_PlayerStandard.FindAction("Look", throwIfNotFound: true);
        m_PlayerStandard_PickUp = m_PlayerStandard.FindAction("PickUp", throwIfNotFound: true);
        m_PlayerStandard_PushBack = m_PlayerStandard.FindAction("PushBack", throwIfNotFound: true);
        // UserInterface
        m_UserInterface = asset.FindActionMap("UserInterface", throwIfNotFound: true);
        m_UserInterface_Restart = m_UserInterface.FindAction("Restart", throwIfNotFound: true);
        m_UserInterface_Pause = m_UserInterface.FindAction("Pause", throwIfNotFound: true);
        m_UserInterface_Screenshot = m_UserInterface.FindAction("Screenshot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerStandard
    private readonly InputActionMap m_PlayerStandard;
    private IPlayerStandardActions m_PlayerStandardActionsCallbackInterface;
    private readonly InputAction m_PlayerStandard_LookY;
    private readonly InputAction m_PlayerStandard_LookX;
    private readonly InputAction m_PlayerStandard_Gun;
    private readonly InputAction m_PlayerStandard_ThrustersX;
    private readonly InputAction m_PlayerStandard_ThrustersZ;
    private readonly InputAction m_PlayerStandard_ThrustersY;
    private readonly InputAction m_PlayerStandard_Look;
    private readonly InputAction m_PlayerStandard_PickUp;
    private readonly InputAction m_PlayerStandard_PushBack;
    public struct PlayerStandardActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerStandardActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookY => m_Wrapper.m_PlayerStandard_LookY;
        public InputAction @LookX => m_Wrapper.m_PlayerStandard_LookX;
        public InputAction @Gun => m_Wrapper.m_PlayerStandard_Gun;
        public InputAction @ThrustersX => m_Wrapper.m_PlayerStandard_ThrustersX;
        public InputAction @ThrustersZ => m_Wrapper.m_PlayerStandard_ThrustersZ;
        public InputAction @ThrustersY => m_Wrapper.m_PlayerStandard_ThrustersY;
        public InputAction @Look => m_Wrapper.m_PlayerStandard_Look;
        public InputAction @PickUp => m_Wrapper.m_PlayerStandard_PickUp;
        public InputAction @PushBack => m_Wrapper.m_PlayerStandard_PushBack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerStandard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerStandardActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerStandardActions instance)
        {
            if (m_Wrapper.m_PlayerStandardActionsCallbackInterface != null)
            {
                @LookY.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookY;
                @LookY.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookY;
                @LookY.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookY;
                @LookX.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookX;
                @LookX.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookX;
                @LookX.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLookX;
                @Gun.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnGun;
                @Gun.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnGun;
                @Gun.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnGun;
                @ThrustersX.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersX.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersX.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersZ.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersZ.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersZ.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersY.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @ThrustersY.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @ThrustersY.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @Look.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @PickUp.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @PushBack.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPushBack;
                @PushBack.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPushBack;
                @PushBack.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPushBack;
            }
            m_Wrapper.m_PlayerStandardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LookY.started += instance.OnLookY;
                @LookY.performed += instance.OnLookY;
                @LookY.canceled += instance.OnLookY;
                @LookX.started += instance.OnLookX;
                @LookX.performed += instance.OnLookX;
                @LookX.canceled += instance.OnLookX;
                @Gun.started += instance.OnGun;
                @Gun.performed += instance.OnGun;
                @Gun.canceled += instance.OnGun;
                @ThrustersX.started += instance.OnThrustersX;
                @ThrustersX.performed += instance.OnThrustersX;
                @ThrustersX.canceled += instance.OnThrustersX;
                @ThrustersZ.started += instance.OnThrustersZ;
                @ThrustersZ.performed += instance.OnThrustersZ;
                @ThrustersZ.canceled += instance.OnThrustersZ;
                @ThrustersY.started += instance.OnThrustersY;
                @ThrustersY.performed += instance.OnThrustersY;
                @ThrustersY.canceled += instance.OnThrustersY;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @PushBack.started += instance.OnPushBack;
                @PushBack.performed += instance.OnPushBack;
                @PushBack.canceled += instance.OnPushBack;
            }
        }
    }
    public PlayerStandardActions @PlayerStandard => new PlayerStandardActions(this);

    // UserInterface
    private readonly InputActionMap m_UserInterface;
    private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
    private readonly InputAction m_UserInterface_Restart;
    private readonly InputAction m_UserInterface_Pause;
    private readonly InputAction m_UserInterface_Screenshot;
    public struct UserInterfaceActions
    {
        private @PlayerControls m_Wrapper;
        public UserInterfaceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Restart => m_Wrapper.m_UserInterface_Restart;
        public InputAction @Pause => m_Wrapper.m_UserInterface_Pause;
        public InputAction @Screenshot => m_Wrapper.m_UserInterface_Screenshot;
        public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IUserInterfaceActions instance)
        {
            if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
            {
                @Restart.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRestart;
                @Pause.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPause;
                @Screenshot.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScreenshot;
                @Screenshot.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScreenshot;
                @Screenshot.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScreenshot;
            }
            m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Screenshot.started += instance.OnScreenshot;
                @Screenshot.performed += instance.OnScreenshot;
                @Screenshot.canceled += instance.OnScreenshot;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    public interface IPlayerStandardActions
    {
        void OnLookY(InputAction.CallbackContext context);
        void OnLookX(InputAction.CallbackContext context);
        void OnGun(InputAction.CallbackContext context);
        void OnThrustersX(InputAction.CallbackContext context);
        void OnThrustersZ(InputAction.CallbackContext context);
        void OnThrustersY(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnPushBack(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnRestart(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnScreenshot(InputAction.CallbackContext context);
    }
}
