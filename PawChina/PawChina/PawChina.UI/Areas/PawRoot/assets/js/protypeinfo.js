$(document).ready(function () {
    //人性化提示
    $('#name').keyup(function () {
        rightMsg(this, 100);
    });
    $('#container').keyup(function () {
        rightMsg(this, 300);
    });

    //半自动化操作
    //商品分类
    $('#autoA').click(function () {
        $('#groupType').find("option[value='1']").attr("selected", true)
    });
    //配件分类
    $('#autoB').click(function () {
        $('#groupType').find("option[value='2']").attr("selected", true)
    });
    //一级分类
    $('#autoC').click(function () {
        $('#floor').find("option[value='1']").attr("selected", true)
    });
    //二级分类
    $('#autoD').click(function () {
        $('#floor').find("option[value='2']").attr("selected", true)
    });
    //三级分类
    $('#autoE').click(function () {
        $('#floor').find("option[value='3']").attr("selected", true)
    });
});

//右边人性化提示，obj:当前对象，n:最多多少字符
function rightMsg(obj, n) {
    var str = $.trim(obj.value);
    if (str.length > n) {
        str = str.substring(0, n);
        $(obj).val(str);
    }
    $(obj).parent().parent().find('.lot-blue').text('剩余字符数：' + (n - str.length));
}

function ajaxToProType(isEdit) {
    var name = $('#name').val();
    var gtype = $('#groupType').val();
    var floor = $('#floor');
    var pid = $('#pid').val();
    var sortnum = $('#sort').val();
    var displayPic = $('#displayPic').attr('src');
    var container = $('#container').val();
    var updateTime = new Date().getTime();

    //是否是编辑页面
    if (isEdit) {
        var dId = $('#tId').val();
        var dataStatus = $('#dataStatus').val(); //数据状态
        $.post('/PawRoot/ProType/Edit', { TId: tId, TName: name, TContent: container, TPid: pid, TSort: sortnum, TFloor: floor, TUpdateTime: updateTime, TGroupType: gtype, TDisplayPic: displayPic, TDataStatus: dataStatus }, function (data) {
            if (data.Status) {
                showMsg(data.Msg, 500);
                setTimeout(function () {
                    window.location.href = '/PawRoot/ProType/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    } else {
        $.post('/PawRoot/ProType/Add', { TName: name, TContent: container, TPid: pid, TSort: sortnum, TFloor: floor, TUpdateTime: updateTime, TGroupType: gtype, TDisplayPic: displayPic, TDataStatus: dataStatus }, function (data) {
            if (data.Status) {
                showMsg(data.Msg);
                setTimeout(function () {
                    window.location.href = '/PawRoot/ProType/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    }
}