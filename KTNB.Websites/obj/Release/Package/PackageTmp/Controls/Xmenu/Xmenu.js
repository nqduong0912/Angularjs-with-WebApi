function PartWrapperToggle(elementName) {
	var HeaderElement = null;
	var BodyElement = null; 
	if(document.getElementsByName) {
		HeaderElement = document.getElementsByName(elementName+"Header");
		BodyElement = document.getElementsByName(elementName+"Body");
		UpImage = document.getElementsByName(elementName+"Up");
		DownImage = document.getElementsByName(elementName+"Down");
		if(BodyElement) {	
			if(BodyElement[0].style.display == "none") {
				BodyElement[0].style.display = "block";
				HeaderElement[0].className = "ListNuggetHeader";
				DownImage[0].style.display = "none";
				UpImage[0].style.display = "block";
			} else {
				BodyElement[0].style.display = "none";
				HeaderElement[0].className = "ListNuggetHeaderClosed";
				UpImage[0].style.display = "none";
				DownImage[0].style.display = "block";
			}
		}	
	}
	window.event.cancelBubble = true;
	return false;

}

//Auto Resize window

function resizeWin(maxX,maxY,speed,delay,win){
	this.obj = "resizeWin" + (resizeWin.count++);
	eval(this.obj + "=this");
	if (!win)     this.win = self;    else this.win = eval(win);
	if (!maxX)    this.maxX = 400;    else this.maxX = maxX;
	if (!maxY)    this.maxY = 300;    else this.maxY = maxY;
	if (!speed)   this.speed = 1/5;   else this.speed = 1/speed;
	if (!delay)   this.delay = 0;    else this.delay = delay;
	this.doResize = (document.all || document.getElementById);
	this.stayCentered = false;
	
	this.initWin = 	function(){
		if (this.doResize){
			this.resizeMe();
			}
		else {
			this.win.resizeTo(this.maxX + 10, this.maxY - 20);
			}
		}

	this.resizeMe = function(){
		this.win.focus();
		this.updateMe();
		}
	
	this.resizeTo = function(x,y){
		this.maxX = x;
		this.maxY = y;
		this.resizeMe();
		}
		
	this.stayCentered = function(){
		this.stayCentered = true;
		}

	this.updateMe = function(){
		this.resizing = true;
		var x = Math.ceil((this.maxX - this.getX()) * this.speed);
		var y = Math.ceil((this.maxY - this.getY()) * this.speed);
		if (x == 0 && this.getX() != this.maxX) {
			if (this.getX() > this.maxX) x = -1;
			else  x = 1;
			}
		if (y == 0 && this.getY() != this.maxY){
			if (this.getY() > this.maxY) y = -1;
			else y = 1;
			}
		if (x == 0 && y == 0) {
			this.resizing = false;
    		}
		else {
			this.win.top.resizeBy(parseInt(x),parseInt(y));
			if (this.stayCentered == true) this.win.moveTo((screen.width - this.getX()) / 2,(screen.height - this.getY()) / 2);
			setTimeout(this.obj + '.updateMe()',this.delay)
			}
		}
		
	this.write =  function(text){
		if (document.all && this.win.document.all["coords"]) this.win.document.all["coords"].innerHTML = text;
		else if (document.getElementById && this.win.document.getElementById("coords")) this.win.document.getElementById("coords").innerHTML = text;
		}
		
	this.getX =  function(){
		if (document.all) return (this.win.top.document.body.clientWidth + 10)
		else if (document.getElementById)
			return this.win.top.outerWidth;
		else return this.win.top.outerWidth - 12;
	}
	
	this.getY = function(){
		if (document.all) return (this.win.top.document.body.clientHeight + 29)
		else if (document.getElementById)
			return this.win.top.outerHeight;
		else return this.win.top.outerHeight - 31; 
	}
	
	this.onResize =  function(){
		if (this.doResize){
			if (!this.resizing) this.resizeMe();
			}
		}

	return this;
}
resizeWin.count = 0;

function ResizeWindow(intWidth, intHeight){
		rW = new resizeWin(intWidth,intHeight);
		rW.stayCentered();
		
		rW.initWin();
		rW.onResize();
}
function clickitem(url, target){
    window.open(url, target,'center=yes,toolbar=no,status=yes,resizable=yes,top=0,left=0,scrollbars=yes');
    return false;
}