using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsmenuoperation")]
    public class MenuOperation : BaseModel
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        private string menuName;
        public string MenuName
        {
            get { return menuName; }
            set
            {
                menuName = value;
                HandlerPropertyChanged("MenuName");
            }
        }

        /// <summary>
        /// 父
        /// </summary>
        private int? parentId;
        public int? ParentId
        {
            get { return parentId; }
            set
            {
                parentId = value;
                HandlerPropertyChanged("ParentId");
            }
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; HandlerPropertyChanged("Url"); }
        }

        /// <summary>
        /// 菜单类型
        /// </summary>
        private string menuType;
        public string MenuType
        {
            get { return menuType; }
            set { menuType = value; HandlerPropertyChanged("MenuType"); }
        }


        public bool Visible { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        private string perms;
        public string Perms
        {
            get { return perms; }
            set { perms = value; HandlerPropertyChanged("Perms"); }
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; HandlerPropertyChanged("Remark"); }
        }



        //获取子菜单
        private List<MenuOperation> children;
        public List<MenuOperation> Children
        {
            get
            {
                if (children == null)
                {
                    children = new List<MenuOperation>();
                }
                return children;
            }
            set { children = value; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        private int? orderNum;
        public int? OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; HandlerPropertyChanged("OrderNum"); }
        }

        /// <summary>
        /// 对应角色是否有权限
        /// </summary>
        private bool hasPerm;
        [Editable(false)]
        public bool HasPerm
        {
            get { return hasPerm; }
            set { hasPerm = value; HandlerPropertyChanged("HasPerm"); }
        }


    }
}
