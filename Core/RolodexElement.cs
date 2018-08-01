using System;
using System.Text;

namespace Rolodex.Core
{
    public class RolodexElement
    {
        public RolodexElement(string name, Action action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; }
        public Action Action { get; }
        
        
    }
}