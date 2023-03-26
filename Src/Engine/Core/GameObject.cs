using Engine.Graphics.Shaders;
using System.Collections.Generic;

namespace Engine.Core
{
    public class GameObject
    {
        private readonly Transform _transform;
        private List<GameObject> _children;
        private List<GameComponent> _components;

        public Transform Transform => _transform;

        public bool IsActive { get; set; }

        public GameObject()
        {
            _children = new List<GameObject>();
            _components = new List<GameComponent>();
            _transform = new Transform();
            IsActive = true;
        }

        public void AddChild(GameObject child)
        {
            _children.Add(child);
            child.AddToEngine();
            child.Transform.SetParent(_transform);
        }

        public void AddComponent(GameComponent component)
        {
            _components.Add(component);
            component.Parent = this;
        }

        public virtual void Input()
        {
            Transform.Update();

            if (IsActive)
            {
                foreach (GameComponent component in _components)
                    component.Input();

                foreach (GameObject child in _children)
                    child.Input();
            }
        }

        public virtual void FixedUpdate()
        {
            if (IsActive)
            {
                foreach (GameComponent component in _components)
                    component.FixedUpdate();

                foreach (GameObject child in _children)
                    child.FixedUpdate();
            }
        }

        public virtual void Update()
        {
            if (IsActive)
            {
                foreach (GameComponent component in _components)
                    component.Update();

                foreach (GameObject child in _children)
                    child.Update();
            }
        }

        public virtual void Render(Shader shader)
        {
            if (IsActive)
            {
                foreach (GameComponent component in _components)
                    component.Render(shader);

                foreach (GameObject child in _children)
                    child.Render(shader);
            }
        }

        public virtual void Resize()
        {
            if (IsActive)
            {
                foreach (GameComponent component in _components)
                    component.Resize();

                foreach (GameObject child in _children)
                    child.Resize();
            }
        }

        public virtual void Dispose()
        {
            foreach (GameComponent component in _components)
                component.Dispose();

            foreach (GameObject child in _children)
                child.Dispose();
        }

        public virtual void AddToEngine()
        {
            foreach (GameComponent component in _components)
                component.AddToEngine();

            foreach (GameObject child in _children)
                child.AddToEngine();
        }
    }
}
