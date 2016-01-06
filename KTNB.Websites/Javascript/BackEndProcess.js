/**********************************************************************************************************/
/*
 * Back end processing
 *
 * Copyright (c) 2009 by Dungnt (Mr.)
 * Dual licensed under the MIT (MIT-LICENSE.txt)
 * and GPL (GPL-LICENSE.txt) licenses.
 *
 * $Date: Mar 17 2009 $
 * $Rev: 1000 $
 * Define all action which will be operating with DOCUMENT, PROCESS, ACTIVITY
 */
/**********************************************************************************************************/

var MSG_DO_QUESTION = "Bạn chắc chắn muốn thực hiện ?";
var MSG_ADD_QUESTION = "Bạn chắc chắn muốn thêm mới ?";
var MSG_DEL_QUESTION = "Bạn chắc chắn muốn xóa ?";
var MSG_EIDT_QUESTION = "Bạn chắc chắn muốn cập nhật ?";

var MSG_ADD_OK = "Thêm mới thành công.";
var MSG_ADD_ER = "Có lỗi xảy ra trong quá trình thêm mới. Liên hệ với admin để kiểm tra.";

var MSG_EDIT_OK = "Cập nhật thành công.";
var MSG_EDIT_ER = "Có lỗi xảy ra trong quá trình hiệu chỉnh. Liên hệ với admin để kiểm tra.";

var MSG_DEL_OK = "Xóa thành công.";
var MSG_DEL_ER = "Có lỗi xảy ra trong quá trình xóa. Liên hệ với admin để kiểm tra.";

var MSG_DATA_ESXIT = "Dữ liệu nhập bị trùng lặp. Kiểm tra lại.";




var CREATE_DOCUMENT = 1;
var UPDATE_DOCUMENT = 2;
var UPDATE_PROPERTYVALUE_ON_DOCUMENT = 3;
var CREATE_DOCUMENT_WITH_DOCLINK = 4;
var LOAD_DOCUMENT = 5;
var DELETE_DOCUMENT = 8;
var GET_DOCUMENT_PROPERTY_VALUE = 14;
var REMOVE_DOCUMENTLINK = 16;
var CREATE_PROCESS = 32;
var DELETE_PROCESS = 128;

var VIEWTYPE_ADDNEW = 1;
var VIEWTYPE_SHOW = 3;
var VIEWTYPE_EDIT = 5;
var VIEWTYPE_REPORT = 7;
var VIEWTYPE_ADDNEW_ON_PROCESS = 2;
var VIEWTYPE_SHOW_ON_PROCESS = 4;
var VIEWTYPE_EDIT_ON_PROCESS = 6;

var NHANVIEN_CN_DONVI = "4C5C7F82-4460-40C5-984A-ADE81D47C792";
var LANHDAO_CN_DONVI = "298202FD-0888-451C-85A6-C52BB04A8096";
            
var FormIsvalid;
var post_url = "../../Modules/BackEnd/BackEndProcess.aspx";
var post_data = "";
var strOp="";
var strField="";
var pre_id_formcontrol = "ContentPlaceHolder1_FormContent_";

