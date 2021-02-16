using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.ViewModel
{
    public class PageInfoVM: INotifyPropertyChanged
    {
        private int pageIndex;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PageIndex")); }
        }

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PageSize")); }
        }

        private int totalCount;

        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalCount")); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 设置分页默认值
        /// </summary>
        public PageInfoVM()
        {
            PageIndex = 1;
            PageSize = 30;
            TotalCount = 0;
        }
    }
}
