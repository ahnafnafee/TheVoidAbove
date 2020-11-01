// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Scripts/InputActions/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace _Project.Scripts.InputActions
{
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
                    ""name"": """",
                    ""id"": ""a63c8dd6-ed3f-4ba0-a50a-2b34a21d2349"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrustersY"",
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
                    ""name"": """",
                    ""id"": ""a8161dc5-adeb-4e4a-af11-fb01a737e68d"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RestartMap"",
            ""id"": ""02f6e21a-41b9-47f7-aa44-5d6ccda98a8f"",
            ""actions"": [
                {
                    ""name"": ""restart"",
                    ""type"": ""Button"",
                    ""id"": ""9b2d9b13-18b4-4d6e-b9e0-8f11042fa8db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""982ccc63-bbf9-41f0-9723-6148734e7916"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""restart"",
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
            // RestartMap
            m_RestartMap = asset.FindActionMap("RestartMap", throwIfNotFound: true);
            m_RestartMap_restart = m_RestartMap.FindAction("restart", throwIfNotFound: true);
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
                }
            }
        }
        public PlayerStandardActions @PlayerStandard => new PlayerStandardActions(this);

        // RestartMap
        private readonly InputActionMap m_RestartMap;
        private IRestartMapActions m_RestartMapActionsCallbackInterface;
        private readonly InputAction m_RestartMap_restart;
        public struct RestartMapActions
        {
            private @PlayerControls m_Wrapper;
            public RestartMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @restart => m_Wrapper.m_RestartMap_restart;
            public InputActionMap Get() { return m_Wrapper.m_RestartMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(RestartMapActions set) { return set.Get(); }
            public void SetCallbacks(IRestartMapActions instance)
            {
                if (m_Wrapper.m_RestartMapActionsCallbackInterface != null)
                {
                    @restart.started -= m_Wrapper.m_RestartMapActionsCallbackInterface.OnRestart;
                    @restart.performed -= m_Wrapper.m_RestartMapActionsCallbackInterface.OnRestart;
                    @restart.canceled -= m_Wrapper.m_RestartMapActionsCallbackInterface.OnRestart;
                }
                m_Wrapper.m_RestartMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @restart.started += instance.OnRestart;
                    @restart.performed += instance.OnRestart;
                    @restart.canceled += instance.OnRestart;
                }
            }
        }
        public RestartMapActions @RestartMap => new RestartMapActions(this);
        public interface IPlayerStandardActions
        {
            void OnLookY(InputAction.CallbackContext context);
            void OnLookX(InputAction.CallbackContext context);
            void OnGun(InputAction.CallbackContext context);
            void OnThrustersX(InputAction.CallbackContext context);
            void OnThrustersZ(InputAction.CallbackContext context);
            void OnThrustersY(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
        }
        public interface IRestartMapActions
        {
            void OnRestart(InputAction.CallbackContext context);
        }
    }
}
