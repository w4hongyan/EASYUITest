using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public partial class Role
    {
        private int _Id;
        private string _Code;
        private string _Name;
        public int Id {
            get
            {
               return _Id;
            }
            set
            {
                _Id=value;
            }
        }
        public string Code {
            get
            {
               return _Code;
            }
            set
            {
                _Code=value;
            }
        }
        public string Name {
            get
            {
               return _Name;
            }
            set
            {
                _Name=value;
            }
        }
    }
}
