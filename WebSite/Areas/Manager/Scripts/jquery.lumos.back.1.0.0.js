(function ($) {
    $.lumos = lumos = {
        isNullOrEmpty: function (obj) {
            if (obj == null) {
                return true;
            }
            else if (obj == "") {
                return true;
            }
            else {
                return false;
            }
        },

        htmlEncode: function (str) {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/&/g, "&amp;");
            s = s.replace(/</g, "&lt;");
            s = s.replace(/>/g, "&gt;");
            s = s.replace(/ /g, "&nbsp;");
            s = s.replace(/\'/g, "&#39;");
            s = s.replace(/\"/g, "&quot;");
            s = s.replace(/\n/g, "<br>");
            return s;
        },

        htmlDecode: function html_decode(str) {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/&amp;/g, "&");
            s = s.replace(/&lt;/g, "<");
            s = s.replace(/&gt;/g, ">");
            s = s.replace(/&nbsp;/g, " ");
            s = s.replace(/&#39;/g, "\'");
            s = s.replace(/&quot;/g, "\"");
            s = s.replace(/<br>/g, "\n");
            return s;
        },
        newGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        },
        parseFormArray: function (obj) {

            var type = Object.prototype.toString.call(obj);
            var array = new Array();

            if (type == "[object Array]") {
                $.each(obj, function (i, n) {
                    $.each(n, function (x, j) {
                        array.push({ name: x, value: j })
                    })
                });

            }
            else if (type == "[object Object]") {
                $.each(obj, function (x, j) {
                    array.push({ name: x, value: j })
                })
            }

            return array;

        },

        messageBox: function (opts) {
            opts = $.extend({
                type: 'tip',
                title: 'title',
                content: 'content',
                isPopup: true,
                showBoxId: '',
                padding: "0px 0px",
                errorStackTrace: ""
            }, opts || {});


            var _type = opts.type.toLowerCase();
            var _title = opts.title;
            var _content = opts.content;
            var _isPopup = opts.isPopup;
            var _showBoxId = opts.showBoxId;
            var _padding = opts.padding;
            var _errorStackTrace = opts.errorStackTrace;

            var errorHtml = '  <div class="messagebox">';
            errorHtml += ' <div class="wrapper">';
            errorHtml += '   <div class="content">';
            errorHtml += '     <dl>';
            errorHtml += '      <dt class="' + _type + '" ></dt>';
            errorHtml += '    <dd >';
            errorHtml += '     <h1>' + _title + '</h1>';
            errorHtml += '      <p>' + lumos.htmlDecode(_content) + '</p>';
            errorHtml += '     </dd>';
            errorHtml += '   </dl>';


            if (_errorStackTrace != "") {
                errorHtml += '<div class=\"errorstacktrace\">';
                errorHtml += _errorStackTrace;
                errorHtml += '</div>';
            }


            errorHtml += '  <div class="clear"></div>';
            errorHtml += ' </div>';
            errorHtml += ' </div>';
            errorHtml += '</div>';

            if (_isPopup) {
                art.dialog({
                    content: errorHtml,
                    cancelVal: 'Close',
                    title: 'Tip',
                    width: "500px",
                    height: "100px",
                    cancel: true,
                    padding: _padding
                });
            }
            else {
                if (_showBoxId != '') {
                    $("#" + _showBoxId).html(errorHtml)
                }
                else {

                    if ($("#gbr_main_content_right").length > 0) {
                        $('#gbr_main_content_right').html(errorHtml)
                    }
                    else if ($("#gbr_main_content").length > 0) {
                        $('#gbr_main_content').html(errorHtml)
                    }
                    else {
                        $('body').html(errorHtml)
                    }

                }
            }
        },

        openDialog: function (url, title, iwidth, iheight) {
            var dialogSymbol = "dialogtitle=" + escape(title);
            if (url.indexOf('?') > -1) {
                url += "&"
            }
            else {
                url += "?"
            }
            url += dialogSymbol;
            art.dialog.open(url, { id: "openDialog", title: title, width: iwidth, height: iheight, lock: true });
            return false;
        },

        cloaseDialog: function () {
            setTimeout(function () { art.dialog.close(); }, 100);
            return false;
        },

        getUrlParamValue: function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值

        },

        postJson: function (opts) {

            opts = $.extend({
                url: '',
                data: null,
                async: true,
                timeout: 0,
                beforeSend: function (XMLHttpRequest) {
                },
                complete: function (XMLHttpRequest, status) {
                    if (status == 'timeout') {
                        art.dialog.tips("Network request timeout, please re open the page");
                    }
                    else if (status == 'error') {
                        art.dialog.tips("Network request failed, please check whether the network has been connected ");
                    }
                },
                success: function () { }
            }, opts || {});

            var _url = opts.url;

            if (_url == '') {
                art.dialog.tips("Request failed, the link is empty");
                return;
            }





            var _data = opts.data;
            var _async = opts.async;
            var _timeout = opts.timeout;
            var _success = opts.success;
            var _beforeSend = opts.beforeSend;
            var _complete = opts.complete;


            //判断__RequestVerificationToken是否存在
            var tokeName = "__RequestVerificationToken";
            var token = $("input[name=" + tokeName + "]");
            var tokenValue = "";
            if ($(token).length > 0) {
                tokenValue = $($(token)[0]).val();
                if (_data != null) {
                    var isExist = false;
                    var type = Object.prototype.toString.call(_data);
                    if (type == "[object Array]") {


                        $.each(_data, function (i, n) {
                            $.each(n, function (x, j) {
                                if (j == tokeName) {
                                    isExist = true;
                                    return;
                                }
                            })
                        });
                        if (!isExist) {
                            _data.push({ name: tokeName, value: tokenValue });
                        }
                    }
                    else if (type == "[object Object]") {
                        $.each(_data, function (x, j) {

                            if (x == tokeName) {
                                isExist = true;
                                return;
                            }
                        })

                        if (!isExist) {
                            _data.__RequestVerificationToken = tokenValue
                        }
                    }
                }
            }


            $.ajax({
                type: "Post",
                dataType: "json",
                async: _async,
                timeout: _timeout,
                data: _data,
                url: _url,
                beforeSend: function (XMLHttpRequest) {
                    _beforeSend(XMLHttpRequest);
                },
                complete: function (XMLHttpRequest, status) {
                    _complete(XMLHttpRequest, status);
                }
            }).done(function (data) {

                if (data.result == "Exception") {
                    var messsage = data.content;
                    $.lumos.messageBox({ type: "exception", title: messsage.Title, content: messsage.Content, isPopup: messsage.IsPopup, errorStackTrace: messsage.ErrorStackTrace })
                }
                else {
                    _success(data);
                }
            });
        },

        ajaxSubmit: function (opts) {

            opts = $.extend({
                formId: "form1",
                url: '',
                success: function () { return false; }
            }, opts || {});

            var _formId = opts.formId;
            var _url = opts.url;
            var _success = opts.success;

            if (_url == '') {
                art.dialog.tips("Request failed, the link is empty");
                return;
            }


            $('#' + _formId).ajaxSubmit({
                type: "post",  //提交方式
                dataType: "json",
                url: _url,
                success: function (data) {

                    if (data.result == "Exception") {
                        var messsage = data.content;
                        $.lumos.messageBox({ type: "exception", title: messsage.Title, content: messsage.Content, errorStackTrace: messsage.errorStackTrace })
                    }
                    else {
                        _success(data);
                    }
                }
            });

        }
    }


    $.fn.loadDataTable = function (opts) {

        opts = $.extend({
            emptyTip: "No Data",
            url: 'test.apsx',
            searchButtonId: "btn_Search",
            searchParams: null,
            rowDataCombie: function () { },
            operate: null,
            containerId: 'form1'
        }, opts || {});

        var _thisTable = $(this); //当前table
        var _url = opts.url;//访问的地址
        var _searchParams = opts.searchParams;//查询条件
        var _emptyTip = opts.emptyTip;
        var _container = $("#" + opts.containerId)
        //alert(Object.prototype.toString.call(opts.searchParams)  )

        var _searchButtonId = opts.searchButtonId;
        if (_searchParams == null) {
            _searchParams = new Array();
        }

        //构造分页
        function getPagination(totalrecord, pagesize, pageindex) {
            var l_pageIndex = parseInt(pageindex);
            var l_totalRecord = parseInt(totalrecord);
            var l_pageSize = parseInt(pagesize);
            var l_pagination = "";
            var l_paginationInfo = "<div class=\"pagination-info\"><span>Total " + l_totalRecord + " Records ，" + l_pageSize + " Records of Page  </span></div>";
            l_pagination += l_paginationInfo;

            var l_pageCount = Math.ceil(l_totalRecord / l_pageSize)//页数



            var l_paginationIndex = "";
            l_paginationIndex += ' <div class="pagination-index">';
            l_paginationIndex += '<ul>';

            if (l_pageSize > 1 && pageindex != 0) {
                l_paginationIndex += '<li page-index="0"><a>«</a></li>';
            }
            else {
                l_paginationIndex += '<li class="disabled" page-index="0"><a>«</a></li>';

            }

            if (pageindex > 0) {
                l_paginationIndex += '<li  page-index="' + (pageindex - 1) + '"><a>‹</a></li>';
            }
            else {
                l_paginationIndex += '<li class="disabled" page-index="0"><a>‹</a></li>';
            }

            var l_spitIndex = l_pageIndex - 2;
            if (l_spitIndex > 4) {
                l_paginationIndex += '<li page-index="0"><a>1</a></li>';
                l_paginationIndex += '<li page-index="' + (l_spitIndex - 2) + '"><a>...</a></li>';
            }
            else {
                for (var i = 0; i < l_spitIndex; i++) {
                    l_paginationIndex += '<li page-index="' + i + '"><a>' + (i + 1) + '</a></li>';
                }


            }

            for (var i = l_pageIndex - 2; i < l_pageIndex; i++) {
                if (i >= l_pageIndex || i < 0) {
                    continue;
                }
                l_paginationIndex += '<li page-index="' + i + '"><a>' + (i + 1) + '</a> </li>';
            }


            l_paginationIndex += ' <li class="active" ><span>' + (l_pageIndex + 1) + '</span></li>';




            for (var i = l_pageIndex + 1; i < l_pageCount; i++) {
                if (i >= l_pageIndex + 3) {
                    break;
                }
                l_paginationIndex += ' <li  page-index="' + i + '"><a>' + (i + 1) + '</a> </li>';
            }

            l_spitIndex = l_pageIndex + 3;


            if (l_pageCount - 4 > l_spitIndex) {
                l_paginationIndex += ' <li page-index="' + (l_spitIndex + 2) + '"><a >...</a> </li>';
                l_paginationIndex += ' <li page-index="' + (l_pageCount - 1) + '"><a>' + l_pageCount + '</a> </li>';
            }
            else {
                for (var i = l_spitIndex; i < l_pageCount; i++) {
                    l_paginationIndex += '   <li page-index="' + (i) + '"><a>' + (i + 1) + '</a> </li>';
                }
            }



        if (l_pageIndex != l_pageCount - 1 && l_pageCount>0) {
                l_paginationIndex += '<li page-index="' + (l_pageIndex + 1) + '"><a>›</a></li>';
            }
            else {

                l_paginationIndex += '<li class="disabled" page-index="0"><a>›</a></li>';
            }



            if (l_pageSize > 1 && (l_pageIndex != l_pageCount - 1) && l_pageCount > 0) {
                l_paginationIndex += '<li  page-index="' + (l_pageIndex + 1) + '"><a>»</a></li>';
            }
            else {
                l_paginationIndex += '<li class="disabled" page-index="0"><a>»</a></li>';
            }





            l_paginationIndex += '</ul>';
            l_paginationIndex += '</div>';

            l_pagination += l_paginationIndex;


            var l_paginationGoto = "<div class=\"pagination-goto\">  <input class=\"input-control input-go\"   type=\"text\"/>  <input class=\"btn btn-go\" type=\"button\"  pagecount=\"" + l_pageCount + "\" value=\"Go\" /> </div>";
            l_pagination += l_paginationGoto;


            return l_pagination;
        }

        //加载数据
        function getList(currentPageIndex, searchparams) {


            $(_thisTable).data('currentPageIndex', currentPageIndex);

            $.each(searchparams, function (i, field) {
                if (field.name == "PageIndex") {
                    field.value = currentPageIndex
                }
                else if (field.name == "PageSize") {
                    field.value = 10;
                }
                else {
                    field.value = $("*[name='" + field.name + "']").val();
                }
            });


            //alert(JSON.stringify(searchparams))
            // alert(currentPageIndex)
            var loading;
            var l_StrRows = ""; //行数据


            $.lumos.postJson({
                type: "post",
                url: _url,
                async: true,
                dataType: 'json',
                data: _searchParams,
                beforeSend: function (XMLHttpRequest) {
                    loading = art.dialog.loading("Loading data... Please wait for a moment. ", 120000);
                },
                complete: function (XMLHttpRequest, status) {
                    loading.close();
                    if (status == 'timeout') {
                        art.dialog.tips("Network request timeout, please re open the page ");
                    }
                    else if (status == 'error') {
                        art.dialog.tips("Network request failed, please check whether the network has been connected");

                        if ($(_thisTable).find("tbody tr").length == 0) {
                            var headLen = $(_thisTable).find("thead tr th").length;
                            $(_thisTable).find("tbody").append("<tr><td colspan=\"" + headLen + "\" class=\"emptytip\">" + _emptyTip + "</td></tr>");
                        }
                    }
                    else if (status == 'parsererror') {
                        art.dialog.tips("Network request error");
                    }
                    else if (status == 'success') {
                        //loading.close();
                    }
                },
                success: function (data) {

                    loading.close();
                    // setTimeout(function () { loading.close() }, 500);
                    var dataContent = data.content;

                    var list = dataContent;
                    var list_Data = null;
                    if (typeof list.Rows != "undefined") {
                        list_Data = dataContent.Rows
                    }



                    $(_thisTable).find("tbody tr").remove(); //删除所有行
                    $.each(list_Data, function (p_index, p_row) {

                        l_StrRows += opts.rowDataCombie(p_index, p_row); //加载行数据

                    });
                    $(_thisTable).find("tbody").append(l_StrRows); //追加所有行

                    $(_thisTable).addTableStyle();//追加样式




                    $(_thisTable).find(".pagination").html("");
                    pagination = getPagination(list.TotalRecord, list.PageSize, currentPageIndex);
                    $(_thisTable).find(".pagination").append(pagination);

                    //处理每行的checkbox
                    var td_ckb = $(_thisTable).find(" thead th ").eq(0).find("input[type=checkbox]")[0];
                    if ($(td_ckb).length > 0) {
                        $(td_ckb).attr("checked", false);
                        var td_ckb_child_name = $(td_ckb).attr("childname")
                        var tr = $(_thisTable).find("tbody tr");
                        for (var i = 0; i < tr.length; i++) {
                            var tr_td_0 = $(tr[i]).find("td").eq("0");
                            $(tr_td_0).html("<input type='checkbox' name='" + td_ckb_child_name + "' />" + $(tr_td_0).text() + "");
                        }

                    }

                    //空数据提示
                    if (list_Data.length == 0) {
                        var headLen = $(_thisTable).find("thead tr th").length;
                        $(_thisTable).find("tbody").append("<tr><td colspan=\"" + headLen + "\" class=\"emptytip\">" + _emptyTip + "</td></tr>");
                    }
                }


            });
        }

        _searchParams.push({ name: "PageSize", value: 10 });
        _searchParams.push({ name: "PageIndex", value: 0 });


        getList(0, _searchParams);


        //处理分页
        $(_thisTable).find(".pagination-index li").live("click", function () {
            var currentPageIndex = $(this).attr("page-index");
            var classname = $(this).attr("class");
            if (classname != "active" && classname != "disabled") {
                getList(currentPageIndex, _searchParams);
            }
        });

        $(_thisTable).find(".pagination-goto .btn-go").live("click", function () {
            var index = $(_thisTable).find(".pagination-goto .input-go").val();
            var pagecount = parseInt($(this).attr("pagecount"));
            var regexp = /^[1-9]\d*$/;
            if (!regexp.test(index)) {
                art.dialog.tips("Please enter a positive integer greater than 0 ");
                return;
            }

            if (index > pagecount) {
                art.dialog.tips("Please re-enter, not more than " + pagecount);
                return;
            }

            index = index - 1;

            getList(index, _searchParams);

        });

        //处理查询按钮
        $("#" + _searchButtonId).live("click", function () {
            _searchParams = opts.searchParams;

            getList(0, _searchParams);
        });

        //处理所在区域的操作
        if (opts.operate != null) {
            $.each(opts.operate, function (i, field) {

                $.each(field, function (btnclassname, f) {

                    switch (btnclassname) {
                        case "delete":
                            $(_container).find("*[operate=" + btnclassname + "]").live("click", function () {

                                var multiple = $(this).attr("multiple");

                                var del_titleIndex = $(_thisTable).find(".tipitem").index();
                                var del_titleValue = $("#list_table").find("thead tr th").eq(del_titleIndex).text().trim();
                                var del_tips = "Confirm to delete the following data 【" + del_titleValue + "】为<br/>";
                                var keys = new Array();//todo
                                if (typeof multiple == "undefined") {
                                    del_tips += $(this).parent().parent().find(".tipitem").text().trim();
                                    var key_text = $(this).parent().parent().attr("key");
                                    key = $.parseJSON(key_text);
                                    keys.push(key);
                                }
                                else {

                                    var tr_Checked = $(_thisTable).find(" tbody tr input[checked=checked]");

                                    if ($(tr_Checked).length <= 0) {
                                        art.dialog.tips("Please select the data you want to delete");
                                        return
                                    }

                                    $(tr_Checked).each(function () {
                                        del_tips += "" + $(this).parent().parent().find(".tipitem").text().trim() + "<br/>";

                                        var key_text = $(this).parent().parent().attr('key')
                                        key = $.parseJSON(key_text);
                                        keys.push(key);
                                    });
                                    // keys = keys[0]
                                }

                                art.dialog.confirm(del_tips, function () {
                                    f(keys)
                                    return true;
                                },
                                function () { keys = ""; })
                            });
                            break;
                        case "select":
                            $(_container).find("*[operate=" + btnclassname + "]").live("click", function () {

                                var multiple = $(this).attr("multiple");

                                var del_titleIndex = $(_thisTable).find(".tipitem").index();
                                var del_titleValue = $("#list_table").find("thead tr th").eq(del_titleIndex).text().trim();
                                var del_tips = "Confirm that you want to select the following data 【" + del_titleValue + "】为<br/>";
                                var keys = new Array();//todo
                                if (typeof multiple == "undefined") {
                                    del_tips += $(this).parent().parent().find(".tipitem").text().trim();
                                    var key_text = $(this).parent().parent().attr("key");
                                    var key = $.parseJSON(key_text);
                                    keys.push(key);
                                }
                                else {

                                    var tr_Checked = $(_thisTable).find(" tbody tr input[checked=checked]");

                                    if ($(tr_Checked).length <= 0) {
                                        art.dialog.tips("Please select data");
                                        return
                                    }
                                    $(tr_Checked).each(function () {
                                        del_tips += "" + $(this).parent().parent().find(".tipitem").text().trim() + "<br/>";
                                        var key_text = $(this).parent().parent().attr('key')
                                        key = $.parseJSON(key_text);
                                        keys.push(key);
                                    });
                                }

                                art.dialog.confirm(del_tips, function () {
                                    f(keys)
                                    return true;
                                },
                                function () { keys = ""; })
                            });
                            break;
                    }

                });

            });
        }

        this.loadData = function () {

            var pageIndex = $(_thisTable).data('currentPageIndex');
            getList(pageIndex, _searchParams);

        }

        return this;

    }


    $.fn.initUploadImage = function (options) {
        var defaults = {
            url: "/Manager/Common/UploadImage",//调用的后台方法
            path: ""//上传到里路径，如：/Fund
        };
        var opts = $.extend({}, defaults, options);

        var _this = $(this);
        var _url = opts.url;
        var _path = opts.path;



        $(this).live('click', function () {
            var thisUpload = $(this);
            var inputName = $(thisUpload).attr("inputname");
            if (typeof inputName == undefined) {
                inputName = "";
            }


            $(".uploadImageForm").remove();

            var from_FileName = inputName + "_file"
            var formTemplate =
                '<form class=\"uploadImageForm\" enctype="multipart/form-data" style=\"display:none\">' +
                '<input type="hidden" name="valueinputname" value="' + inputName + '" />' +
                '<input type="hidden" name="fileinputname" value="' + from_FileName + '" />' +
                '<input type="hidden" name="savepath" value="' + _path + '" />' +
                '<input type="file" name="' + from_FileName + '" /></form>';
            var form = $(formTemplate);

            $(form).data("comefrom", thisUpload)

            $('body').append(form);


            $(form).find("input[type=file]").trigger("click");

        });



        $(".uploadImageForm").find("input[type=file]").live("change", function () {

   
            var form = $(this).parent();
            var comefrom = $(form).data("comefrom");


            var fileName = $(this).val();//文件名

            if (fileName != "") {
                if (!fileName.match(/.jpg|.gif|.png|.bmp/i)) {
                    $(this).val("");
                    art.dialog.tips('You upload the image format (.Jpg|.gif|.png|.bmp) is not correct, please re select');
                    return;
                }
            }

            var file_size = 0;
            var isFileSizeFlag = false;


            if ($.browser.msie) {
                var img = new Image();
                img.src = fileName;
                while (true) {
                    if (img.fileSize > 0) {
                        if (img.fileSize > 10 * 1024) {
                            isFileSizeFlag = true;
                        }
                        break;
                    }
                }
            } else {
                try {
                    file_size = this.files[0].size;
                    var size = file_size / 1024;
                    if (size > 10 * 1024) {
                        isFileSizeFlag = true;
                    }
                }
                catch (e) {
                    isFileSizeFlag = true;
                }
            }

            if (isFileSizeFlag) {
                art.dialog.tips("Please choose the picture not to exceed 10M ");
                if ($(this).val() != "") {
                    $(this).val("");
                    $(imgId).attr("src", "").hide();
                }
                return;
            }


            var p_d = art.dialog({
                title: 'Picture upload',
                content: 'Uploading pictures... Please later! ',
                cancel: false,
                lock: true,
                dblclickClose: false
            });

            $(form).ajaxSubmit({
                type: "post",
                url: _url,
                dataType: "json",
                success: function (data) {
                    p_d.close();
                    if (data.result == "Success") {
                        var imgObject = data.content;
                        
                        if (imgObject.OriginalPath.length > 0) {
                           $(comefrom).find('img').attr("src", imgObject.OriginalPath);
                           $(comefrom).find("input[type=hidden]").val(imgObject.OriginalPath);//上传成功后把新文件名保存到hidden控件中
                        }
                    }
                    else {
                        $("#" + fileInputName).val("");
                        art.dialog.alert(data.message)
                    }
                    return false;
                }
            });

        });




    }

    $.fn.decimalInput = function (num) {

        //获取当前光标在文本框的位置
        function getCurPosition(domObj) {
            var position = 0;
            if (domObj.selectionStart || domObj.selectionStart == '0') {
                position = domObj.selectionStart;
            }
            else if (document.selection) { //for IE
                domObj.focus();
                var currentRange = document.selection.createRange();
                var workRange = currentRange.duplicate();
                domObj.select();
                var allRange = document.selection.createRange();
                while (workRange.compareEndPoints("StartToStart", allRange) > 0) {
                    workRange.moveStart("character", -1);
                    position++;
                }
                currentRange.select();
            }
            return position;
        }
        //获取当前文本框选中的文本
        function getSelectedText(domObj) {
            if (domObj.selectionStart || domObj.selectionStart == '0') {
                return domObj.value.substring(domObj.selectionStart, domObj.selectionEnd);
            }
            else if (document.selection) { //for IE
                domObj.focus();
                var sel = document.selection.createRange();
                return sel.text;
            }
            else return '';
        }

        $(this).css("ime-mode", "disabled");
        this.bind("keypress", function (e) {
            if (e.charCode === 0) return true;  //非字符键 for firefox
            var code = (e.keyCode ? e.keyCode : e.which);  //兼容火狐 IE
            if (code >= 48 && code <= 57) {
                var pos = getCurPosition(this);
                var selText = getSelectedText(this);
                var dotPos = this.value.indexOf(".");
                if (dotPos > 0 && pos > dotPos) {
                    if (pos > dotPos + 2) return false;
                    if (selText.length > 0 || this.value.substr(dotPos + 1).length < num)
                        return true;
                    else
                        return false;
                }
                return true;
            }
            //输入"."
            if (code == 46) {
                var selText = getSelectedText(this);
                if (selText.indexOf(".") > 0) return true; //选中文本包含"."
                else if (/^[0-9]+\.$/.test(this.value + String.fromCharCode(code)))
                    return true;
            }
            return false;
        });
        this.bind("blur", function () {
            if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
                this.value = this.value.substr(0, this.value.length - 1);
            } else if (isNaN(this.value)) {
                this.value = "";
            }
            if (this.value)
                this.value = parseFloat(this.value).toFixed(2);
            $(this).trigger("input");
        });
        this.bind("paste", function () {
            if (typeof window.clipboardData != undefined) {
                var s = clipboardData.getData('text');
                if (!isNaN(s)) {
                    value = parseFloat(s);
                    return true;
                }
            }
            return false;
        });
        this.bind("dragenter", function () {
            return false;
        });
        this.bind("keyup", function () {
        });
        this.bind("propertychange", function (e) {
            if (isNaN(this.value))
                this.value = this.value.replace(/[^0-9\.]/g, "");
        });
        this.bind("input", function (e) {
            if (isNaN(this.value))
                this.value = this.value.replace(/[^0-9\.]/g, "");
        });
    }

    $.fn.addTableStyle = function () {

        var _this = $(this)

        if (typeof $(_this).get(0) != "undefined") {
            var tagName = $(_this).get(0).tagName
            if (tagName == "TABLE") {

                //-表格隔行,滑动,点击 变色
                $(this).find(' tbody tr:even ').addClass('trold');
                $(this).find(' tbody tr ').hover(
                          function () { $(this).addClass('trmouseover'); },
                         function () {
                             $(this).removeClass('trmouseover');
                         });


            }
        }
    }

    $.fn.openDialog = function (url, title, iwidth, iheight) {

        var _this = $(this);
        $(_this).on("click", function () {
            lumos.openDialog(url, title, iwidth, iheight);
            return false;
        })
    }

    $.fn.multiSelect = function () {

        var _this = $(this);
        var _childname = $(_this).attr("childname");
        $(_this).live('click', function () {

            var isCheck = $(this).attr("checked");
            if (typeof isCheck == "undefined") {
                $('input[name=' + _childname + ']').removeAttr("checked");
            }
            else {
                $('input[name=' + _childname + ']').attr("checked", "checked");
            }

        });


        $('input[name=' + _childname + ']').live("click", function () {


            var isCheck = $(this).attr("checked");
            if (typeof isCheck == "undefined") {
                $(this).removeAttr("checked");
            }
            else {
                $(this).attr("checked", "checked");
            }
            var ckbLen = $('input[name=' + _childname + ']').length;
            var ckbCheckedLen = $('input[name=' + _childname + '][checked=checked]').length;
            if (ckbLen == ckbCheckedLen) {
                $(_this).attr("checked", "checked");
            }
            else {
                $(_this).removeAttr("checked");
            }



        });


    }

    $.fn.tab = function (opts) {


        opts = $.extend({
            beforeClick: function (index) { return true; }
        }, opts || {});

        var _beforeClick = opts.beforeClick;

        var _tabs = $(this);

        $(_tabs).each(function () {

            var _tab = $(this);
            var _tabitems = $(_tab).find(".item");
            var _tabcontents = $(_tab).find(".tab-content .subcontent");

            function showitem(index) {
                $(_tabitems).removeClass("selected");
                $(_tabitems).eq(index).addClass("selected");
                $(_tabcontents).hide();
                $(_tabcontents).eq(index).show()
            }

            $(_tabitems).click(function () {

                var index = $(this).index();
                var isflag = _beforeClick(index);
                if (!isflag)
                    return
                var disabled = $(this).hasClass("disabled");
                if (!disabled) {
                    showitem(index);
                }
            });

            $(_tabitems).each(function () {

                var isflag = $(this).hasClass("selected");
                if (isflag) {
                    var index = $(this).index();
                    showitem(index);
                }
            });




        });


    }

})(jQuery);