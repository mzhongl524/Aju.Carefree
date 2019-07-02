//弹出层
modalOpen = function (options)
{
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };

    var options = $.extend(defaults, options);
    var _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
    var _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';

    top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fixed: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        maxmin: true,
        yes: function (index, layero)
        {
            options.callBack('layui-layer-iframe' + index);
        },
        cancel: function ()
        {
            return true;
        }
    });
};