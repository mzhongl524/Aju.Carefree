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
    //layer弹出层显示在top顶层
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
//确认弹框
modalConfirm = function (content, callBack)
{
    top.layer.confirm(content, {
        icon: "fa-exclamation-circle",
        title: "系统提示",
        btn: ['确认', '取消'],
        btnclass: ['btn btn-primary', 'btn btn-danger']
    }, function ()
        {
            callBack(true);
        }, function ()
        {
            callBack(false);
        });
};
//Alert弹框
modalAlert = function (content, type)
{
    var icon = "";
    if (type === 'success') {
        icon = "fa-check-circle";
    }
    if (type === 'error') {
        icon = "fa-times-circle";
    }
    if (type === 'warning') {
        icon = "fa-exclamation-circle";
    }
    top.layer.alert(content, {
        icon: icon,
        title: "系统提示",
        btn: ['确认'],
        btnclass: ['btn btn-primary']
    });
};
//MsgModal
modalMsg = function (content, type)
{
    if (type !== undefined) {
        var icon = 0;
        if (type === 'success') {
            icon = 1;
        }
        if (type === 'error') {
            icon = 2;
        }
        if (type === 'warning') {
            icon = 0;
        }
        top.layer.msg(content, { icon: icon, time: 4000, shift: 5 });
        top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        top.layer.msg(content);
    }
};
//关闭Modal
modalClose = function ()
{
    var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
    var IsClose = $IsdialogClose.is(":checked");
    if ($IsdialogClose.length === 0) {
        IsClose = true;
    }
    if (IsClose) {
        top.layer.close(index);
    } else {
        location.reload();
    }
};
//Form Submit
submitForm = function (options)
{
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    loading(true, options.loading);
    window.setTimeout(function ()
    {
        if ($('[name=AntiforgeryKey_Aju]').length > 0) {
            options.param["AntiforgeryKey_Aju"] = $('[name=AntiforgeryKey_Aju]').val();
        }
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data)
            {
                if (data.state === "success") {
                    options.success(data);
                    modalMsg(data.message, data.state);
                    if (options.close === true) {
                        modalClose();
                    }
                } else {
                    modalAlert(data.message, data.state);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown)
            {
                loading(false);
                //alert(errorThrown);
                console.log(errorThrown);
                modalMsg(textStatus, "error");
            },
            beforeSend: function ()
            {
                loading(true, options.loading);
            },
            complete: function ()
            {
                loading(false);
            }
        });
    }, 500);
};
//Form delete
deleteForm = function (options)
{
    var defaults = {
        prompt: "注：您确定要删除该项数据吗？",
        url: "",
        param: [],
        loading: "正在删除数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r)
    {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function ()
            {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data)
                    {
                        if (data.state == "success") {
                            options.success(data);
                            $.modalMsg(data.message, data.state);
                        } else {
                            $.modalAlert(data.message, data.state);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown)
                    {
                        $.loading(false);
                        $.modalMsg(errorThrown, "error");
                    },
                    beforeSend: function ()
                    {
                        $.loading(true, options.loading);
                    },
                    complete: function ()
                    {
                        $.loading(false);
                    }
                });
            }, 500);
        }
    });
};

reload = function ()
{
    location.reload();
    return false;
};

loading = function (bool, text)
{
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}