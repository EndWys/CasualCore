using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameplayInput
{
    public interface ITouchInputHandler : IService
    {
        event Action<Vector2> OnTouchDown;
        event Action<Vector2> OnTouchMove;
        event Action<Vector2> OnTouchUp;
    }

    public class GameTouchInputHandler : ITouchInputHandler
    {
        private readonly GameInput _input;
        private bool _wasTouching;

        public event Action<Vector2> OnTouchDown;
        public event Action<Vector2> OnTouchMove;
        public event Action<Vector2> OnTouchUp;

        public GameTouchInputHandler()
        {
            _input = new GameInput();
        }

        public void UpdateInput()
        {
            Vector2 pos = _input.Gameplay.TouchPosition.ReadValue<Vector2>();
            bool isPressed = _input.Gameplay.TouchPress.IsPressed();

            if (isPressed && !_wasTouching)
            {
                OnTouchDown?.Invoke(pos);
            }
            else if (isPressed && _wasTouching)
            {
                OnTouchMove?.Invoke(pos);
            }
            else if (!isPressed && _wasTouching)
            {
                OnTouchUp?.Invoke(pos);
            }

            _wasTouching = isPressed;
        }

        public void OnRegister() 
        { 
            _input.Enable(); 
        }

        public void OnUnregister() { 
            _input.Disable();
            _input.Dispose();
        }
    }
}