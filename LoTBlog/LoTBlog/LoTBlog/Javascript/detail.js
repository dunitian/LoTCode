$(function () {
    // 弹窗box
    if ($("#show_box").size() == 0) {
        $("body").append('<div id="show_box"><img src="" /></div>');
    }

    // 点击相册图片
    $('img').on("click", function (event) {
        $imgurl = $(this).attr("src");
        if (!$imgurl) {
            return;
        }
        var $imgbox = $("#show_box");
        $imgbox.find("img").attr("src", $imgurl);
        //$imgbox.show().animate(40);
        easyDialog.open({
            container: 'show_box',
            autoClose: 30000,
            fixed: false
        });
    });

    // 点击弹窗图片关闭
    $(document).on("click", "#show_box", function () {
        $(this).fadeOut(200);
        easyDialog.close();
    });
    $(document).on("click", "#overlay", function () {
        $(this).fadeOut(200);
        easyDialog.close();
    });
});