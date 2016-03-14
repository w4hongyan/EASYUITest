using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public partial class User
    {
        private int _Id;
        private string _Name;
        private string _Email;
        private string _Password;
        private DateTime? _CreateTime;
        private DateTime? _LastLoginTime;
        private string _LastLoginIP;
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
        public string Email {
            get
            {
               return _Email;
            }
            set
            {
                _Email=value;
            }
        }
        public string Password {
            get
            {
               return _Password;
            }
            set
            {
                _Password=value;
            }
        }
        public DateTime? CreateTime {
            get
            {
               return _CreateTime;
            }
            set
            {
                _CreateTime=value;
            }
        }
        public DateTime? LastLoginTime {
            get
            {
               return _LastLoginTime;
            }
            set
            {
                _LastLoginTime=value;
            }
        }
        public string LastLoginIP {
            get
            {
               return _LastLoginIP;
            }
            set
            {
                _LastLoginIP=value;
            }
        }
    }
}
