
var myWin;

function openWin(url) {
	myWin = window.open(url);
	myWin.focus();
}
function openWin(url, winName, feature) {
	myWin = window.open(url, winName, feature);
	myWin.focus();
}
function openCenterWin(url, winName, w, h) {
	myWin = open(url, winName, "left=" + (screen.availWidth - w) / 2 + ",top=" + (screen.availHeight - h) / 3 + ",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no");
	myWin.focus();
}

function openUpload(url) {
	w = 485;
	h = 550;
	myWin = open(url, "upload", "left=" + (screen.availWidth - w) / 2 + ",top=" + (screen.availHeight - h) / 3 + ",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes");
	myWin.focus();
}

function openLoginWin(url) {
	w = 218;
	h = 140;
	myWin = open(url, "login", "left=" + (screen.availWidth - w) / 2 + ",top=" + (screen.availHeight - h) / 3 + ",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no");
	myWin.focus();
}

function openImageUpload(url) {
	w = 450;
	h = 200;
	myWin = open(url, "upload", "left=" + (screen.availWidth - w) / 2 + ",top=" + (screen.availHeight - h) / 3 + ",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=yes");
	myWin.focus();
}
function openFlashUpload(url) {
	w = 450;
	h = 200;
	myWin = open(url, "upload", "left=" + (screen.availWidth - w) / 2 + ",top=" + (screen.availHeight - h) / 3 + ",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=yes");
	myWin.focus();
}

function openEditor(trigger, key) {
	var no = "";
	var myWin;
	var w = 800;
	var h = 570;
	var lx =  ((screen.availWidth - w) * 0.5);
	var ly =  ((screen.availHeight - h) * 0.3);
	
	/*if (trigger.id.lastIndexOf("_") != -1) {
		no = trigger.id.substring(trigger.id.lastIndexOf("_") + 1);
		key += "_" + no;
	}*/
	
	myWin = open("html_editor.aspx?key=" + key, "editor", "left="+lx+",top="+ly+",width="+w+",height="+h+",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=yes");	
	myWin.focus();
}