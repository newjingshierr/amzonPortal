"use strict";
exports.__esModule = true;
var $ = require("jquery");
/**
 * Created by administrator on 2017/8/24.
 */
var HttpHelper = (function () {
    function HttpHelper() {
    }
    HttpHelper.prototype.Post = function (url, data) {
        $.ajax({
            type: "Post",
            data: data,
            url: url,
            contentType: "application/json"
        });
    };
    HttpHelper.prototype.Get = function (url, data) {
    };
    HttpHelper.prototype.Put = function () {
    };
    HttpHelper.prototype.Delete = function () {
    };
    return HttpHelper;
}());
exports.HttpHelper = HttpHelper;
