using OpenTK.Input;
using OpenTK;
using Engine.Core;
using Engine.Util;
using Engine.Input;

namespace Engine.Components
{
    public class FreeMove : GameComponent
    {
        private float _speed;

        private readonly Mapping _forwardMap;
        private readonly Mapping _backwardMap;
        private readonly Mapping _leftMap;
        private readonly Mapping _rightMap;
        private readonly Mapping _upMap;
        private readonly Mapping _downMap;

        private bool _moveForward;
        private bool _moveBackward;
        private bool _moveLeft;
        private bool _moveRight;
        private bool _moveUp;
        private bool _moveDown;

        public FreeMove(float speed)
        {
            _speed = speed;

            CoreEngine.Input.AddKeyMap(Direction.Forward, Key.W);
            CoreEngine.Input.AddKeyMap(Direction.Backward, Key.S);
            CoreEngine.Input.AddKeyMap(Direction.Left, Key.A);
            CoreEngine.Input.AddKeyMap(Direction.Right, Key.D);
            CoreEngine.Input.AddKeyMap(Direction.Up, Key.Space);
            CoreEngine.Input.AddKeyMap(Direction.Down, Key.LControl);

            _forwardMap = CoreEngine.Input.Mapping(Direction.Forward);
            _backwardMap = CoreEngine.Input.Mapping(Direction.Backward);
            _leftMap = CoreEngine.Input.Mapping(Direction.Left);
            _rightMap = CoreEngine.Input.Mapping(Direction.Right);
            _upMap = CoreEngine.Input.Mapping(Direction.Up);
            _downMap = CoreEngine.Input.Mapping(Direction.Down);
        }

        public override void Input()
        {
            base.Input();

            if (_forwardMap.Down)
                _moveForward = true;

            if (_forwardMap.Up)
                _moveForward = false;

            if (_backwardMap.Down)
                _moveBackward = true;

            if (_backwardMap.Up)
                _moveBackward = false;

            if (_leftMap.Down)
                _moveLeft = true;

            if (_leftMap.Up)
                _moveLeft = false;

            if (_rightMap.Down)
                _moveRight = true;

            if (_rightMap.Up)
                _moveRight = false;

            if (_upMap.Down)
                _moveUp = true;

            if (_upMap.Up)
                _moveUp = false;

            if (_downMap.Down)
                _moveDown = true;

            if (_downMap.Up)
                _moveDown = false;

            if (_moveForward)
                Move(Transform.Forward, _speed);

            if (_moveBackward)
                Move(Transform.Forward, -_speed);

            if (_moveLeft)
                Move(Transform.Right, -_speed);

            if (_moveRight)
                Move(Transform.Right, _speed);
        }

        private void Move(Vector3 dir, float amt)
            => Transform.Position = Vector3.Add(Transform.Position, dir * (amt * Time.DeltaTime));
    }
}
