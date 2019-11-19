using System;

namespace TuringMachine.Model
{
    public class State : ICloneable
    {
        public string Name { get; set; }
        public string Action { get; set; }

        public object Clone()
        {
            return new State 
            {
                Name = this.Name,
                Action = this.Action
            };
        }
    }
}
