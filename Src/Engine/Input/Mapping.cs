using System.Collections;
using OpenTK.Input;

namespace Engine.Input
{
    public class Mapping
    {
        public bool _checkMouseWheelDown = false;
        public bool _checkMouseWheelUp = false;

        public string Name { get; }
        public ArrayList Keys { get; } = new ArrayList();
        public ArrayList Buttons { get; } = new ArrayList();

        public bool Pressed { get; set; }

        public bool Down { get; set; }

        public bool Up { get; set; }

        public bool Repeated { get; set; }

        public bool MWheelDown { get; set; }

        public bool MWheelUp { get; set; }

        public Mapping(string name)
            => Name = name;

        public Mapping(string name, Key value)
        {
            Name = name;

            Keys.Add(value);
        }

        public Mapping(string name, MouseButton value)
        {
            Name = name;

            Buttons.Add(value);
        }


        public void AddKey(Key key) 
            => Keys.Add(key);

        public int Key(int i)
        {
            object key = Keys[i];

            return (int)key;
        }


        public void AddButton(MouseButton button) 
            => Buttons.Add(button);

        public int Button(int i)
        {
            object button = Buttons[i];

            return (int)button;
        }
    }
}
