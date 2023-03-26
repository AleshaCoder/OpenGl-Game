using System.Drawing;
using OpenTK;
using OpenTK.Input;
using Engine.Core;
using Engine.Input;

namespace Engine.Components
{
    public class FreeLook : GameComponent
    {
        private const string UnlockMouse = "UnlockMouse";

        /** Max and minimum angle you can look around */
        private static float MAX_LOOK_ANGLE = 89.99f;
        private static float MIN_LOOK_ANGLE = -89.99f;

        public float UpAngle = 0;

        private bool _mouseLocked = false;
        private readonly float _mouseSensitivity;
        private readonly Mapping _unlockMapping;

        public FreeLook(float sensitivity)
        {
            _mouseSensitivity = sensitivity;

            CoreEngine.Input.AddButtonMap(UnlockMouse, MouseButton.Middle);
            _unlockMapping = CoreEngine.Input.Mapping(UnlockMouse);
        }

        public override void Input()
        {
            base.Input();
            Vector2 center = new Vector2(CoreEngine.Window.Bounds.Left + (CoreEngine.Window.Bounds.Width / 2), CoreEngine.Window.Bounds.Top + (CoreEngine.Window.Bounds.Height / 2));

            if (_unlockMapping.Up && _mouseLocked)
            {
                //CoreEngine.window.CursorVisible = true;
                _mouseLocked = false;
            }
            else if (_unlockMapping.Up)
            {
                //CoreEngine.window.CursorVisible = false;
                _mouseLocked = true;
            }

            if (_mouseLocked)
            {
                Vector2 mousePos = new Vector2(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                Vector2 deltaPos = center - mousePos;

                bool rotY = deltaPos.X != 0;
                bool rotX = deltaPos.Y != 0;

                Vector3 euler = Vector3.Zero;

                if (rotY)
                    euler.X = (float)MathHelper.DegreesToRadians(-deltaPos.X * _mouseSensitivity);

                if (rotX)
                    euler.Z = (float)MathHelper.DegreesToRadians(-deltaPos.Y * _mouseSensitivity);


                Transform.Rotate(euler);

                //Vector3 right = Vector3.Transform(new Vector3(1, 0, 0), Transform.Transformation()).Normalized();

                //if(rotY)
                //    Transform.Rotate(new Vector3(0, 1, 0), (float)MathHelper.DegreesToRadians(-deltaPos.X * sensitivity));
                //if(rotX)
                //    Transform.Rotate(new Vector3(1, 0, 0), (float)MathHelper.DegreesToRadians(-deltaPos.Y * sensitivity));

                if (rotY || rotX)
                    System.Windows.Forms.Cursor.Position = new Point((int)center.X, (int)center.Y);
            }

        }
    }
}
