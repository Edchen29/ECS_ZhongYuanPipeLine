using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.ViewModel
{
    /// <summary>
    /// 用于用户编辑和新增时，角色的VM
    /// </summary>
    public class UserEditVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }

        private string roleName;

        public string RoleName
        {
            get { return roleName; }
            set
            {
                roleName = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RoleName"));
                }
            }
        }

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }



    }
}
