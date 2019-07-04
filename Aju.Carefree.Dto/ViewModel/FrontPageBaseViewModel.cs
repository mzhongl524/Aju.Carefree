using System.Collections.Generic;

namespace Aju.Carefree.Dto.ViewModel
{
    /// <summary>
    /// 前端页面
    /// </summary>
    public class FrontPageBaseViewModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 操作消息
        /// </summary>
        public string msg { get; set; } = "操作成功";

        /// <summary>
        /// 数据内容
        /// </summary>
        public dynamic data { get; set; }
    }
    /// <summary>
    /// layer ui Table 数据返回格式
    /// </summary>
    public class TableDataModel : FrontPageBaseViewModel
    {
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int count { get; set; }
    }


    public class AuthTreeViewModel : FrontPageBaseViewModel
    {
       
    }
    public class AuthTreeViewModelExt
    {
        public List<AuthTreeViewModelList> trees { get; set; }
    }
    public class AuthTreeViewModelList
    {
        public string name { get; set; }
        public string value { get; set; }
        public bool @checked { get; set; }
        public List<AuthTreeViewModelList> list { get; set; }
    }
}
