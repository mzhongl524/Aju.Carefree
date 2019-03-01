using SqlSugar;
namespace Aju.Carefree.Models
{
    [SugarTable("TB_Area")]
    public class Areas
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Code { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }
        public string ParentCode { get; set; }

        public int Level { get; set; }
    }
}
