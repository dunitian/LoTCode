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
//清空编辑器
$('#autoD').click(function () {
    ue.setContent('');
});
//自动分词
$('#autoE').click(function () {

});