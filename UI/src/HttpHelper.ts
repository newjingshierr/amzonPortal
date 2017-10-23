import * as $ from "jquery";
/**
 * Created by administrator on 2017/8/24.
 */
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
