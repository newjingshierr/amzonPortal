"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
