using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace hn.Client.Views.Common
{
    public partial class FrmSelectList<T> : DevExpress.XtraEditors.XtraForm where T:class
    {

        public List<T> SelectList { get; set; }

        public FrmSelectList()
        {
            InitializeComponent();
        }

        public FrmSelectList(Type entityType,List<T> selectList)
        {
            this.SelectList = selectList;
            var pis = entityType.GetProperties();

            List<GridColumn> cols=new List<GridColumn>();

            foreach (var pi in pis)
            {
                cols.Add(GridColumnBuilder.BuildColumn(pi));
            }

            gridViewSelectList.Columns.AddRange(cols.ToArray());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
             var indexs= gridViewSelectList.GetSelectedRows();

             foreach (var i in indexs)
             {
                 T entity = gridViewSelectList.GetRow(i) as T;

                 if (entity != null)
                 {
                     SelectList.Remove(entity);
                 }
             }
        }
    }
}
