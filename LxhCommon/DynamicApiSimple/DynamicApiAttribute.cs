using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LxhCommon.DynamicApiSimple
{
    public class DynamicApiAttribute:Attribute
    {
        public string Name;
        public string Order;
        public string Description;
        public DynamicApiAttribute()
        {
            
        }
        public DynamicApiAttribute(string _name,string _order,string _description)
        {
            Name = _name;
            Order = _order; 
            Description = _description;
        }
    }
}
