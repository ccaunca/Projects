namespace Budget.Base
{
    internal class CalendarViewMode
    {
        private bool add;
        private bool edit;
        private bool view;
        public bool Add
        {
            get { return add; }
            set { add = value; }
        }
        public bool Edit
        {
            get { return edit; }
            set { edit = value; }
        }
        public bool View
        {
            get { return view; }
            set { view = value; }
        }

        public CalendarViewMode(bool view, bool add, bool edit)
        {
            this.view = view;
            this.add = add;
            this.edit = edit;
        }
    }
}