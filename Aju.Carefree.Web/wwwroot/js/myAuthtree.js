layui.config({ base: '/lib/extends/' }).extend({ authtree: 'authtree' });
Authtree = function (url, bindId)
{
    layui.use(['jquery', 'authtree', 'form', 'layer'], function ()
    {
        var $ = layui.jquery;
        var authtree = layui.authtree;
        var layer = layui.layer;
        $.ajax({
            url: url,
            dataType: 'json',
            success: function (data)
            {
                // debugger;
                // 渲染时传入渲染目标ID，树形结构数据（具体结构看样例，checked表示默认选中），以及input表单的名字
                authtree.render(bindId, data.data.trees, {
                    inputname: 'authids[]',
                    layfilter: 'lay-check-auth',
                    hidechoose: true,
                    openchecked: false,
                    autowidth: true
                });
                authtree.on('deptChange(lay-check-auth)', function (data)
                {
                    console.log('监听到显示层数改变', data);
                });
            },
            error: function (xml, errstr, err)
            {
                layer.alert(errstr + '，获取数据失败，请检查！');
            }
        });
    });
};
/*
 * 获取子节点选中的值
 * bindId ：绑定的tree ID
 */
getAuthtreeLeafValue = function (bindId)
{
    var val = "";
    layui.use(['jquery', 'authtree', 'form', 'layer'], function ()
    {
        var authtree = layui.authtree;
        val = authtree.getLeaf(bindId);
    });
    return val;
};
/*
 * 获取所有选中的值
 * bindId ：绑定的tree ID
 */
getAuthtreeCheckedValue = function (bindId)
{
    var val = "";
    layui.use(['jquery', 'authtree', 'form', 'layer'], function ()
    {
        var authtree = layui.authtree;
        val = authtree.getChecked(bindId);
    });
    return val;
}