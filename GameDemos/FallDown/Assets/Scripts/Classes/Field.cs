using UnityEngine;

namespace FallDownDemo
{
    public class Field
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                }
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = value;
                }
            }
        }
        public Field(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}