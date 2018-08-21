using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexView : MonoBehaviour
    {
        public RolodexFactory Factory { get; private set; }

        public virtual void Init(RolodexFactory factory)
        {
            
        }
    }
}