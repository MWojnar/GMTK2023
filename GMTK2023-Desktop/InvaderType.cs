using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class InvaderType
    {
        public Sprite Sprite;
        public int Cost;
        public Type EntityClass;

        public InvaderType(Sprite sprite, int cost, Type entityClass)
        {
            Sprite = sprite;
            Cost = cost;
            EntityClass = entityClass;
        }
    }
}
