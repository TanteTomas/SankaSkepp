using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Message
    {
        public string text;
        public bool shootAgain;
        public SoundPlayer player;

        public Message(string _text , bool _shootAgain , SoundPlayer _player)
        {
            text = _text;
            shootAgain = _shootAgain;
            player = _player;
        }
    }
}
