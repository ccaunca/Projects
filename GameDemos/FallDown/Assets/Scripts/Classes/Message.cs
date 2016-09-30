using System.Collections.Generic;

namespace FallDownDemo
{
    public class Message
    {
        private IList<Field> _fields;
        public IList<Field> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                if (value != _fields)
                {
                    _fields = value;
                }
            }
        }
        public Message(IList<Field> fields)
        {
            Fields = fields;
        }
    }
}