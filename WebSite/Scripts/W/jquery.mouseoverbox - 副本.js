
(function ($) {

    $.fn.mouseOverTip = function (opts) {

        opts = $.extend({
            width: '336px',
            height: '200px',
            outlineBoxId: 'outlineBox',//界限最大的Box
            content: '<div></div>',   //内容,
            boxId: 'product_xiuTipBox'
        }, opts || {});

        var pThis = $(this);//本对象
        var limitBoxObj = $("#" + opts.outlineBoxId);
        var tipBoxId = opts.boxId;
        var htmlContent = opts.content;

        var tipBoxHtml = '<div class="poptip" id="' + tipBoxId + '"  style="left: 422px; top: 162px;width: ' + opts.width + ';height: ' + opts.height + '; position: absolute; padding:10px; display:none">dasdadda</div>';
        var tipBoxObj;
        if ($("#" + tipBoxId).length == 0) {
            tipBoxObj = $(tipBoxHtml).appendTo($("body"));
        }
        else {
            tipBoxObj = $("#" + tipBoxId);
        }

        var ihtml = '';
        ihtml += '<span class="t_bg"></span>';
        ihtml += '<span class="b_bg"></span>';
        ihtml += '<span class="arrow" id="tipArr"></span>';
        ihtml += '<div  style="overflow: hidden;width: ' + opts.width + ';height: ' + opts.height + ';">';
        ihtml += htmlContent;
        ihtml += '</div>';
        ihtml += '</div>';
        ihtml += '</div>';



        $(pThis).hover(function () {
            //alert(ihtml)
            $(tipBoxObj).html(ihtml);

            $("body").find(".poptip").hide();

            var pThisOffWidth = 130, pThisOffHeight = 130;

            var left = 0;
            var top = 0;

            if (typeof pThis.attr("tipPos") != "undefined") {
                var c = pThis.attr("tipPos").split("|");
                left = c[0];
                top = c[1];
                arrow= c[2];

            } else {

                var pThisOffSetLeft = pThis.offset().left;
                var pThisOffSetTop = pThis.offset().top, tipBoxOffHeight = tipBoxObj.outerHeight(true), tipBoxOffWidth = tipBoxObj.outerWidth(true);


                var arrow = "bottom";
                if (limitBoxObj) {
                    var margin = 10;
                    var limitBoxOffSetTop = limitBoxObj.offset().top;
                    var limitBoxOffSetLeft = limitBoxObj.offset().left;
                    var limitBoxOffSetRight = limitBoxOffSetLeft + limitBoxObj.width();
                    var limitBoxOffSetBottom = limitBoxOffSetTop + limitBoxObj.height();
                    var limitBoxHeight = limitBoxObj.height();
                    var limitBoxWidth = limitBoxObj.width();
                    left = ((pThisOffWidth / 2) + pThisOffSetLeft) - (tipBoxOffWidth / 2);//计算出提示框离左边的距离,以鼠标划过框为中心点
                    top = pThisOffHeight + pThisOffSetTop - margin;

             
                    if (left > limitBoxOffSetLeft && (left + tipBoxOffWidth) < limitBoxOffSetRight+10) {
                        if ((limitBoxOffSetBottom - pThisOffSetTop - pThisOffHeight) > tipBoxOffHeight) {
                            arrow = "bottom";
                        }
                        else {
                            if ((limitBoxOffSetBottom - pThisOffSetTop) > tipBoxOffHeight) {
                                if ((pThisOffSetLeft - limitBoxOffSetLeft) < tipBoxOffWidth) {

                                    left = pThisOffSetLeft + pThisOffWidth - margin;
                                    top = pThisOffSetTop;
                                    arrow = "right";
                                }
                                else {
                                    left = pThisOffSetLeft - tipBoxOffWidth + margin;
                                    top = pThisOffSetTop;
                                    arrow = "left";
                                }
                            }
                            else {
                                arrow = "top"
                                top = limitBoxOffSetBottom - (pThisOffHeight + tipBoxOffHeight) + margin;
                            }
                        }
                    }
                    else {

                        if (left < limitBoxOffSetLeft && (left + tipBoxOffWidth) < limitBoxOffSetRight) {
                            if ((limitBoxOffSetBottom - pThisOffSetTop - pThisOffHeight) > tipBoxOffHeight) {
                                left = limitBoxOffSetLeft;
                                arrow = "bottom_left";
                            }
                            else {

                                left = pThisOffSetLeft + pThisOffWidth - margin;
                                top = pThisOffSetTop;
                                arrow = "right";

                                if ((top + tipBoxOffHeight) > limitBoxOffSetBottom) {
                                    left = limitBoxOffSetLeft;
                                    top = limitBoxOffSetBottom - tipBoxOffHeight - pThisOffWidth + margin;
                                    arrow = "top_left";
                                }
                            }
                        }
                        else {

                            if ((limitBoxOffSetBottom - pThisOffSetTop - pThisOffHeight) > tipBoxOffHeight) {
                                left = limitBoxOffSetRight - tipBoxOffWidth;
                                arrow = "bottom_right";
                            } else {

                                left = pThisOffSetLeft - tipBoxOffWidth;
                                top = pThisOffSetTop;
                                arrow = "left";
                                if ((top + tipBoxOffHeight) > limitBoxOffSetBottom) {
                                    left = limitBoxOffSetRight - tipBoxOffWidth;
                                    top = limitBoxOffSetBottom - (pThisOffHeight + tipBoxOffHeight) + margin;
                                    arrow = "top_right";
                                }
                            }
                        }
                    }
                }
            }

            $(tipBoxObj).data("showTipObj", pThis);
            $(tipBoxObj).css({ top: top + "px", left: left + "px" }).find("#tipArr").get(0).className = "arrow arrow_" + arrow;
            //$(pThis).attr("tipPos", left + "|" + top + "|" + arrow);

            $(tipBoxObj).show()
        }, function () {
            $(tipBoxObj).hide();
        });

        tipBoxObj.hover(function () {
            // $(this).show();
        }, function () {
            $(this).hide();
        });

        $(tipBoxObj).mouseenter(function () {
            $(this).show();
        });

        $(tipBoxObj).mouseleave(function () {
            $(this).hide();
        });
    }

})(jQuery);