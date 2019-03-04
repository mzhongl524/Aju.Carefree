using SqlSugar;
namespace Aju.Carefree.Models
{
    [SugarTable("TB_Area")]
    public class Areas
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDescription = "地区编码", Length = 60, IsNullable = false, ColumnDataType = "nvarchar")]
        public string Code { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "nvarchar", Length = 250, ColumnDescription = "地区名称")]
        public string Name { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "nvarchar", Length = 50, ColumnDescription = "Remark")]
        public string Remark { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "nvarchar", Length = 50, ColumnDescription = "地区上级编码")]
        public string ParentCode { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "int", ColumnDescription = "地区名称")]
        public int Level { get; set; }
    }
}
