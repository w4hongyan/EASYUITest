using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public partial class UserRole
    {
        private int _Id;
        private int? _UserId;
        private int? _RoleId;
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
        public int? UserId {
            get
            {
               return _UserId;
            }
            set
            {
                _UserId=value;
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
    }
}
