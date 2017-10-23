"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var HttpHelper_1 = require("./HttpHelper");
var $ = require("jquery");
var httpContext = new HttpHelper_1.HttpHelper();
var data = { Name: "66", Price: 77, Category: "99" };
var url = "http://192.168.0.210:3636/api/Products/";
$("#post").onclick(function () {
    httpContext.Post(url, data);
});
