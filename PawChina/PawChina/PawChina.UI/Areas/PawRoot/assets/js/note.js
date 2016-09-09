//实例化编辑器
var ue = UE.getEditor('container');

$(document).ready(function () {
    //人性化提示
    $('#title').keyup(function () {
        RightMsg(this, 100);
    });
    $('#author').keyup(function () {
        RightMsg(this, 50);
    });

    //半自动化操作
    //默认作者
    $('#autoA').click(function () {
        $('#author').val('PawChina');
    });
    //随机浏览
    $('#autoB').click(function () {
        $('#hitCount').val(Math.floor(Math.random() * 1000));
    });
    //重置展图
    $('#autoC').click(function () {
        $('#displayPic').attr('src', '');
    });
    //autoD有差异需要自定义
    //自动分词
    $('#autoE').click(function () {
        if (ue.hasContents()) {
            $.post('/PawRoot/PartialView/JieBaSplit', { content: ue.getContentTxt() }, function (data) {
                if (data.Status) {
                    $('#seoKeywords').val(data.Msg);
                } else {
                    showMsg(data.Msg);
                }
            });
        } else {
            showMsg('文章内容不能为空！');
        }
    });
    //返回Index
    $('#form-gohome').click(function () {
        window.location.href = '/PawRoot/Note/Index';
    });
});

//右边人性化提示，obj:当前对象，n:最多多少字符
function RightMsg(obj, n) {
    var str = $.trim(obj.value);
    if (str.length > n) {
        str = str.substring(0, n);
        $(obj).val(str);
    }
    $(obj).parent().parent().find('.lot-blue').text('剩余字符数：' + (n - str.length));
}

//add and edit post to note
function AjaxToNote(isEdit) {
    //标题
    var title = $.trim($('#title').val());
    if (title.length < 1) {
        showMsg('文章标题不能为空！');
        return false;
    }
    //作者
    var author = $.trim($('#author').val());
    if (author.length < 1) {
        showMsg('文章作者不能为空！');
        return false;
    }
    //浏览量
    var hitCount = $('#hitCount').val();
    //是否推送到主页
    var push = $('#push').is(':checked');
    //文章展览小图
    var displayPic = $('#displayPic').attr('src');
    //编辑器内容
    var ueHtml = '';
    var ueText = '';
    if (ue.hasContents()) {
        ueHtml = escape(ue.getContent());
        ueText = ue.getContentTxt();
        ue.execCommand("clearlocaldata");//清空草稿箱
    } else {
        showMsg('文章内容不能为空！');
        return false;
    }
    //SEO关键词
    var seoKeywords = $.trim($('#seoKeywords').val());
    if (seoKeywords.length < 1) {
        showMsg('SEO关键词不能为空！');
        return false;
    }
    //SEO详细描述
    var seodescription = $.trim($('#seodescription').val());
    if (seodescription.length < 1) {
        showMsg('SEO描述不能为空！');
        return false;
    }
    //更新时间
    var updateTime = new Date().getTime();
    //是否是编辑页面
    if (isEdit) {
        var nId = $('#nId').val();
        var seoId = $('#seoId').val();
        var dataStatus = $('#dataStatus').val(); //数据状态
        var createTime = $('#createTime').val(); //创建时间

        $.post('/PawRoot/Note/Edit', { NId: nId, NTitle: title, NContent: ueHtml, NContentText: ueText, NAuthor: author, NHitCount: hitCount, NDisplayPic: displayPic, NPush: push, NCreateTime: createTime, NUpdateTime: updateTime, NDataStatus: dataStatus, NSeoId: seoId, SeoInfo: { Id: seoId, SeoKeywords: seoKeywords, Sedescription: seodescription } }, function (data) {
            if (data.Status) {
                showMsg(data.Msg, 500);
                setTimeout(function () {
                    window.location.href = '/PawRoot/Note/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    } else {
        var createTime = new Date().getTime(); //创建时间
        $.post('/PawRoot/Note/Add', { NTitle: title, NContent: ueHtml, NContentText: ueText, NAuthor: author, NHitCount: hitCount, NDisplayPic: displayPic, NPush: push, NCreateTime: createTime, NUpdateTime: updateTime, SeoInfo: { SeoKeywords: seoKeywords, Sedescription: seodescription } }, function (data) {
            if (data.Status) {
                showMsg(data.Msg);
                setTimeout(function () {
                    window.location.href = '/PawRoot/Note/Index';
                }, 1000)
            } else {
                showMsg(data.Msg);
            }
        });
    }
}