jQuery.ajaxFramework = new ajaxFramework();

//Ajax处理框架对象
function ajaxFramework() {

    //当前Ajax处理框架对象
    var obj = this;

    /*  ajax方法-post
    url:请求的url地址 
    params:请求的参数  示例：{ name : "abc", password : "123"}
    callback:请求成功的回调函数  示例：function(data){ };       
    */
    this.post = function (url, params, callback) {

        obj.ajax(url, params, callback, "POST", "json");
    };


    /*  ajax方法-get
    url:请求的url地址
    params:请求的参数  示例：{ name : "abc", password : "123"}
    callback:请求成功的回调函数  示例：function(data){ };               
    */
    this.get = function (url, params, callback) {

        obj.ajax(url, params, callback, "GET", "json");
    };

    /*  ajax方法
    url:请求的url地址
    params:请求的参数  示例：{ name : "abc", password : "123"}
    callback:请求成功的回调函数  示例：function(data){ };    
    type:请求类型（POST/GET）
    dataType:数据响应类型(xml,json,text)
    */
    this.ajax = function (url, params, callback, type, dataType) {

        $.ajax({
            url: url,
            type: type,
            data: params,
            dataType: dataType,
            success: function (data) {

                if (data.IsSuccess) {
                    callback(data);
                }
                else {
                    if (data.ErrorCode == "100") {
                        $.messager.alert("错误", "操作失败，错误消息：" + data.ErrorMessage, "error", function () {
                            window.top.location.href = '/Admin/Login';
                        });
                    }
                    else {
                        $.messager.alert("错误", "操作失败，错误消息：" + data.ErrorMessage, "error");
                    }
                }
            },
            error: function () {

                $.messager.alert("错误", "HTTP请求发生异常，请联系管理员", "error");
            }
        });
    };
    /*表单提交成功之后的提示信息*/
    this.OnSuccess = function (data) {
        var d = data.IsSuccess;
        $.messager.alert("信息", data.Data, "info");
    };
    /*平台上传图片封装方法,调用示例： $.ajaxFramework.Upload("#uploadImg", "#img_Photo", "#ImgUrl");
    upCtr:上传控制div,示例(#upImg)
    displayCtr:展示图片img,示例(#displayCtr,.displayCtr)
    valueCtr：上传图片的隐藏值，示例(#valueCtr,.valueCtr)
    */
    this.Upload = function (upCtr, displayCtr, valueCtr) {
        $(upCtr).uploadify({
            uploader: "/UpLoad/UploadFile?FolderPath=/upf",
            swf: "/Scripts/uploadify/uploadify.swf",    // 上传使用的 Flash
            width: 69,                          // 按钮的宽度
            height: 29,                         // 按钮的高度
            buttonText: "浏 览",                 // 按钮上的文字
            simUploadLimit: 5,
            buttonCursor: 'hand',                // 按钮的鼠标图标
            fileObjName: 'Filedata',            // 上传参数名称
            // 两个配套使用
            fileTypeExts: "*.bmp;*.gif;*.jpg;*.png",             // 扩展名
            fileTypeDesc: "请选择 bmp gif jpg png 文件",     // 文件说明
            fileSizeLimit: "20MB",
            auto: true,                // 选择之后，自动开始上传
            multi: false,               // 是否支持同时上传多个文件
            queueSizeLimit: 1,         // 允许多文件上传的时候，同时上传文件的个数
            onSelectError: function (file, errorCode, errorMsg) {
                if (errorCode == -110) {
                    $.messager.alert("警告", "文件 \"" + file.name + "\" 超过（" + $(upCtr).uploadify("settings", 'fileSizeLimit') + "）大小限制，请重新上传!");
                    return false;
                }
                else if (errorCode == -130) {
                    $.messager.alert("警告", "只允许上传（" + $(upCtr).uploadify("settings", 'fileTypeExts') + "）类型的文件!");
                    return false;
                }
            },
            onUploadSuccess: function (file, data, response) {
                if (response) {
                    $(displayCtr).attr("style", "display:block;");
                    $(displayCtr).attr("src", data);
                    $(valueCtr).val(data); //隐藏域赋值
                }
            }
        });
    };
};