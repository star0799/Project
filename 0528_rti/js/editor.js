
var key_arr = new Array();

url_str = new String(window.location);
url_str = url_str.substring(url_str.indexOf('?')+1, url_str.length)
key_arr = url_str.split("=")

function fileBrowserCallBack(field_name, url, type, win) {
	// This is where you insert your custom filebrowser logic
	alert("Filebrowser callback: field_name: " + field_name + ", url: " + url + ", type: " + type);

	// Insert new URL, this would normaly be done in a popup
	win.document.forms[0].elements[field_name].value = "someurl.htm";
}
function Receive_Txt(){	
	document.getElementById("content").innerHTML = opener.document.getElementById(key_arr[1]).value;
}
function Restore_Txt(){
	tinyMCE.triggerSave();
	opener.document.getElementById(key_arr[1]).value = $('#content').val();
	window.close();
}

function removeEditor(id) {
	var ele = document.getElementById(id);

	if (tinyMCE.getInstanceById(id) != null) {
		tinyMCE.execCommand('mceRemoveControl', false, id);
	}
}

function toggleEditor(id) {
	var ele = document.getElementById(id);

	if (tinyMCE.getInstanceById(id) == null) {
		//ele.previousSibling.style.display = "none";
		tinyMCE.execCommand('mceAddControl', false, id);
	} else {
		//ele.previousSibling.style.display = "";
		tinyMCE.execCommand('mceRemoveControl', false, id);
	}
}


tinyMCE.init({
		mode : typeof(__gSpecificEditor) == 'undefined' ? "none" : "specific_textareas",
		theme : "advanced",
			plugins : "table,advimage,advlink,preview,contextmenu,flash",
	theme_advanced_buttons1_add_before : "separator",
	theme_advanced_buttons1_add : "fontsizeselect",
	theme_advanced_buttons2_add : "separator,insertdate,preview,separator,forecolor,backcolor",
	theme_advanced_buttons2_add_before: "cut,copy,paste,separator,search,replace,separator",
	theme_advanced_buttons3_add_before : "tablecontrols,separator",
	theme_advanced_buttons3_add : "iespell,advhr,separator,flash",
	theme_advanced_toolbar_location : "top",
	theme_advanced_toolbar_align : "left",
	theme_advanced_path_location : "bottom",
	content_css : "../css/mod.css",
	plugin_insertdate_dateFormat : "%Y-%m-%d",
	plugin_insertdate_timeFormat : "%H:%M:%S",
	extended_valid_elements : "a[class|name|href|target|title|onclick],img[class|src|border=0|alt|title|hspace|vspace|width|height|align|onmouseover|onmouseout|name],hr[class|width|size|noshade],font[class|face|size|color],span[class|align|style]",
	language : "zh_tw_utf8",
	flash_wmode : "transparent",
	flash_quality : "high",
	flash_menu : "false",
	width: "100%",
	height: 400
	});
