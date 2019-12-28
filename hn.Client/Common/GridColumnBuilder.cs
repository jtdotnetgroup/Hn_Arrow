using System.Linq;
using System.Reflection;
using DevExpress.XtraGrid.Columns;
using hn.Client.CustomAttribute;

namespace hn.Client
{
    public class GridColumnBuilder
    {
        public static DevExpress.XtraGrid.Columns.GridColumn BuildColumn(PropertyInfo pi)
        {
            var result = new GridColumn();

            result.FieldName = pi.Name;
            result.Caption = pi.Name;
            result.Width = 100;

            var colAttr =
                pi.GetCustomAttributes(true).SingleOrDefault(p => p is GridColumnAttribute) as GridColumnAttribute;

            if (colAttr != null)
            {
                result.Caption = colAttr.Text;
                result.VisibleIndex = colAttr.VisiabIndex;
                result.Width = colAttr.Width;
                result.OptionsColumn.ReadOnly = !colAttr.IsEdit;
                result.Visible = colAttr.Visiable;
                result.OptionsColumn.AllowEdit = colAttr.IsEdit;
            }

            return result;
        }
    }
}