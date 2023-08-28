using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        public Vector2 Axis => GetActiveInputAxis();
        public bool IsFire => IsFireTap();
        
        protected const string FIRE_BUTTON = "Fire";
        protected const string HORIZONTAL_AXIS = "Horizontal";
        protected const string VERTICAL_AXIS = "Vertical";
        private const KeyCode SPACE_KEYCODE = KeyCode.Space;

        private Vector2 GetActiveInputAxis()
        {
            var axis = GetSimpleInputAxis();

            if (axis == Vector2.zero)
                axis = GetUnityAxis();

            return axis;
        }
        protected static Vector2 GetUnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(HORIZONTAL_AXIS), UnityEngine.Input.GetAxis(VERTICAL_AXIS));

        protected Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(HORIZONTAL_AXIS), SimpleInput.GetAxis(VERTICAL_AXIS));

        private bool IsFireTap() =>
            IsSimpleInputFireClicked() || IsSpacebarUp();
        
        private bool IsSpacebarUp() =>
            UnityEngine.Input.GetKeyUp(SPACE_KEYCODE);
        
        private static bool IsSimpleInputFireClicked() =>
            SimpleInput.GetButtonUp(FIRE_BUTTON);
    }
}