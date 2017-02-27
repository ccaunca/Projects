using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Enum;

namespace Budget.Base
{
    public class ViewMode
    {
        private CalendarModeEnum _mode;
        public CalendarModeEnum Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
            }
        }
        public ViewMode(bool isView, bool isAdd, bool isEdit)
        {
            SetMode(isView, isAdd, isEdit);
        }
        private void SetMode(bool isView, bool isAdd, bool isEdit)
        {
            if (isView)
            {
                Mode = CalendarModeEnum.View;
            }
            else if (isAdd)
            {
                Mode = CalendarModeEnum.Add;
            }
            else if (isEdit)
            {
                Mode = CalendarModeEnum.Edit;
            }
        }
    }
}
