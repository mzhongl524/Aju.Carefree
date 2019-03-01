namespace Aju.Carefree.Common.Model
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class Pagination
    {
        public int rows { get; set; }

        public int page { get; set; }

        public string sidx { get; set; }

        public string sord { get; set; }

        public int records { get; set; }

        public int total
        {
            get
            {
                if (this.records > 0)
                    return this.records % this.rows == 0 ? this.records / this.rows : this.records / this.rows + 1;
                return 0;
            }
        }
    }
}
