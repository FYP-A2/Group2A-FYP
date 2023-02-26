// GENERATED AUTOMATICALLY FROM 'Assets/00_InputActions/Custom_IA.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Custom_IA : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Custom_IA()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Custom_IA"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""da734d41-1950-49a1-bf7d-0ca609440a39"",
            ""actions"": [
                {
                    ""name"": ""WASD"",
                    ""type"": ""Value"",
                    ""id"": ""9d081ae0-deb4-4319-8967-953e2a8876a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""Value"",
                    ""id"": ""147c6834-6d99-4ac4-be31-8087e063c1fa"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""Value"",
                    ""id"": ""22b1ec75-1b7d-4702-bc45-16636ae022c9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""dd7892bc-2eca-486f-a44e-7cce7f9b6d52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookAt"",
                    ""type"": ""Value"",
                    ""id"": ""b01fb72e-1c5d-43f6-8877-5a8c406d9333"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""05d1f2fa-9aca-4feb-a899-bdbb0bc25d04"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4154191f-27a3-46ff-90a5-4fe0aa73c199"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c190d12c-a563-44fc-b721-3efd5d34b507"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4437d31a-1400-4258-ab3d-2e8d25cda9ed"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8fb5c9a1-b356-49f6-9bda-33b2e40d35d8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50d99468-b337-4f74-883c-cf3b7738cb7c"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27a0efbb-b6ca-4047-b2ef-a7df82d26476"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77a0b28b-8e52-4847-a581-5085014f7a86"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8cd6546-c7ce-44ba-8700-f6d06218a3e0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""HUD"",
            ""id"": ""af243b54-ab7b-4870-87f9-8a11b35fbbf0"",
            ""actions"": [
                {
                    ""name"": ""F1"",
                    ""type"": ""Button"",
                    ""id"": ""3965e7b7-2d6a-4f3b-be8c-80e086b23bbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F2"",
                    ""type"": ""Button"",
                    ""id"": ""6be59c38-e5a2-44ae-aa88-ae2ad7342a3b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F3"",
                    ""type"": ""Button"",
                    ""id"": ""c69b5ace-2e98-4665-850b-825fee03deb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F4"",
                    ""type"": ""Button"",
                    ""id"": ""dd6ada26-a13f-4225-8623-a58d0054d453"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee005b4e-abcc-48e6-a1ac-cea401ae3731"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2848d82a-b80f-47e2-8fc2-7b48a88b5cee"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26a28371-4dc2-469a-8729-f0be261b55b1"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce8e6d79-ffa7-4e43-b25a-c2db739b87b8"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TP"",
            ""id"": ""5d73636f-6062-4d5d-af6d-f7c9b8f99499"",
            ""actions"": [
                {
                    ""name"": ""F5"",
                    ""type"": ""Button"",
                    ""id"": ""95211d22-00c4-419c-8858-4b4afd9aab78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F6"",
                    ""type"": ""Button"",
                    ""id"": ""0ac30b50-f0f5-4aad-8ed8-e5aec6fcd730"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F7"",
                    ""type"": ""Button"",
                    ""id"": ""ab288755-d3a7-4e75-82fa-ba11d1443f80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F8"",
                    ""type"": ""Button"",
                    ""id"": ""f5efd761-27e1-46cf-83f8-06accc2d53aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b21f7416-5390-4d15-a8e7-3e6c5b968ef7"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1056e32e-0a2e-498b-b112-3168123b2d3c"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5656c2d2-af1b-4ff8-b621-c6883737ceae"",
                    ""path"": ""<Keyboard>/f7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab30d169-4bfd-40f8-baa7-6ab7d1059e71"",
                    ""path"": ""<Keyboard>/f8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Start Menu"",
            ""id"": ""8e42bfe7-190f-40a5-84fd-44115d2c1c6c"",
            ""actions"": [
                {
                    ""name"": ""Any"",
                    ""type"": ""Button"",
                    ""id"": ""79805b47-5238-4bf4-9eb6-23dc51b7b4f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d031a815-f108-4664-b709-808ae7ae8eca"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_WASD = m_Player.FindAction("WASD", throwIfNotFound: true);
        m_Player_MouseX = m_Player.FindAction("MouseX", throwIfNotFound: true);
        m_Player_MouseY = m_Player.FindAction("MouseY", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_LookAt = m_Player.FindAction("LookAt", throwIfNotFound: true);
        // HUD
        m_HUD = asset.FindActionMap("HUD", throwIfNotFound: true);
        m_HUD_F1 = m_HUD.FindAction("F1", throwIfNotFound: true);
        m_HUD_F2 = m_HUD.FindAction("F2", throwIfNotFound: true);
        m_HUD_F3 = m_HUD.FindAction("F3", throwIfNotFound: true);
        m_HUD_F4 = m_HUD.FindAction("F4", throwIfNotFound: true);
        // TP
        m_TP = asset.FindActionMap("TP", throwIfNotFound: true);
        m_TP_F5 = m_TP.FindAction("F5", throwIfNotFound: true);
        m_TP_F6 = m_TP.FindAction("F6", throwIfNotFound: true);
        m_TP_F7 = m_TP.FindAction("F7", throwIfNotFound: true);
        m_TP_F8 = m_TP.FindAction("F8", throwIfNotFound: true);
        // Start Menu
        m_StartMenu = asset.FindActionMap("Start Menu", throwIfNotFound: true);
        m_StartMenu_Any = m_StartMenu.FindAction("Any", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_WASD;
    private readonly InputAction m_Player_MouseX;
    private readonly InputAction m_Player_MouseY;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_LookAt;
    public struct PlayerActions
    {
        private @Custom_IA m_Wrapper;
        public PlayerActions(@Custom_IA wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASD => m_Wrapper.m_Player_WASD;
        public InputAction @MouseX => m_Wrapper.m_Player_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Player_MouseY;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @LookAt => m_Wrapper.m_Player_LookAt;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @WASD.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @MouseX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseY;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @LookAt.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookAt;
                @LookAt.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookAt;
                @LookAt.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookAt;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LookAt.started += instance.OnLookAt;
                @LookAt.performed += instance.OnLookAt;
                @LookAt.canceled += instance.OnLookAt;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // HUD
    private readonly InputActionMap m_HUD;
    private IHUDActions m_HUDActionsCallbackInterface;
    private readonly InputAction m_HUD_F1;
    private readonly InputAction m_HUD_F2;
    private readonly InputAction m_HUD_F3;
    private readonly InputAction m_HUD_F4;
    public struct HUDActions
    {
        private @Custom_IA m_Wrapper;
        public HUDActions(@Custom_IA wrapper) { m_Wrapper = wrapper; }
        public InputAction @F1 => m_Wrapper.m_HUD_F1;
        public InputAction @F2 => m_Wrapper.m_HUD_F2;
        public InputAction @F3 => m_Wrapper.m_HUD_F3;
        public InputAction @F4 => m_Wrapper.m_HUD_F4;
        public InputActionMap Get() { return m_Wrapper.m_HUD; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HUDActions set) { return set.Get(); }
        public void SetCallbacks(IHUDActions instance)
        {
            if (m_Wrapper.m_HUDActionsCallbackInterface != null)
            {
                @F1.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnF1;
                @F1.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnF1;
                @F1.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnF1;
                @F2.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnF2;
                @F2.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnF2;
                @F2.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnF2;
                @F3.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnF3;
                @F3.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnF3;
                @F3.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnF3;
                @F4.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnF4;
                @F4.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnF4;
                @F4.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnF4;
            }
            m_Wrapper.m_HUDActionsCallbackInterface = instance;
            if (instance != null)
            {
                @F1.started += instance.OnF1;
                @F1.performed += instance.OnF1;
                @F1.canceled += instance.OnF1;
                @F2.started += instance.OnF2;
                @F2.performed += instance.OnF2;
                @F2.canceled += instance.OnF2;
                @F3.started += instance.OnF3;
                @F3.performed += instance.OnF3;
                @F3.canceled += instance.OnF3;
                @F4.started += instance.OnF4;
                @F4.performed += instance.OnF4;
                @F4.canceled += instance.OnF4;
            }
        }
    }
    public HUDActions @HUD => new HUDActions(this);

    // TP
    private readonly InputActionMap m_TP;
    private ITPActions m_TPActionsCallbackInterface;
    private readonly InputAction m_TP_F5;
    private readonly InputAction m_TP_F6;
    private readonly InputAction m_TP_F7;
    private readonly InputAction m_TP_F8;
    public struct TPActions
    {
        private @Custom_IA m_Wrapper;
        public TPActions(@Custom_IA wrapper) { m_Wrapper = wrapper; }
        public InputAction @F5 => m_Wrapper.m_TP_F5;
        public InputAction @F6 => m_Wrapper.m_TP_F6;
        public InputAction @F7 => m_Wrapper.m_TP_F7;
        public InputAction @F8 => m_Wrapper.m_TP_F8;
        public InputActionMap Get() { return m_Wrapper.m_TP; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TPActions set) { return set.Get(); }
        public void SetCallbacks(ITPActions instance)
        {
            if (m_Wrapper.m_TPActionsCallbackInterface != null)
            {
                @F5.started -= m_Wrapper.m_TPActionsCallbackInterface.OnF5;
                @F5.performed -= m_Wrapper.m_TPActionsCallbackInterface.OnF5;
                @F5.canceled -= m_Wrapper.m_TPActionsCallbackInterface.OnF5;
                @F6.started -= m_Wrapper.m_TPActionsCallbackInterface.OnF6;
                @F6.performed -= m_Wrapper.m_TPActionsCallbackInterface.OnF6;
                @F6.canceled -= m_Wrapper.m_TPActionsCallbackInterface.OnF6;
                @F7.started -= m_Wrapper.m_TPActionsCallbackInterface.OnF7;
                @F7.performed -= m_Wrapper.m_TPActionsCallbackInterface.OnF7;
                @F7.canceled -= m_Wrapper.m_TPActionsCallbackInterface.OnF7;
                @F8.started -= m_Wrapper.m_TPActionsCallbackInterface.OnF8;
                @F8.performed -= m_Wrapper.m_TPActionsCallbackInterface.OnF8;
                @F8.canceled -= m_Wrapper.m_TPActionsCallbackInterface.OnF8;
            }
            m_Wrapper.m_TPActionsCallbackInterface = instance;
            if (instance != null)
            {
                @F5.started += instance.OnF5;
                @F5.performed += instance.OnF5;
                @F5.canceled += instance.OnF5;
                @F6.started += instance.OnF6;
                @F6.performed += instance.OnF6;
                @F6.canceled += instance.OnF6;
                @F7.started += instance.OnF7;
                @F7.performed += instance.OnF7;
                @F7.canceled += instance.OnF7;
                @F8.started += instance.OnF8;
                @F8.performed += instance.OnF8;
                @F8.canceled += instance.OnF8;
            }
        }
    }
    public TPActions @TP => new TPActions(this);

    // Start Menu
    private readonly InputActionMap m_StartMenu;
    private IStartMenuActions m_StartMenuActionsCallbackInterface;
    private readonly InputAction m_StartMenu_Any;
    public struct StartMenuActions
    {
        private @Custom_IA m_Wrapper;
        public StartMenuActions(@Custom_IA wrapper) { m_Wrapper = wrapper; }
        public InputAction @Any => m_Wrapper.m_StartMenu_Any;
        public InputActionMap Get() { return m_Wrapper.m_StartMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StartMenuActions set) { return set.Get(); }
        public void SetCallbacks(IStartMenuActions instance)
        {
            if (m_Wrapper.m_StartMenuActionsCallbackInterface != null)
            {
                @Any.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAny;
                @Any.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAny;
                @Any.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAny;
            }
            m_Wrapper.m_StartMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Any.started += instance.OnAny;
                @Any.performed += instance.OnAny;
                @Any.canceled += instance.OnAny;
            }
        }
    }
    public StartMenuActions @StartMenu => new StartMenuActions(this);
    public interface IPlayerActions
    {
        void OnWASD(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLookAt(InputAction.CallbackContext context);
    }
    public interface IHUDActions
    {
        void OnF1(InputAction.CallbackContext context);
        void OnF2(InputAction.CallbackContext context);
        void OnF3(InputAction.CallbackContext context);
        void OnF4(InputAction.CallbackContext context);
    }
    public interface ITPActions
    {
        void OnF5(InputAction.CallbackContext context);
        void OnF6(InputAction.CallbackContext context);
        void OnF7(InputAction.CallbackContext context);
        void OnF8(InputAction.CallbackContext context);
    }
    public interface IStartMenuActions
    {
        void OnAny(InputAction.CallbackContext context);
    }
}
