layui.config({ base: '/lib/extends/' }).extend({ treeTable: 'treeTable' });
TreeTable = function (options)
{
    debugger;
    var defaults = {
        id: null,
        url: null,
        cols: null,
        treeIdName: null,//id字段的名称
        treePidName: null,//pid字段的名称
        btnSearchId: null,//搜索按钮的id
        searchInputId: null//搜索框的id
    };
    var options = jQuery.extend(defaults, options);
    layui.use(['layer', 'table', 'treetable'], function ()
    {
        var $ = layui.jquery;
        var table = layui.table;
        var layer = layui.layer;
        var treetable = layui.treetable;
        layer.load(2);
        treetable.render({
            treeColIndex: 1,//树形图标显示在第几列
            treeSpid: -1,//最上级的父级id
            treeIdName: '',//id字段的名称
            treePidName: '',//pid字段的名称
            treeDefaultClose: false, //是否默认折叠
            treeLinkage: false,//父级展开时是否自动展开所有子级
            elem: options.id,
            url: options.url,
            page: false,
            cols: options.cols,
            done: function ()
            {
                layer.closeAll('loading');
            }
        });

        $(options.btnSearchId).click(function ()
        {
            var keyword = $(options.searchInputId).val();
            var searchCount = 0;
            $(options.id).next('.treeTable').find('.layui-table-body tbody tr td').each(function ()
            {
                $(this).css('background-color', 'transparent');
                var text = $(this).text();
                if (keyword != '' && text.indexOf(keyword) >= 0) {
                    $(this).css('background-color', 'rgba(250,230,160,0.5)');
                    if (searchCount == 0) {
                        treetable.expandAll('#auth-table');
                        $('html,body').stop(true);
                        $('html,body').animate({ scrollTop: $(this).offset().top - 150 }, 500);
                    }
                    searchCount++;
                }
            });
            if (keyword == '') {
                layer.msg("请输入搜索内容", { icon: 5 });
            } else if (searchCount == 0) {
                layer.msg("没有匹配结果", { icon: 5 });
            }
        });
    });
}