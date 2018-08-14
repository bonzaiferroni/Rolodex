using System.Collections.Generic;
using System.Text;

namespace Rolodex.Core
{
    public class RolodexMenu
    {
        public RolodexMenu(string name, RolodexMenu parent)
        {
            Name = name;
            Parent = parent;
        }
        
        public string Name { get; set; }
        public RolodexMenu Parent { get; }
        public List<RolodexElement> Elements { get; } = new List<RolodexElement>();
        
        public override string ToString()
        {
            var sb = new StringBuilder(Name);
            var parent = Parent;
            while (parent != null)
            {
                sb.Insert(0, " > ");
                sb.Insert(0, parent.Name);
                parent = parent.Parent;
            }
            
            return sb.ToString();
        }
    }
}