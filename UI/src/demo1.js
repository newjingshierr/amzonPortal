"use strict";
exports.__esModule = true;
/**
 * Created by administrator on 2017/8/24.
 */
var $ = require("jquery");
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
var httpContext = new HttpHelper();
var data = { Name: "66", Price: 77, Category: "99" };
var url = "http://192.168.0.210:3636/api/Products/";
$("#post").onclick(function () {
    httpContext.Post(url, data);
});
