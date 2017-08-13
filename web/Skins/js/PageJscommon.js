/*!
 * pagecommon v1.0 
 * 实现快速建站，常用js操作
 * 详尽信息请看官网：http://www.zyjnb.com/
 *
 * Copyright 2017-2017, zyj
 *
 * 请尊重原创，保留头部版权
 * 在保留版权的前提下可应用于个人或商业用途
 */
var LimsJs = {
    //表单异步提交（POST）
    /*
    *  domid：需要提交内容的最外层容器ID
    *  url：数据提交地址
    *  successfuc：请求成功后执行函数
    */
    FromSubmit: function (domid, url, successfuc) {
        var data = "p=1";
        $("#" + domid + " input").each(function () {
            data += "&" + $(this).attr("name") + "=" + $(this).val();
        });
        $("#" + domid + " select").each(function () {
            data += "&" + $(this).attr("name") + "=" + $(this).val();
        });
        $("#" + domid + " textarea").each(function () {
            data += "&" + $(this).attr("name") + "=" + $(this).val();
        });
        $.post(url, data, function (data) {  //注意：后台数据返回格式为json格式，例如：{result:true,msg:'保存成功！'}
            if (successfuc) { //若存在回调函数，则执行回调函数
                successfuc(data);
            }
            else { //默认执行函数
                var datajson = eval(data);
                if (datajson.result) {
                    top.window.location.reload();
                } else {
                    alert(datajson.msg);
                }
            }
        });
    },
    /*
   *  使用PageOffice预览文件
   *  dom：当前对象
   */
    PageOfficeFileView: function (dom) {
        var href = $(dom).attr("href");
        if (/msie/.test(navigator.userAgent.toLowerCase())) { //由于存在pageoffice浏览器兼容问题， 只有IE可以直接用普通页面方式打开
            return true;
        }
        else {
            POBrowser.openWindow(href, 'width=1000px;height=820px;');
        }
        return false;
    }
};