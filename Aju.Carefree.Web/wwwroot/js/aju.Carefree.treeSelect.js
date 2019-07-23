layui.config({ base: '/lib/extends/' }).extend({ treeSelect: 'treeSelect/treeSelect' });
/*
 * @method AjuCarefree_TreeSelect
 * @desc TreeSelect
 */
AjuCarefree_TreeSelect = function (options)
{
    var defaults = {
        elem: null,
        dataUrl: '',
        type: 'get',
        placeholder: '默认提示信息',
        search: true,
        clickCall: null,
        nodeValue: null,
        elemExt: null
    };
    var options = extend(defaults, options);
    layui.use(['treeSelect', 'form'], function ()
    {
        var treeSelect = layui.treeSelect;

        treeSelect.render({
            // 选择器
            elem: options.elem,
            // 数据
            data: options.dataUrl,
            // 异步加载方式：get/post，默认get
            type: options.type,
            // 占位符
            placeholder: options.placeholder,
            // 是否开启搜索功能：true/false，默认false
            search: options.search,
            style: {
                folder: { // 父节点图标
                    enable: true // 是否开启：true/false
                },
                line: { // 连接线
                    enable: true // 是否开启：true/false
                }
            },
            // 点击回调
            click: function (d)
            {
                // console.log(d);
                //console.log(d.treeId); // 得到组件的id
                //console.log(d.current.id); // 得到点击节点的treeObj对象
                //console.log(d.data); // 得到组成树的数据
                options.clickCall(d.current.id);
            },
            // 加载完成后的回调函数
            success: function (d)
            {
                if (options.nodeValue !== null && options.nodeValue !== undefined && options.nodeValue !== "") {
                    treeSelect.checkNode(options.elemExt, options.nodeValue);
                }
            }
        });
    });
};
/*
 * @method 函数用于将一个或多个对象的内容合并到目标对象
 * @desc 函数用于将一个或多个对象的内容合并到目标对象
 */
function extend()
{
    var length = arguments.length;
    var target = arguments[0] || {};
    if (typeof target !== "object" && typeof target !== "function") {
        target = {};
    }
    if (length === 1) {
        target = this;
        i--;
    }
    for (var i = 1; i < length; i++) {
        var source = arguments[i];
        for (var key in source) {
            // 使用for in会遍历数组所有的可枚举属性，包括原型。
            if (Object.prototype.hasOwnProperty.call(source, key)) {
                target[key] = source[key];
            }
        }
    }
    return target;
}