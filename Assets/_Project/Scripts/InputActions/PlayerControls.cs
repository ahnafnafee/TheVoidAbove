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
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""934f8376-c827-4f9a-b6e1-a9a8c1913cc5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ThrustersX"",
                    ""type"": ""Value"",
                    ""id"": ""5f16383b-f210-40ec-97d3-ed2c3d02e658"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ThrustersZ"",
                    ""type"": ""Value"",
                    ""id"": ""511d6d2a-f29a-4572-8dab-e79c48f2ef08"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ThrustersZDash"",
                    ""type"": ""Value"",
                    ""id"": ""42ee5c1c-7b97-4a16-9c3c-519498008379"",
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
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""0583a930-db09-4826-a685-60ca592e3205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ea6ade84-c27a-4dfa-b900-ce2a4b0135e8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ManualBrake"",
                    ""type"": ""Button"",
                    ""id"": ""bac429f1-cac1-40b4-be58-a5305ebc65a0"",
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
                    ""id"": ""32e48f5e-4114-4463-a3e4-f3ea29bb1564"",
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
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b47f8409-c5e5-4408-8ed9-81fc357a5875"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""65c71bf0-6968-4060-ac6c-8ccfb46da657"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZDash"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9bf540ee-662d-4460-bcdb-1ef292b9cd03"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""12756bcc-b02b-41b8-a227-909173198611"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis Gamepad"",
                    ""id"": ""60af81cc-9619-4393-bcd4-f9096344a613"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersZDash"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cc873a2-d59d-44d9-95d2-68dfa1ad411a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManualBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""412829d8-531f-4aa2-b916-f2d27f521859"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
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
                },
                {
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""14fa623d-6586-4fa0-b30b-e9f027869eb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""8af43037-b5fd-46c7-bdb1-91f4f7939874"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""afbcf17d-6094-474e-8b5b-8cd9cba4ec8a"",
                    ""path"": ""<Keyboard>/f5"",
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
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Screenshot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e14ae8e8-e247-41f2-ad0f-c4cea6031348"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93453345-c7ad-4fbf-abef-56c19dbbb9ef"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Load"",
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
        m_PlayerStandard_Aim = m_PlayerStandard.FindAction("Aim", throwIfNotFound: true);
        m_PlayerStandard_ThrustersX = m_PlayerStandard.FindAction("ThrustersX", throwIfNotFound: true);
        m_PlayerStandard_ThrustersZ = m_PlayerStandard.FindAction("ThrustersZ", throwIfNotFound: true);
        m_PlayerStandard_ThrustersZDash = m_PlayerStandard.FindAction("ThrustersZDash", throwIfNotFound: true);
        m_PlayerStandard_ThrustersY = m_PlayerStandard.FindAction("ThrustersY", throwIfNotFound: true);
        m_PlayerStandard_Look = m_PlayerStandard.FindAction("Look", throwIfNotFound: true);
        m_PlayerStandard_PickUp = m_PlayerStandard.FindAction("PickUp", throwIfNotFound: true);
        m_PlayerStandard_Dash = m_PlayerStandard.FindAction("Dash", throwIfNotFound: true);
        m_PlayerStandard_MouseLook = m_PlayerStandard.FindAction("MouseLook", throwIfNotFound: true);
        m_PlayerStandard_ManualBrake = m_PlayerStandard.FindAction("ManualBrake", throwIfNotFound: true);
        // UserInterface
        m_UserInterface = asset.FindActionMap("UserInterface", throwIfNotFound: true);
        m_UserInterface_Restart = m_UserInterface.FindAction("Restart", throwIfNotFound: true);
        m_UserInterface_Pause = m_UserInterface.FindAction("Pause", throwIfNotFound: true);
        m_UserInterface_Screenshot = m_UserInterface.FindAction("Screenshot", throwIfNotFound: true);
        m_UserInterface_Save = m_UserInterface.FindAction("Save", throwIfNotFound: true);
        m_UserInterface_Load = m_UserInterface.FindAction("Load", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerStandard_Aim;
    private readonly InputAction m_PlayerStandard_ThrustersX;
    private readonly InputAction m_PlayerStandard_ThrustersZ;
    private readonly InputAction m_PlayerStandard_ThrustersZDash;
    private readonly InputAction m_PlayerStandard_ThrustersY;
    private readonly InputAction m_PlayerStandard_Look;
    private readonly InputAction m_PlayerStandard_PickUp;
    private readonly InputAction m_PlayerStandard_Dash;
    private readonly InputAction m_PlayerStandard_MouseLook;
    private readonly InputAction m_PlayerStandard_ManualBrake;
    public struct PlayerStandardActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerStandardActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookY => m_Wrapper.m_PlayerStandard_LookY;
        public InputAction @LookX => m_Wrapper.m_PlayerStandard_LookX;
        public InputAction @Gun => m_Wrapper.m_PlayerStandard_Gun;
        public InputAction @Aim => m_Wrapper.m_PlayerStandard_Aim;
        public InputAction @ThrustersX => m_Wrapper.m_PlayerStandard_ThrustersX;
        public InputAction @ThrustersZ => m_Wrapper.m_PlayerStandard_ThrustersZ;
        public InputAction @ThrustersZDash => m_Wrapper.m_PlayerStandard_ThrustersZDash;
        public InputAction @ThrustersY => m_Wrapper.m_PlayerStandard_ThrustersY;
        public InputAction @Look => m_Wrapper.m_PlayerStandard_Look;
        public InputAction @PickUp => m_Wrapper.m_PlayerStandard_PickUp;
        public InputAction @Dash => m_Wrapper.m_PlayerStandard_Dash;
        public InputAction @MouseLook => m_Wrapper.m_PlayerStandard_MouseLook;
        public InputAction @ManualBrake => m_Wrapper.m_PlayerStandard_ManualBrake;
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
                @Aim.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnAim;
                @ThrustersX.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersX.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersX.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersX;
                @ThrustersZ.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersZ.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersZ.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZ;
                @ThrustersZDash.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZDash;
                @ThrustersZDash.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZDash;
                @ThrustersZDash.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersZDash;
                @ThrustersY.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @ThrustersY.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @ThrustersY.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnThrustersY;
                @Look.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnLook;
                @PickUp.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnPickUp;
                @Dash.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnDash;
                @MouseLook.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnMouseLook;
                @ManualBrake.started -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnManualBrake;
                @ManualBrake.performed -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnManualBrake;
                @ManualBrake.canceled -= m_Wrapper.m_PlayerStandardActionsCallbackInterface.OnManualBrake;
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
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @ThrustersX.started += instance.OnThrustersX;
                @ThrustersX.performed += instance.OnThrustersX;
                @ThrustersX.canceled += instance.OnThrustersX;
                @ThrustersZ.started += instance.OnThrustersZ;
                @ThrustersZ.performed += instance.OnThrustersZ;
                @ThrustersZ.canceled += instance.OnThrustersZ;
                @ThrustersZDash.started += instance.OnThrustersZDash;
                @ThrustersZDash.performed += instance.OnThrustersZDash;
                @ThrustersZDash.canceled += instance.OnThrustersZDash;
                @ThrustersY.started += instance.OnThrustersY;
                @ThrustersY.performed += instance.OnThrustersY;
                @ThrustersY.canceled += instance.OnThrustersY;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @ManualBrake.started += instance.OnManualBrake;
                @ManualBrake.performed += instance.OnManualBrake;
                @ManualBrake.canceled += instance.OnManualBrake;
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
    private readonly InputAction m_UserInterface_Save;
    private readonly InputAction m_UserInterface_Load;
    public struct UserInterfaceActions
    {
        private @PlayerControls m_Wrapper;
        public UserInterfaceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Restart => m_Wrapper.m_UserInterface_Restart;
        public InputAction @Pause => m_Wrapper.m_UserInterface_Pause;
        public InputAction @Screenshot => m_Wrapper.m_UserInterface_Screenshot;
        public InputAction @Save => m_Wrapper.m_UserInterface_Save;
        public InputAction @Load => m_Wrapper.m_UserInterface_Load;
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
                @Save.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSave;
                @Save.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSave;
                @Save.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSave;
                @Load.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnLoad;
                @Load.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnLoad;
                @Load.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnLoad;
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
                @Save.started += instance.OnSave;
                @Save.performed += instance.OnSave;
                @Save.canceled += instance.OnSave;
                @Load.started += instance.OnLoad;
                @Load.performed += instance.OnLoad;
                @Load.canceled += instance.OnLoad;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    public interface IPlayerStandardActions
    {
        void OnLookY(InputAction.CallbackContext context);
        void OnLookX(InputAction.CallbackContext context);
        void OnGun(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnThrustersX(InputAction.CallbackContext context);
        void OnThrustersZ(InputAction.CallbackContext context);
        void OnThrustersZDash(InputAction.CallbackContext context);
        void OnThrustersY(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
        void OnManualBrake(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnRestart(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnScreenshot(InputAction.CallbackContext context);
        void OnSave(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
    }
}
