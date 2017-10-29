
function StripSpaces(newstr)
{
	while (newstr.substring(0,1) == " ")
		newstr = newstr.substring(1);
	
	while (newstr.substring(newstr.length-1,newstr.length) == " ")
		newstr = newstr.substring(0,newstr.length-1);

	return newstr;
}

function doEdit(editCatName, editId) {
	var catNameMaxLen = parseInt(document.getElementById("catNameMaxLen").value);
	var catName = editCatName;
	var catText = document.getElementById("catText").value;
	
	do {
		catName = prompt("請輸入" + catText + " (最多 " + catNameMaxLen + " 字)", catName);
		if (catName == null || StripSpaces(catName) == "") {
			return false;
		}
	} while (parseInt(StripSpaces(catName).length) > catNameMaxLen)

	document.getElementById("actionFlag").value = "Edit";
	document.getElementById("catName").value = StripSpaces(catName);
	document.getElementById("editId").value = editId;
	
	//eval("aspnetForm.master_ContentPlaceHolder1_btnAdd").disabled = true;
	__doPostBack('master$ContentPlaceHolder1$btnAdd', '');
	return true;
}

function confirmPub() {
	return (confirm("確定要正式發布此項目嗎? 一旦發布將無法再取消發布及刪除，要繼續?"));
}

function copyPath(objId) {	
	var o = eval(objId).createTextRange();
	o.execCommand("Copy");
}


function delMod() {
	var e = document.getElementsByTagName("input");

	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp("(.*)chkSelect$"))) {
			if (e[x].checked) {
				return confirm("刪除模組可能導致選單關聯出現問題，確定要繼續?");
			}
		}			
	}
	
	alert("沒有選擇項目!");
	return false;	
}


function selectAll(o) {
	var e = document.getElementsByTagName("input");

	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp("(.*)chkSelect$"))) {
			if (!e[x].disabled) {
				e[x].checked = o.checked;
			}
		}			
	}
}

function selectOne(o) {
	var e = document.getElementsByTagName("input");
	var checked = true;
	
	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp("(.*)chkSelect$")) && !e[x].checked) {
			checked = false;
			break;
		}			
	}
	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp("(.*)chkAll$"))) {
			e[x].checked = checked;
			break;
		}
	}
}

function getNextSiblingElementNode(oNode) {
	var oSiblingEle = oNode.nextSibling;
	while (oSiblingEle && oSiblingEle.nodeType != 1) {
		oSiblingEle = oSiblingEle.nextSibling;
	}
	return oSiblingEle;
}

function changeSort(src) {
	var o = getNextSiblingElementNode(src);
	__doPostBack(o.name, '');
}


function delItem(src) {
	var e = document.getElementsByTagName("input");

	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp("(.*)chkSelect$"))) {
			if (e[x].checked) {
				if (confirm("確定刪除選擇的項目?")) {
					document.getElementById(src.id).disabled = true;
					__doPostBack(src.name, '');
					return true;
				} else {
					return false;
				}
			}
		}
	}
	
	alert("沒有選擇項目!");
	return false;	
}

function doAdd() {
	var catNameMaxLen = parseInt(document.getElementById("catNameMaxLen").value);
	var catName = "";
	var catText = document.getElementById("catText").value;
	
	do {
		catName = prompt("請輸入" + catText + " (最多 " + catNameMaxLen + " 字)", catName);
		if (catName == null || StripSpaces(catName) == "") {
			return false;
		}
	} while (parseInt(StripSpaces(catName).length) > catNameMaxLen)

	document.getElementById("actionFlag").value = "Add";
	document.getElementById("catName").value = StripSpaces(catName);
	
	eval("aspnetForm.master_ContentPlaceHolder1_btnAdd").disabled = true;
	//__doPostBack('master$ContentPlaceHolder1$btnAdd', '');
	return true;
}