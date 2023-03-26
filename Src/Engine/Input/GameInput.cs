using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Engine.Input
{
    public class GameInput
    {
        private Dictionary<int, Mapping> _mappings = new Dictionary<int, Mapping>();

        public int MouseX { get; set; }

        public int MouseY { get; set; }

        public int DeltaX { get; set; }

        public int DeltaY { get; set; }

        public Vector2 MousePosition => new Vector2(MouseX, MouseY);

        public void KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;

                foreach (Key key in mapping.Keys)
                {
                    if (key == e.Key)
                    {
                        mapping.Down = true;
                        mapping.Repeated = e.IsRepeat;
                    }
                }
            }
        }

        // For text input
        public void KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public void KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;

                foreach (Key key in mapping.Keys)
                {
                    if (key == e.Key)
                    {
                        mapping.Up = true;
                        mapping.Repeated = e.IsRepeat;
                    }
                }
            }
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;

                foreach (MouseButton button in mapping.Buttons)
                    if (button == e.Button)
                        mapping.Down = true;
            }
        }

        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;

                foreach (MouseButton button in mapping.Buttons)
                    if (button == e.Button)
                        mapping.Up = true;
            }
        }

        public void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            DeltaX = e.XDelta;
            DeltaY = e.YDelta;
        }

        public void Update()
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;
                mapping.Up = false;
                mapping.Down = false;
                mapping.MWheelDown = false;
                mapping.MWheelUp = false;
            }
        }

        public void AddKeyMap(string name, Key key)
        {
            Mapping mapping = null;

            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                mapping = entry.Value;

                if (mapping.Name.Equals(name))
                    mapping.AddKey(key);
                else
                    mapping = null;
            }

            if (mapping == null)
            {
                mapping = new Mapping(name, key);
                _mappings.Add((int)key, mapping);
            }
        }

        public void AddButtonMap(string name, MouseButton button)
        {
            Mapping mapping = null;

            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                mapping = entry.Value;

                if (mapping.Name.Equals(name))
                    mapping.AddButton(button);
                else
                    mapping = null;
            }

            if (mapping == null)
            {
                mapping = new Mapping(name, button);
                _mappings.Add((int)button, mapping);
            }
        }

        public Mapping Mapping(string name)
        {
            foreach (KeyValuePair<int, Mapping> entry in _mappings)
            {
                Mapping mapping = entry.Value;

                if (mapping.Name.Equals(name))
                    return mapping;
            }

            Console.WriteLine("Error: Couldnt find input mapping with name: " + name);
            return new Mapping("Error");
        }
    }
}
