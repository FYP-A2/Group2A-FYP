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
                    ""name"": ""HUD"",
                    ""type"": ""Button"",
                    ""id"": ""9219f8a5-e0df-425e-a605-97814a0c4fc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""dd7892bc-2eca-486f-a44e-7cce7f9b6d52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
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
                    ""id"": ""da942760-0591-4404-836e-6615123fdeef"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HUD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7ca0cea-02af-486f-aab0-c1af4ea24f09"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HUD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea83d080-0db5-49d4-9abc-131a9ad87259"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HUD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74dc4bd5-dd16-4c26-b6db-e190c104532f"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HUD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d44e428-2d97-46e7-b188-7e6b136dd04b"",
                    ""path"": ""<Keyboard>/f12"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HUD"",
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
                    ""action"": ""Space"",
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
        m_Player_HUD = m_Player.FindAction("HUD", throwIfNotFound: true);
        m_Player_Space = m_Player.FindAction("Space", throwIfNotFound: true);
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
    private readonly InputAction m_Player_HUD;
    private readonly InputAction m_Player_Space;
    public struct PlayerActions
    {
        private @Custom_IA m_Wrapper;
        public PlayerActions(@Custom_IA wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASD => m_Wrapper.m_Player_WASD;
        public InputAction @MouseX => m_Wrapper.m_Player_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Player_MouseY;
        public InputAction @HUD => m_Wrapper.m_Player_HUD;
        public InputAction @Space => m_Wrapper.m_Player_Space;
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
                @HUD.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHUD;
                @HUD.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHUD;
                @HUD.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHUD;
                @Space.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                @Space.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                @Space.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
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
                @HUD.started += instance.OnHUD;
                @HUD.performed += instance.OnHUD;
                @HUD.canceled += instance.OnHUD;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnWASD(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnHUD(InputAction.CallbackContext context);
        void OnSpace(InputAction.CallbackContext context);
    }
}
