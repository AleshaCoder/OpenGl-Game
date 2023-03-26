using Engine.Util;
using OpenTK;

namespace Engine.Core
{
    public class Transform
    {
        private Transform _parent;
        private Matrix4 _parentMatrix;
        private Vector3 _oldPosition;
        private Quaternion _oldRotation;
        private Vector3 _oldScale;

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public Vector3 Forward => Vector3.Transform(new Vector3(0, 0, 1), Rotation);

        public Vector3 Right => Vector3.Transform(new Vector3(1, 0, 0), Rotation);

        public Vector3 Up => Vector3.Transform(new Vector3(0, 1, 0), Rotation);

        public Transform()
        {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = Vector3.One;
            _parentMatrix = Matrix4.Identity;
        }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            _parentMatrix = Matrix4.Identity;
        }

        public void Update()
        {
            if (_oldPosition != null)
            {
                _oldPosition = Position;
                _oldRotation = Rotation;
                _oldScale = Scale;
            }
            else
            {
                _oldPosition = Vector3.Add(Position, Vector3.One);
                _oldRotation = Rotation * 0.5f;
                _oldScale = Vector3.Add(Scale, Vector3.One);
            }
        }

        public void Rotate(Vector3 axis, float angle)
            => Rotation = Quaternion.FromAxisAngle(axis, angle) * Rotation;

        public void Rotate(Quaternion rotationAmount)
            => Rotation = rotationAmount * Rotation;

        public void Rotate(Vector3 euler)
            => Rotation = MathUtil.CreateFromEuler(euler) * Rotation;

        public bool HasChanged()
        {
            if (_parent != null && _parent.HasChanged())
                return true;

            if (Position != _oldPosition)
                return true;

            if (Rotation != _oldRotation)
                return true;

            if (Scale != _oldScale)
                return true;

            return false;
        }

        public Matrix4 Transformation()
        {
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateTranslation(new Vector3(-Position.X, -Position.Y, Position.Z));
            model *= Matrix4.CreateFromQuaternion(Rotation);
            model *= Matrix4.CreateScale(Scale);

            return ParentMatrix() * model;
        }

        private Matrix4 ParentMatrix()
            => _parentMatrix = (_parent != null && _parent.HasChanged())
                                ? _parent.Transformation()
                                : _parentMatrix;

        public void SetParent(Transform parent)
            => _parent = parent;

        public Vector3 TransformedPos()
            => Vector3.Transform(Position, _parentMatrix);

        public Quaternion TransformedRot()
        {
            Quaternion parentRotation = (_parent != null) ? _parent.TransformedRot() : Quaternion.Identity;
            return parentRotation * Rotation;
        }
    }
}
