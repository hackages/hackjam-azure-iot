using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.Model
{
    public class LampState
    {
        public Position Position { get; set; }
    }

    public enum Position
    {
        On,
        Off
    }
}
