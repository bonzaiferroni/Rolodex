using System.Collections.Generic;
using System.Text;

namespace Rolodex.Core
{
    public interface IRoloMenu
    {
        bool CanClose { get; }
        string Name { get; }
        RoloMenu Parent { get; }
        List<IRoloItem> Items { get; }
    }
    
    public class RoloMenu : IRoloMenu
    {
        public RoloMenu(string name, RoloMenu parent, bool canClose = true)
        {
            Name = name;
            Parent = parent;
            CanClose = canClose;
        }

        public bool CanClose { get; }
        public string Name { get; set; }
        public RoloMenu Parent { get; set; }
        public List<IRoloItem> Items { get; } = new List<IRoloItem>();
        
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