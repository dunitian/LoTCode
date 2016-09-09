function ajaxToDisPhoto(isEdit) {
    //标题
    var title = $.trim($('#title').val());
    if (title.length < 1) {
        title = "笔记默认展图";
    }
    //文章展览小图
    var displayPic = $('#displayPic').attr('src');
    if (!displayPic) {
        showMsg('请上传笔记展图');
        return false;
    }
    //是否是编辑页面
    if (isEdit) {
        var dId = $('#dId').val();
        var dataStatus = $('#dataStatus').val(); //数据状态
        $.post('/PawRoot/DisPhoto/Edit', { DId: dId, DTitle: title, DPicUrl: displayPic, DataStatus: dataStatus }, function (data) {
            if (data.Status) {
                showMsg(data.Msg, 500);
                setTimeout(function () {
                    window.location.href = '/PawRoot/DisPhoto/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    } else {
        $.post('/PawRoot/DisPhoto/Add', { DTitle: title, DPicUrl: displayPic }, function (data) {
            if (data.Status) {
                showMsg(data.Msg);
                setTimeout(function () {
                    window.location.href = '/PawRoot/DisPhoto/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    }
}