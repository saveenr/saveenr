namespace Isotope.UI.CommonControls
{
    static class WinFormUtil
    {
        public static void SetItemsChecked(System.Windows.Forms.CheckedListBox checkedlistbox, bool check)
        {
            if (checkedlistbox == null)
            {
                throw new System.ArgumentNullException("checkedlistbox");
            }

            foreach (int index in System.Linq.Enumerable.Range(0, checkedlistbox.Items.Count))
            {
                checkedlistbox.SetItemChecked(index, check);
            }

        }
    }
}