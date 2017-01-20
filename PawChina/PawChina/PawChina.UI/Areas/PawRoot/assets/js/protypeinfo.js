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
        setSelected($('#groupType'), 1);
        reloadTypes(1);
    });
    //配件分类
    $('#autoB').click(function () {
        setSelected($('#groupType'), 2);
        reloadTypes(2);
    });
    //下拉列表选择事件
    $('#groupType').change(function () {
        reloadTypes($(this).val());
    });

    //一级分类
    $('#autoC').click(function () {
        setSelected($('#floor'), 1);
    });
    //二级分类
    $('#autoD').click(function () {
        setSelected($('#floor'), 2);
       
    });
    //三级分类
    $('#autoE').click(function () {
        setSelected($('#floor'), 3);
    });
    //下拉列表选择事件
    $('#floor').change(function () {
        setSelected($('#floor'),$(this).val());
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

function setSelected(obj, value) {
    //先清空其他子节点的selected

    obj.find("option[value='" + value + "']").attr("selected", true);
    if (value == 1) {
        $('#ptype').css('display', 'none');
        $('.lot-tagSelect').selectpicker('val', '');
    } else {
        $('#ptype').css('display', '');
    }
}

//重新加载分类数据
function reloadTypes(id) {
    $.post('/PawRoot/ProType/GetProTypes', { id: id }, function (data) {
        if (data.Status) {
            $('#pid').html('');
            //便利数据
            $.each(data.Data, function (index, item) {
                var typeStr = '<optgroup label="';
                if (item.length > 0 && item[0].TFloor == 1) {
                    typeStr += '一级分类">';
                } else if (item.length > 1 && item[0].TFloor == 2) {
                    typeStr += '二级分类">';
                }
                for (var i = 0; i < item.length; i++) {
                    typeStr += '<option value="' + item[i].TId + '">' + item[i].TName + '</option>';
                }
                typeStr += '</optgroup>';
                $('#pid').append(typeStr);
            });
            $('.lot-tagSelect').selectpicker('refresh');
            //$('.lot-tagSelect').selectpicker('show');
        } else {
            showMsg(data.Msg);
        }
    });
}

//添加和修改
function ajaxToProType(isEdit) {
    var name = $('#name').val();
    var gtype = $('#groupType').val();
    var floor = $('#floor').val();
    var pid = $('.lot-tagSelect').selectpicker('val');
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
        $.post('/PawRoot/ProType/Add', { TName: name, TContent: container, TPid: pid, TSort: sortnum, TFloor: floor, TUpdateTime: updateTime, TGroupType: gtype, TDisplayPic: displayPic }, function (data) {
            if (data.Status) {
                showMsg(data.Msg, 1500);
                setTimeout(function () {
                    window.location.href = '/PawRoot/ProType/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    }
}