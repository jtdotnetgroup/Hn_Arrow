using System;

namespace hn.Client.CustomAttribute
{
    public class GridColumnAttribute:Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 列表排序
        /// </summary>
        public int VisiabIndex { get; set; }

        public bool Visiable { get; set; }

        /// <summary>
        /// 可否编辑
        /// </summary>
        public bool IsEdit { get; set; }
        
        public GridColumnAttribute(string text,bool isEdit=false,int width=100,int index=0,bool visiable=true)
        {
            this.Text = text;
            this.VisiabIndex = index;
            this.IsEdit = isEdit;
            this.Width = width;
            this.Visiable = visiable;
        }
    }
}