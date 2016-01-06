/*************************************************************************************************************************************/
var Qry;
function GetQueryString()
{
    var arrQrStr;
    var qrStr = new String(window.location.search);
    var spQrStr = qrStr.substring(1); 
    var arrQrStr = new Array();
    var arr = spQrStr.split('&');
    for (var i=0;i<arr.length;i++)
    {
        var index = arr[i].indexOf('=');
        var key = arr[i].substring(0,index);
        var val = arr[i].substring(index+1);
        arrQrStr[key] = val;
    } 
    return arrQrStr;
}
Qry = GetQueryString();
/*************************************************************************************************************************************/
function y2k(number) 
{ 
    return (number < 1000)?number+1900:number; 
}
/*************************************************************************************************************************************/
function getWeek(year,month,day) 
{
    var when = new Date(year,month,day);
    var newYear = new Date(year,0,1);
    var offset = 7 + 1 - newYear.getDay();
    if (offset == 8) offset = 1;
    var daynum = ((Date.UTC(y2k(year),when.getMonth(),when.getDate(  ),0,0,0) - Date.UTC(y2k(year),0,1,0,0,0)) /1000/60/60/24) + 1;
    var weeknum = Math.floor((daynum-offset+7)/7);
    if (weeknum == 0) 
    {
        year--;
        var prevNewYear = new Date(year,0,1);
        var prevOffset = 7 + 1 - prevNewYear.getDay();
        if (prevOffset == 2 || prevOffset == 8) 
            weeknum = 53; 
        else 
            weeknum = 52;
    }
    return weeknum;
}
/*************************************************************************************************************************************/
var win_detail;
function opendetail(url,winname)
{
    var H=600;
    var W=800;
    var T=(screen.height) ? (screen.height-H)/2 : 0;
    var L=(screen.width) ? (screen.width-W)/2 : 0;
    
    try
    {
        if(winname.length==0)
            if((!win_detail)||(win_detail.closed))
                win_detail = window.open(url,'win_detail','height='+H+',width='+W+',top='+T+',left='+L+',resizable=yes,status=yes,scrollbars=yes,toolbar=no,center=yes');
            else
                win_detail.focus();
        else
        {
            win_detail = window.open(url,winname,'height='+H+',width='+W+',top='+T+',left='+L+',resizable=yes,status=yes,scrollbars=yes,toolbar=no,center=yes');
            win_detail.focus();
            
        }   
    }
    catch(e)
    {
    }
}
/*************************************************************************************************************************************/
var open_win;
function openwin(url)
{
    if((!open_win)||(open_win.closed))
        open_win = window.open(url,'open_win','height=450px,width=800px,top=0,left=0,resizable=yes,status=yes');
    else
        open_win.focus();
}
/*************************************************************************************************************************************/
var popup_win;
function popup(url)
{
    if(popup_win==null||popup_win.closed)
        popup_win=window.open(url,'newwin','height=450px,width=800px,center=yes,toolbar=no,status=no,resizable=yes');
    else
        popup_win.focus();
}
/*************************************************************************************************************************************/
Date.prototype.getWeek = function() {
    var determinedate = new Date();
    determinedate.setFullYear(this.getFullYear(), this.getMonth(), this.getDate());
    var D = determinedate.getDay();
    if(D == 0) D = 7;
    determinedate.setDate(determinedate.getDate() + (4 - D));
    var YN = determinedate.getFullYear();
    var ZBDoCY = Math.floor((determinedate.getTime() - new Date(YN, 0, 1, -6)) / 86400000);
    var WN = 1 + Math.floor(ZBDoCY / 7);
    return WN;
}
/*************************************************************************************************************************************/
function openDialog(url,iWidth,iHeight) 
{
	if(document.all) {
		var xMax = screen.width;
		var yMax = screen.height;
	}
	else {
		if (document.layers) {
			var xMax = window.outerWidth;
			var yMax = window.outerHeight;
		}
		else {
			var xMax = 640;
			var yMax=480;
		}
	}
	var xOffset = (xMax - iWidth)/2, yOffset = (yMax - iHeight)/2;
	return window.showModalDialog(url,"noname","dialogHeight:" + iHeight + ";dialogwidth:" + iWidth + ";resizable:yes;edge:sunken;help:no;status:no");
}
/*************************************************************************************************************************************/
function GetCurrentDate()
{
    var date=new Date();
    
    var dd = new String(date.getDate());
    if(dd.length==1) dd = "0" + dd;
    
    var mm = new String(date.getMonth()+1);
    if(mm.length==1) mm = "0" + mm;
    
    var yyyy = date.getFullYear();
    
    return dd + "/" + mm + "/" + yyyy;
}
/*************************************************************************************************************************************/
function GetCurrentYear()
{
    var date=new Date();
    var yyyy = date.getFullYear();
    return yyyy;
}
/*************************************************************************************************************************************/
function DisableControl(id)
{
    $("#" + id).attr("disabled",true);
}
/*************************************************************************************************************************************/
function EnableControl(id)
{
    $("#" + id).attr("disabled",false);
}
/*************************************************************************************************************************************/
function HideControl(id)
{
    $("#" + id).hide();
}
/*************************************************************************************************************************************/
function ShowControl(id)
{
    $("#" + id).show();
}
/*************************************************************************************************************************************/
function GetValue(id)
{
    return $("#"+id).attr("value");
}
/*************************************************************************************************************************************/
function GetSvrCtlValue(id)
{
    //return $("#" + pre_id_formcontrol + id).attr("value");
    return $("#" + pre_id_formcontrol + id).val();
    //$('#txt_name').val('bla');
}
/*************************************************************************************************************************************/
function SetValue(id,s)
{
    $("#"+id).attr("value",s);
}
/*************************************************************************************************************************************/
function SetSvrCtlValue(id,s)
{
    $("#" + pre_id_formcontrol + id).attr("value", s);
}
/*************************************************************************************************************************************/
function DisableSvrCtl(id)
{
    $("#" + pre_id_formcontrol + id).attr("disabled", true);
}
/*************************************************************************************************************************************/
function EnableSvrCtl(id)
{
    $("#" + pre_id_formcontrol + id).attr("disabled", false);
}
/*************************************************************************************************************************************/
function HideSvrCtl(id)
{
    $("#" + pre_id_formcontrol + id).hide();
}
/*************************************************************************************************************************************/
function ShowSvrCtl(id)
{
    $("#" + pre_id_formcontrol + id).show();
}
/*************************************************************************************************************************************/
function FocusSvrCtl(id)
{
    $("#" + pre_id_formcontrol + id).focus();
}
/*************************************************************************************************************************************/
//Ex: StringValue = "123456::Tran Van B#78904::Nguyen Van A#...."
//    SeperateField = #
//    SeperateKeyValue = ::  
function bindData2Combo(ControlID, StringValue, SeperateField, SeperateKeyValue, SelectedKey) //Key::Value#...
{
    var arr = StringValue.split(SeperateField);
    for(i=0;i<arr.length;i++)
    {
        var keyvalue = arr[i];
        if(keyvalue.length>0) {
            var kv = keyvalue.split(SeperateKeyValue);
            if (kv.length>0)
            {
                var k = kv[0];
                var v = kv[1];
                if (k == SelectedKey) {
                    $('#' + ControlID).append($("<option selected value='" + k + "'>" + v + "</option>"));
                }
                else
                    $('#' + ControlID).append($("<option value='" + k + "'>" + v + "</option>"));
            }
        }
    }
}
/*************************************************************************************************************************************/