using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Message
    {
        public string text;
        public bool shootAgain;

        public Message(string _text , bool _shootAgain)
        {
            text = _text;
            shootAgain = _shootAgain;
        }
    }
}
