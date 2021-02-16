using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsuser")]
    public class User : BaseModel
    {
        private string userCode;

        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; HandlerPropertyChanged("UserCode"); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; HandlerPropertyChanged("UserName"); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; HandlerPropertyChanged("Password"); }
        }

        private string partment;

        public string Partment
        {
            get { return partment; }
            set { partment = value; HandlerPropertyChanged("Partment"); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; HandlerPropertyChanged("Address"); }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value;HandlerPropertyChanged("Phone"); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value;HandlerPropertyChanged("Remark"); }
        }

        private bool disable;

        public bool Disable
        {
            get { return disable; }
            set { disable = value;HandlerPropertyChanged("Disable"); }
        }


        //逻辑外键
        private List<Role> roles;
        [Editable(false)]
        public List<Role> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = new List<Role>();
                    return roles;
                }
                else
                {
                    return roles;
                }
            }
            set { roles = value; }
        }


    }
}
