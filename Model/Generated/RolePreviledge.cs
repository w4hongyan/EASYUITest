using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public partial class RolePreviledge
    {
        private int _Id;
        private int? _RoleId;
        private int? _PreviledgeId;
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
        public int? RoleId {
            get
            {
               return _RoleId;
            }
            set
            {
                _RoleId=value;
            }
        }
        public int? PreviledgeId {
            get
            {
               return _PreviledgeId;
            }
            set
            {
                _PreviledgeId=value;
            }
        }
    }
}
