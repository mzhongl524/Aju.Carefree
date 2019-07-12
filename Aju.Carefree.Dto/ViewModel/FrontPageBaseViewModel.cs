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

    /// <summary>
    /// Layui Tree 前端数据 ViewModel
    /// </summary>
    public class LayuiTreeViewModel
    {
        /// <summary>
        /// 节点标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 节点唯一索引，用于对指定节点进行各类操作
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 点击节点弹出新窗口对应的 url。需开启 isJump 参数
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 节点是否初始展开，默认 false
        /// </summary>
        public bool spread { get; set; } = true;
        /// <summary>
        /// 节点是否初始为选中状态（如果开启复选框的话），默认 false
        /// </summary>
        public bool @checked { get; set; } = false;

        /// <summary>
        /// 节点是否为禁用状态。默认 false
        /// </summary>
        public bool disabled { get; set; } = false;

        public List<LayuiTreeViewModel> children { get; set; }
    }
}
