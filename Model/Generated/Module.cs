using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public partial class Module
    {
        private int _Id;
        private string _Code;
        private string _Name;
        private string _Url;
        private string _Ico;
        private int? _ParentId;
        private DateTime? _CreateTime;
        private bool? _isDelete;
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
        public string Url {
            get
            {
               return _Url;
            }
            set
            {
                _Url=value;
            }
        }
        public string Ico {
            get
            {
               return _Ico;
            }
            set
            {
                _Ico=value;
            }
        }
        public int? ParentId {
            get
            {
               return _ParentId;
            }
            set
            {
                _ParentId=value;
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
        public bool? isDelete {
            get
            {
               return _isDelete;
            }
            set
            {
                _isDelete=value;
            }
        }
    }
}
