using OpenTK.Input;
using OpenTK;
using Engine.Core;
using Engine.Util;
using Engine.Input;

namespace Engine.Components
{
    public class LockedHorizontalMovement : GameComponent
    {
        private readonly Vector3 _leftPosition;
        private readonly Vector3 _rigthPosition;

        private readonly Mapping _leftMap;
        private readonly Mapping _rightMap;

        private bool _moveLeft;
        private bool _moveRight;

        public float Speed { get; set; }

        public LockedHorizontalMovement(float speed, Vector3 leftPosition, Vector3 rigthPosition)
        {
            Speed = speed;
            _leftPosition = leftPosition;
            _rigthPosition = rigthPosition;

            CoreEngine.Input.AddKeyMap(Direction.Right, Key.A);
            CoreEngine.Input.AddKeyMap(Direction.Left, Key.D);

            _leftMap = CoreEngine.Input.Mapping(Direction.Left);
            _rightMap = CoreEngine.Input.Mapping(Direction.Right);
        }

        public override void Input()
        {
            base.Input();

            if (_leftMap.Down)
                _moveLeft = true;

            if (_leftMap.Up)
                _moveLeft = false;

            if (_rightMap.Down)
                _moveRight = true;

            if (_rightMap.Up)
                _moveRight = false;

            if (_moveLeft)
                Move(Transform.Right, -Speed);

            if (_moveRight)
                Move(Transform.Right, Speed);
        }

        private void Move(Vector3 dir, float amt)
        {
            if ((Transform.Position.X <= _leftPosition.X || _moveLeft) &&
                (Transform.Position.X >= _rigthPosition.X || _moveRight))
            {
                Transform.Position = Vector3.Add(Transform.Position, dir * (amt * Time.DeltaTime));
            }
        }
    }
}