var g_alertmess_after_operation_scusscess=false;
var g_callback_url = "";
/**********************************************************************************************************/
function savedocumentwithoutconfirm(document, doctype, callbackfunction_onsuccess, callbackfunction_onerror) {    
    if (!FormValidated())
        return false;
    post_url = "savedoc.do";
    post_data = ParsingForm();
    post_data += "&act=" + CREATE_DOCUMENT;
    post_data += "&doctype=" + doctype;
    post_data += "&doc=" + document;
    post_data = post_data.replace("undefined", "");
    while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
        post_data = post_data.replace(pre_id_formcontrol, "");
    /*prepare file attachment for uploading*/
    var fileattach = "";
    if (post_data.lastIndexOf("FileAttachment") != -1)
        fileattach = getfileattacht(post_data);

    StartProcessingForm("");
    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function (msg) {
            FinishProcessingForm();
            var ErrorMessage = new String(msg);
            if (ErrorMessage == "") {
                if (callbackfunction_onsuccess != "") {
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                alert(ErrorMessage);
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f();
                }
                else {
                    alert(ErrorMessage);
                }
            }
        }
    });
}
/**********************************************************************************************************/
/**********************************************************************************************************/
function savedocument(document, doctype, callbackfunction_onsuccess, callbackfunction_onerror, confirmsg) {
    if (!FormValidated())
        return false;
    if ((confirmsg == '')||(confirmsg == null)||(confirmsg == 'undefined'))
        confirmsg = MSG_ADD_QUESTION;
    post_url = "savedoc.do";
    post_data = ParsingForm();

    if (!window.confirm(confirmsg))
        return false;
    
    post_data += "&act=" + CREATE_DOCUMENT;
    post_data += "&doctype=" + doctype;
    post_data += "&doc=" + document;
    
    post_data = post_data.replace("undefined","");
    while(post_data.lastIndexOf(pre_id_formcontrol)!=-1)
        post_data = post_data.replace(pre_id_formcontrol, "");

    /*prepare file attachment for uploading*/
    var fileattach = "";
    if(post_data.lastIndexOf("FileAttachment")!=-1)
        fileattach = getfileattacht(post_data);
    
    StartProcessingForm("");
    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function (msg) {           
            FinishProcessingForm();
            var ErrorMessage = new String(msg);
            if (ErrorMessage == "") {
                if (callbackfunction_onsuccess != "") {                   
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                alert(ErrorMessage);
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f();
                }
                else {
                    alert(ErrorMessage);
                }
            }
        }
    });
}
/**********************************************************************************************************/
function savedocumentwithlink(document, doclink, doctype, callbackfunction_onsuccess, callbackfunction_onerror, confirmsg) {

    if (!FormValidated())
        return false;

    if ((confirmsg != '') && (confirmsg != null) && (confirmsg != 'undefined'))
        if (!window.confirm(confirmsg))
            return false;

    post_url = "savedoc.do";
    post_data = ParsingForm();

    post_data += "&act=" + CREATE_DOCUMENT_WITH_DOCLINK;
    post_data += "&doctype=" + doctype;
    post_data += "&doc=" + document;
    post_data += "&doclink=" + doclink;

    post_data = post_data.replace("undefined", "");
    while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
        post_data = post_data.replace(pre_id_formcontrol, "");

    /*prepare file attachment for uploading*/
    var fileattach = "";
    if (post_data.lastIndexOf("FileAttachment") != -1)
        fileattach = getfileattacht(post_data);
    StartProcessingForm("");
    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function (msg) {
            FinishProcessingForm();
            var ErrorMessage = new String(msg);
            if (ErrorMessage == "") {
                if (callbackfunction_onsuccess != "") {
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                alert(ErrorMessage);
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f();
                }
                else {
                    alert(ErrorMessage);
                }
            }
        }
    });
}
/**********************************************************************************************************/
function savedocumentandprocess(processdefinition, processinstance, document, doctype, callbackfunction_onsuccess, callbackfunction_onerror)
{    
    if (!FormValidated())
        return false;
    
    post_url = "savedoc.do";
    post_data = ParsingForm();

    if (!window.confirm("Bạn chắc chắn khởi tạo document này ?"))
        return false;

    post_data += "&act=" + CREATE_PROCESS;
    post_data += "&doctype=" + doctype;
    post_data += "&doc=" + document;
    post_data += "&wf_def_id=" + processdefinition;
    post_data += "&wf_id=" + processinstance;

    post_data = post_data.replace("undefined", "");
    while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
        post_data = post_data.replace(pre_id_formcontrol, "");

    /*prepare file attachment for uploading*/
    var fileattach = "";
    if (post_data.lastIndexOf("FileAttachment") != -1)
        fileattach = getfileattacht(post_data);
    
    StartProcessingForm("");
    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function(msg) {
            FinishProcessingForm();
            var ErrorMessage = new String(msg);
            if (ErrorMessage == "") {
                if (callbackfunction_onsuccess != "") {
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                alert(ErrorMessage);
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f();
                }
                else {
                    alert(ErrorMessage);
                }
            }
        }
    });
}
/**********************************************************************************************************/
function transitto(activityinstance, fromActivity, toActivityID, processinstance, documentid, transitionname) {
    var url = "../wfs/ConfigNextActivity.aspx?processinstance=" + processinstance + "&activityinstance=" + activityinstance + "&fromactivity=" + fromActivity + "&toactivity=" + toActivityID + "&document=" + documentid;
    var result = openDialog(url, '800px', '600px');
    if ((result == null) || (result == "undefined"))
        return;

    if (result == "")
        window.location.reload(true);
}
/**********************************************************************************************************/     
function loaddocument(condition,documentid,viewid,callbackfunction_onsuccess,callbackfunction_onerror)
{
    post_url = "loaddoc.do";
    StartProcessingForm("");
    
    post_data = GetFormContent();
    post_data += "&act=" + LOAD_DOCUMENT;
    post_data += "&doctype=" + viewid;
    post_data += "&doc=" + documentid;
    post_data += "&condition=" + condition;
    
    post_data = post_data.replace("undefined","");
    while(post_data.lastIndexOf(pre_id_formcontrol)!=-1)
        post_data = post_data.replace(pre_id_formcontrol, "");

    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function(msg) {
            FinishProcessingForm();
            alert("msg:" + msg);
            var ResultMessage = new String(msg);
            if (ResultMessage.lastIndexOf("@FM") != -1) {
                var adoc = ResultMessage.split("@FM");
                var n = adoc.length;
                for (i = 0; i < n; i++) {
                    var fv = new String(adoc[i]);
                    var afv = fv.split("@VM");
                    var f = new String(afv[0]);
                    var v = new String(afv[1]);
                    if (f.length > 0) {
                        bindingform(f, v);
                    }
                }
                if (callbackfunction_onsuccess != "") {
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f(msg);
                }
                else {
                    alert(msg);
                }
            }
        }
    });
}
/**********************************************************************************************************/
function updatedocument(document, callbackfunction_onsuccess, callbackfunction_onerror, confirmsg) {

    if (!FormValidated())
        return false;

    if ((confirmsg == '') || (confirmsg == null) || (confirmsg == 'undefined'))
        confirmsg = MSG_EIDT_QUESTION;

    if (!window.confirm(confirmsg))
        return false;
        
    post_url = "savedoc.do";
    post_data = ParsingForm();
    post_data += "&act=" + UPDATE_DOCUMENT;
    post_data += "&doc=" + document;

    post_data = post_data.replace("undefined", "");
    while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
        post_data = post_data.replace(pre_id_formcontrol, "");
    
    /*prepare file attachment for uploading*/
    var fileattach = "";
    if (post_data.lastIndexOf("FileAttachment") != -1)
        fileattach = getfileattacht(post_data);

    StartProcessingForm("");
    $.ajax({
    type: "POST",
    url: post_url,
    data: post_data,
    success: function(msg){
        FinishProcessingForm();
        var ErrorMessage = new String(msg);
        if(ErrorMessage=="")
        {
            if(callbackfunction_onsuccess!="")
            {
               var f=callbackfunction_onsuccess;
               f();
            }
        }
        else
        {
            if(callbackfunction_onerror!="")
            {
                var f=callbackfunction_onerror;
                f();
            }
            else
            {
                alert(ErrorMessage);
            }
        }
    }
    });
}
/**********************************************************************************************************/
function deleteprocess(processinstance, callbackfunction_onsuccess, callbackfunction_onerror) {
    if (!window.confirm("Bạn chắc chắn xóa luồng thực hiện này ?"))
        return false;
    post_url = "savedoc.do";
    StartProcessingForm("");
    post_data = "act=" + DELETE_PROCESS;
    post_data += "&wf_id=" + processinstance;

    $.ajax({
        type: "POST",
        url: post_url,
        data: post_data,
        success: function(msg) {
            FinishProcessingForm();
            var ErrorMessage = new String(msg);
            if (ErrorMessage=="") {
                alert("Luồng thực hiện đã được xóa. Thao tác thành công.");
                if (callbackfunction_onsuccess != "") {
                    var f = callbackfunction_onsuccess;
                    f();
                }
            }
            else {
                if (callbackfunction_onerror != "") {
                    var f = callbackfunction_onerror;
                    f();
                }
                else {
                    alert(ErrorMessage);
                }
            }
        }
    }); 
}
/**********************************************************************************************************/
function deletedocument(documentid, callbackfunction_onsuccess, callbackfunction_onerror, confirmsg) {

    if ((confirmsg == '') || (confirmsg == null) || (confirmsg == 'undefined'))
        confirmsg = MSG_DEL_QUESTION;

    if (!window.confirm(confirmsg))
        return false;

    post_url = "savedoc.do";
    StartProcessingForm("");
    post_data = "act=" + DELETE_DOCUMENT;
    post_data += "&doc=" + documentid;
    
    $.ajax({
    type: "POST",
    url: post_url,
    data: post_data,
    success: function(msg){
        FinishProcessingForm();
        var ErrorMessage = new String(msg);
        if(ErrorMessage=="")
        {
            if(callbackfunction_onsuccess!="")
            {
               var f=callbackfunction_onsuccess;
               f();
            }
        }
        else
        {
            if(callbackfunction_onerror!="")
            {
                var f=callbackfunction_onerror;
                f();
            }
            else
            {
                alert(ErrorMessage);
            }
        }
    }
    });    
}
/**********************************************************************************************************/            
function DeleteRowSelected(indexFrame)
{
    var row_selected_color="#ececec";
    var parentWnd = window.opener.parent;
    var fra=parentWnd.frames[indexFrame];
    var tbl = eval(fra.window.document.getElementById(pre_id_formcontrol + 'dataCtrl'));
    var rowCNT = tbl.rows.length;
    for(i=1;i<rowCNT;i++)
    {
        var stl = eval(tbl.rows[i].style);
        var color = stl.backgroundColor;
        if(typeof(color)!="undefined")
        {
            if(color==row_selected_color)
                stl.display='none';            
        }    
    }
}
/**********************************************************************************************************/
function RemoveDocument_BackEnd(DocumentID)
{
    alert('Remove document '+DocumentID);
}
/**********************************************************************************************************/
function GetFormContent()
{
    var post_data="";
    var theForm = document.forms['frmWsp'];
    if (!theForm) 
        theForm = document.frmWsp;
    var name_value;
    for(i=0;i<theForm.length;i++)
    {
        name_value = RequestControl(theForm,i);
        if(name_value.indexOf(undefined)==-1)
            post_data += name_value;
    }
    if(typeof(post_data)==undefined) post_data="";
    return post_data; 
}
/**********************************************************************************************************/
function bindingform(f,v)
{   
    var ctl="";
    
    //textbox
    $(":text").each(function(){
        ctl = new String($(this).attr("id"));
        if(ctl.toUpperCase().lastIndexOf(f.toUpperCase())!=-1)
            $(this).attr("value",v);
    });
    //dropdownbox
    $("select").each(function(){
        ctl = new String($(this).attr("id"));
        if(ctl.toUpperCase().lastIndexOf(f.toUpperCase())!=-1)
            $(this).attr("value",v);
    });
    
    //listbox
    
    //checkbox
    $(":checkbox").each(function(){
        ctl = new String($(this).attr("id"));
        if(ctl.toUpperCase().lastIndexOf(f.toUpperCase())!=-1)
        {
            if((v.length>0)&&(v=="on"))
            {
                $(this).attr("checked",true);
            }
            else
            {
                $(this).attr("checked",false);
            }
        }
    });
    //radiobox
    $(":radio").each(function(){
        ctl = new String($(this).attr("id"));
        if(ctl.toUpperCase().lastIndexOf(f.toUpperCase())!=-1)
        {
            var value = $(this).attr("value");
            if(value==v)
                $(this).attr("checked",true);
        }
    });
}
/**********************************************************************************************************/
function ParsingForm() {
    post_data = new String("");
    var name_value = "";
    
    var theForm = document.forms['frmWsp'];
    if (!theForm) 
        theForm = document.frmWsp;
    for(i=0;i<theForm.length;i++)
    {
        name_value = RequestData(theForm, i);
        if ((typeof (name_value) != "undefined") && (name_value != "") && (name_value != null)) {
            if ((name_value.lastIndexOf("ID9") != -1) || (name_value.lastIndexOf("ID8") != -1) || (name_value.lastIndexOf("ID6") != -1) || (name_value.lastIndexOf("ID3") != -1) || (name_value.toLowerCase().lastIndexOf("docname") != -1) || (name_value.toLowerCase().lastIndexOf("docstatus") != -1) || (name_value.toLowerCase().lastIndexOf("docdescription") != -1))
                if (post_data.lastIndexOf(name_value) == -1) {
                    post_data += name_value;
                }
        }
    }
    
    if ((typeof (post_data) == "undefined")||  (post_data=="") || (post_data==null))
        post_data = "";
    while (post_data.lastIndexOf("+") != -1)
        post_data = post_data.replace("+", encodeURIComponent('+'));
    return post_data;    
}
/**********************************************************************************************************/
function FormValidated() {
    var theForm = document.forms['frmWsp'];
    if (!theForm) 
        theForm = document.frmWsp;
    for(i=0;i<theForm.length;i++)
    {
        key = theForm.elements[i].id;

        if (key == "")
            continue;

        var css_class = new String($("#" + key).attr("class"));
        
        if (css_class == "") continue;
        
        var style = new String($("#" + key).attr("style"));
        if(
            (css_class.lastIndexOf("TextBoxRequired")!=-1)
            ||(css_class.lastIndexOf("TextBoxDateRequired")!=-1)
            ||(css_class.lastIndexOf("TextBoxNumericRequired")!=-1)
            || (css_class.lastIndexOf("DropDownListRequired") != -1)
          )
        {
            var value = $("#" + key).val();
            
            if ((typeof (value) == undefined) || (value == "")) {
                var requireField = $("#" + key).parent().parent().find('label').html().split('<span')[0];
                var m = 'Bạn vui lòng nhập thông tin "' + requireField + '"';
                alert(m);
                $("#" + key).focus();
                return false;
            }
            if (css_class.lastIndexOf("TextBoxNumericRequired") != -1) {
                if (isNaN(value)) {
                    alert("Please enter numeric value.");
                    $("#" + key).focus();
                    return false;
                }
            }
        }
    }
    return true;
}
/**********************************************************************************************************/
function RequestControl(theForm,i)
{
    var key;
    var value;
    var cls;
    var ret;
    key=theForm.elements[i].id;
    cls = $("#"+key).attr("class");

    if(key.toUpperCase().indexOf("VIEWSTATE")>0) return "";
    if(key.toUpperCase().indexOf("EVENTTARGET")>0) return "";
    if(key.toUpperCase().indexOf("EVENTARGUMENT")>0) return "";
    
    value="";
    if(typeof(key)!=undefined)
        ret = key+"="+value+"&";
    return ret;
}
/**********************************************************************************************************/
function RequestData(theForm, i) {
    var key;
    var type;
    var value;
    var cls;
    var checked;

    key = new String(theForm.elements[i].id);
    type = theForm.elements[i].type;
    try {
        if (key.toUpperCase().lastIndexOf("VIEWSTATE") > 0) return "";
        if (key.toUpperCase().lastIndexOf("EVENTTARGET") > 0) return "";
        if (key.toUpperCase().lastIndexOf("EVENTARGUMENT") > 0) return "";
    }
    catch (ex) {
        return "";
    }

    if (theForm.elements[i].value != null) value = theForm.elements[i].value.trim();

    var key_value = "";

    if (typeof (key) != "undefined") {
        if (type == "radio") {
            try {
                checked = $("#" + key).attr("checked");
                if (checked)
                    key_value = key + "=" + value + "&";
                else
                    key_value = undefined;
            }
            catch (e) {
                key_value = undefined;
            }
        }
        else if (type == "checkbox")       //not supported checkbox group
        {
            checked = $("#" + key).attr("checked");
            if (checked)
                key_value = key + "=" + value + "&";
            else {
                value = "NULL";
                key_value = key + "=" + value + "&";
            }
        }
        else {
            key_value = key + "=" + value + "&";
        }
    }
    return key_value;
}
/**********************************************************************************************************/
function ShowTransError()
{
    alert("Error....");
}
/**********************************************************************************************************/
function StartProcessingForm(message)
{
    //$.blockUI({ message: "<br/><br/><img src='../../images/indicator.gif'><i><font color='blue'>Đang thực hiện...</font></i><br/><br/><br/>" });
    //document.body.style.cursor = "normal";
}
/**********************************************************************************************************/
function FinishProcessingForm()
{
    //$.unblockUI();
    //$("#divImgProcessing").hide();
    //$("#td_buttonspace").show();
}
/**********************************************************************************************************/
function finishProcess(message) {
    $("#divImgProcessing").hide();
    $("#td_buttonspace").show();
    var m = new String(message);
    window.location.href = '../modules/dialog/message.aspx?t=Timeout&m=Timeout...';
}
/**********************************************************************************************************/
function Password_onkeyup() {
    if (event.keyCode == 13) do_login();
}
function login() {
    var u = $("#UserName").val();
    var p = $("#Password").val();

    if ((u == "") || (p == "")) return;

    var url = "SignIn.aspx";
    var query = "act=login";
    query += "&UserName=" + u;
    query += "&Password=" + p;
    $.ajax({
        type: "POST",
        url: url,
        data: query,
        success: function(data) {
            try {
                var inf = data;
                eval(inf);
                var g = new Number(ci.gs);
                if (!isNaN(g)) {
                    if (g >= 1) {
                        window.location.href = 'Default.aspx';
                    }
                    else {
                        alert("Kiểm tra lại tên đăng nhập hoặc mật khẩu.");
                    }
                }
                else {
                    //alert(data);
                    alert("Kiểm tra lại tên đăng nhập hoặc mật khẩu.");
                }
            }
            catch (e) {
                //alert(data);
                alert("Kiểm tra lại tên đăng nhập hoặc mật khẩu.");
            }
        }
    });
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function FormatNumber(obj) {
    var strvalue;
    if (eval(obj))
        strvalue = eval(obj).value;
    else
        strvalue = obj;
    var num;
    num = strvalue.toString().replace(/\$|\,/g, '');

    if (isNaN(num))
        num = "";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    num = Math.floor(num / 100).toString();
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
 	        num.substring(num.length - (4 * i + 3));
    eval(obj).value = (((sign) ? '' : '-') + num);
}
function replaceNumber(sNumber, oldChar, newChar) {
    var s = "";
    var a = sNumber.split(oldChar);
    for (i = 0; i < a.length; i++)
        s += new String(a[i]);
    return s;
}
function Mo() {
    EnableControl(pre_id_formcontrol + "btnSave")
    EnableControl(pre_id_formcontrol + "drLoaiGiayTo")
    EnableControl(pre_id_formcontrol + "fUpload")
}
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function NewGuid() {
    guid = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
    return guid;
}