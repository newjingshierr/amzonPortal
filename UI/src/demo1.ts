/**
 * Created by administrator on 2017/8/24.
 */
import * as $ from "jquery";

export  class HttpHelper {
    Post(url:string,data:any) {
        $.ajax({
            type:"Post",
            data:data,
            url:url,
            contentType:"application/json"
        });
    }

    Get(url:string,data:any) {

    }

    Put() {

    }

    Delete() {

    }
}

var  httpContext = new HttpHelper();
var data = { Name: "66", Price: 77, Category: "99" };
var url ="http://192.168.0.210:3636/api/Products/";
$("#post").onclick(function () {
    httpContext.Post(url,data);
});

