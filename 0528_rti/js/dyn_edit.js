
var formObj = aspnetForm;
var prefixId = "master_ContentPlaceHolder1_";
var tblList = document.getElementById(prefixId + "tblList");
var tblBack = document.getElementById(prefixId + "tblBack");
var aryHidden = new Array();

function checkMainItem (o, td) {
	if (o.checked) {
		var t = document.getElementById(td);
		for (var x = 0; x < t.children.length; x++) {
			t.children[x].checked = o.checked;
		}
	}
}

function checkSubItems (o, table) {

	var x, y;
	var t = document.getElementById(table);
	var tagName;

	for (x = 0 ; x < t.rows.length ; x++) {
		for (y = 0 ; y < t.rows[x].cells.length ; y++) {
			if (t.rows[x].cells[y].children.length > 0) {
				
				tagName = t.rows[x].cells[y].children[0].tagName.toLowerCase();
				if (tagName == "input") {					
					t.rows[x].cells[y].children[0].checked = o.checked;					
				} else if (tagName == "table") {
					checkSubItems(o, t.rows[x].cells[y].children[0].id);
				}

			}
		}		// for loop-y
	}		// for loop-x
}


function setCtlNameID(o, n)
{
	var x, y, i, oo;

	for (x=0;x<o.cells.length;x++){
		for (y=0;y<o.cells[x].children.length;y++){
			oo = o.cells[x].children[y];
			setChildCtlNameID(oo, n);
			
			for (i=0;i<oo.children.length;i++){
				setChildCtlNameID(oo.children[i], n);
			}
			
		}
	}
}

function setChildCtlNameID(o, n){
	
	var specifyName = false, i;

	if (o.tagName.toUpperCase() == "TABLE") {
		for (i=0;i<o.rows.length;i++){
			setCtlNameID(o.rows[i], n);
		}
		//specifyName = true;
	} else if (o.tagName.toUpperCase() == "INPUT") {								
		//specifyName = (o.type.toLowerCase() != "radio");		// e.g. id = genderF, genderM, name = gender
	} else if (o.tagName.toUpperCase() == "DIV" || o.tagName.toUpperCase() == "SPAN") {
		for (i=0;i<o.children.length;i++){
			setChildCtlNameID(o.children[i], n);
		}
	} else {
		//specifyName = true;
	}				

	if (o.name != null && o.name != "") {
		if (o.id.lastIndexOf("_") == -1) {
			//o.id = (specifyName ? o.name : o.id) + "_" + n;
			o.id = (o.id != null && o.id != "") ? o.id : o.name + "_" + n;
		} else {			
			if (!isNaN(o.id.substring(o.id.lastIndexOf("_") + 1))) {
				o.id = o.id.substring(0, o.id.lastIndexOf("_") + 1) + n;
			} else {
				o.id = o.id + "_" + n;
			}
		}		

		//if (specifyName) {
		if (!(o.tagName.toUpperCase() == "INPUT" && o.type.toLowerCase() == "radio")) {
			o.name = o.id;		// set name directly equals to id (prefixId)
			//alert("name :  " +o.name + ", id  : " + o.id);
		} else {				// input type is radio
			
			if (o.group == undefined) {
				if (o.name.lastIndexOf("_") == -1) {
					o.name = o.name.replace(/\$/g, "_") + "_" + n;
				} else {
					o.name = o.name.substring(0, o.name.lastIndexOf("_") + 1).replace(/\$/g, "_") + n;
				}
			}
			
			if (o.t_value != null) o.value = o.t_value;
			if (o.t_checked != null) o.checked = (o.t_checked == 1);			

			o.outerHTML = "<input type=radio id=" + o.id + " name=" + o.name + " value=" + o.value + 
			  (o.checked == 1 ? " checked" : "") + (o.group != undefined ? " group=1" : "") + ">";
		}
			
		//alert("id = " + o.id + "   , name = " + o.name + "\ninnerHTML = " + o.innerHTML + "\nvalue = " + o.value);

	} else if (o.id != null && o.id != "") {		

		if (o.id.lastIndexOf("_") == -1) {
			o.id = o.id + "_" + n;
		} else {
			// o.id = o.id.substring(0, o.id.lastIndexOf("_") + 1) + n;			
			if (!isNaN(o.id.substring(o.id.lastIndexOf("_") + 1))) {
				o.id = o.id.substring(0, o.id.lastIndexOf("_") + 1) + n;
			} else {
				o.id = o.id + "_" + n;
			}
		}

		// specify names
		if (!(o.tagName.toUpperCase() == "INPUT" && o.type.toLowerCase() == "radio")) {
			o.name = o.id;		// set name directly equals to id
		} else {

			if (o.name.lastIndexOf("_") == -1) {
				o.name = o.name.replace(/\$/g, "_") + "_" + n;
			} else {
				o.name = o.name.substring(0, o.name.lastIndexOf("_") + 1).replace(/\$/g, "_") + n;
			}	
			
			if (o.t_value != null) o.value = o.t_value;
			if (o.t_checked != null) o.checked = (o.t_checked == 1);			

			o.outerHTML = "<input type=radio id=" + o.id + " name=" + o.name + " value=" + o.value + 
			  (o.checked == 1 ? " checked" : "") + (o.group != undefined ? " group=1" : "") + ">";
		}		

		//alert("id = " + o.id + "   , name = " + o.name + "\ninnerHTML = " + o.innerHTML + "\nvalue = " + o.value);
	}
}


function selectAll(o) {
	var e = document.getElementsByTagName("input");
	
	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkSelect_(.+)$"))) {
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
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkSelect_(.+)$")) && !e[x].checked) {
			checked = false;
			break;
		}			
	}
	for (var x = 0; x < e.length; x++) {
		if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkAll$"))) {
			e[x].checked = checked;
			break;
		}
	}
}

function fnRemoveItem(blockNo) {			
	var x, y, o, no, objName, newName;

	tblList.rows[blockNo].removeNode(true);
	
	formObj.itemCount.value = parseInt(formObj.itemCount.value) - 1;	

	// remove hidden fields
	for (x=0 ; x < aryHidden.length ; x++) {
		o = eval("formObj." + aryHidden[x] + "_" + blockNo);		
		if (o != null) o.removeNode(true);
	}

	// renew hidden field names
	for(x = 0 ; x < formObj.elements.length ; x++){
		o = formObj.elements[x];
		if (o.tagName.toUpperCase() == "INPUT") {
			if (o.type.toLowerCase() == "hidden" && o.name.lastIndexOf("_") != -1) {
				// hidden fields				
				objName = o.name.substring(0, o.name.lastIndexOf("_"));
				for (y = 0 ; y < aryHidden.length ; y++) {
					if (objName == aryHidden[y]) {
						no = o.name.substring(o.name.lastIndexOf("_") + 1);	
						if (no > blockNo) {
							// renew name
							newName = o.name.substring(0, o.name.lastIndexOf("_") + 1) + (parseInt(no) - 1);
							o.outerHTML = "<input type=\"hidden\" name=\"" + newName + "\" value=\"" + o.value + "\">";
						}						
					}
				}
			}
		}
	}

	renewTableElementsId();
}

function addItem(){
	formObj.itemCount.value = parseInt(formObj.itemCount.value) + 1;
	tblList.insertRow(tblList.rows.length).swapNode(tblBack.rows[0].cloneNode(true));
	setCtlNameID(tblList.rows[tblList.rows.length - 1], formObj.itemCount.value);
}
function removeItem(){
	
	var x, y, o, noCheck;			
	var aryRM = new Array("itemCount");
	
	noCheck = true;
	for(x=1 ; x <= parseInt(formObj.itemCount.value) ; x++){
		o = eval("formObj." + prefixId + "chkSelect_" + x);				
		if (o.checked) 	{
			noCheck = false;
			break;
		}
	}
	if (noCheck) {
		alert("Please select item!");
		o = eval("formObj." + prefixId + "chkSelect_1");
		if (o != null) o.focus();
		return false;
	}									
	if (!confirm("Confirm to remove?")){
		return false;
	}
	
	
	// ------- remove items -------	
	for(x = 1 ; x <= parseInt(formObj.itemCount.value) ; x++){
		o = eval("formObj." + prefixId + "chkSelect_" + x);
		if (o.checked) {
			fnRemoveItem(x, aryRM);
			x -= 1;
		}
	}
	
	if (eval("formObj." + prefixId + "chkAll") != undefined) {
		eval("formObj." + prefixId + "chkAll").checked = false;
	}
}
function renewTableElementsId(){
	var x;		

	for(x = 1 ; x <= parseInt(formObj.itemCount.value) ; x++){		
		setCtlNameID(tblList.rows[x], x);
	}
}

function sortItem(trigger, cmd) {
	var x, sNo, o, tNo, tmpValue, obj1, obj2, h;
	//var aryChk = new Array("delrec");

	//var chk1 = new Array(ary.length), chk2 = new Array(ary.length);

	sNo = trigger.id.substring(trigger.id.lastIndexOf("_") + 1);
	/*for (x = 0 ; x < aryChk.length ; x++)	{
		o = document.getElementById(aryChk[x] + "_" + sNo);
		if (o != null) chk1[x] = o.checked;
	}*/

	if (cmd == "Down") {
		tNo = parseInt(sNo) + 1;
		if (tNo > parseInt(formObj.itemCount.value)) tNo = 1;		
	} else if (cmd == "Up") {
		tNo = parseInt(sNo) - 1;
		if (tNo < 1) tNo = parseInt(formObj.itemCount.value);
	} else if (cmd == "Bottom") {
		for (x = sNo; x < parseInt(formObj.itemCount.value); x++) {
			trigger = document.getElementById("btnSortDown" + "_" + x);
			sortItem(trigger, 'Down');
		}
		return;
	} else if (cmd == "Top") {
		for (x = sNo; x > 1; x--) {
			trigger = document.getElementById("btnSortUp" + "_" + x);
			sortItem(trigger, 'Up');
		}
		return;
	}
	
	/*for (x = 0 ; x < aryChk.length ; x++)	{
		o = document.getElementById(aryChk[x] + "_" + tNo);
		if (o != null) chk2[x] = o.checked;
	}*/

	// swap item			
	tblList.rows[sNo].swapNode(tblList.rows[tNo]);
	/*for (x = 0 ; x < aryChk.length ; x++)	{
		o = document.getElementById(aryChk[x] + "_" + sNo);
		if (o != null) o.checked = chk1[x];
		o = document.getElementById(aryChk[x] + "_" + tNo);
		if (o != null) o.checked = chk2[x];
	}*/
	
	// swap hidden field values
	for(x = 0 ; x < aryHidden.length ; x++){
		o = eval("formObj." + aryHidden[x] + "_" + sNo);
		if (o == null){
			h = document.createElement("input");
			h.type = "hidden";
			h.name = aryHidden[x] + "_" + sNo;
			h.id = h.name;
			formObj.appendChild(h);
		}
		
		o = eval("formObj." + aryHidden[x] + "_" + tNo);
		if (o == null){
			h = document.createElement("input");
			h.type = "hidden";
			h.name = aryHidden[x] + "_" + tNo;
			h.id = h.name;
			formObj.appendChild(h);
		}
		
		obj1 = eval("formObj." + aryHidden[x] + "_" + sNo);
		obj2 = eval("formObj." + aryHidden[x] + "_" + tNo);		
		tmpValue = obj1.value;
		obj1.value = obj2.value
		obj2.value = tmpValue;
	}
	
	renewTableElementsId(tblList);
}

function removeFile(rmObj, type) {
	var x, no = null;
	var ary1, ary2, o;

	if (!confirm("確定移除此檔案?")) {
		return false;
	}
	
	// hidden fields
	if (type == "site_config_img") {
		ary1 = new Array("imgNameOri1", "imgNameThumb1", "imgNameOri2", "imgNameThumb2", "imgNameOri3", "imgNameThumb3");
		ary2 = new Array("imgHref1", "imgHref1", "imgHref2", "imgHref2", "imgHref3", "imgHref3");
	} else if (type == "img10") {
		ary1 = new Array("imgNameOri10", "imgNameThumb10");
		ary2 = new Array("imgHref10", "imgHref10");
	} else if (type == "img4") {
		ary1 = new Array("imgNameOri", "imgNameThumb", "imgNameOri2", "imgNameThumb2", "imgNameOri3", "imgNameThumb3", "imgNameOri4", "imgNameThumb4");
		ary2 = new Array("imgHref", "imgHref", "imgHref2", "imgHref2", "imgHref3", "imgHref3", "imgHref4", "imgHref4");
	} else if (type == "img3") {
		ary1 = new Array("imgNameOri", "imgNameThumb", "imgNameOri2", "imgNameThumb2", "imgNameOri3", "imgNameThumb3");
		ary2 = new Array("imgHref", "imgHref", "imgHref2", "imgHref2", "imgHref3", "imgHref3");
	} else if (type == "img2") {
		ary1 = new Array("imgNameOri", "imgNameThumb", "imgNameOri2", "imgNameThumb2");
		ary2 = new Array("imgHref", "imgHref", "imgHref2", "imgHref2");
	} else if (type == "dyn_home_img") {
		no = parseInt(rmObj.id.substring(rmObj.id.lastIndexOf("_") + 1));
		ary1 = new Array("imgNameOriL", "imgNameThumbL", "imgNameOriR", "imgNameThumbR");
		ary2 = new Array("imgHrefL", "imgHrefL", "imgHrefR", "imgHrefR");
	} else if (type == "home_img") {
		ary1 = new Array("imgNameOriL", "imgNameThumbL", "imgNameOriR", "imgNameThumbR");
		ary2 = new Array("imgHrefL", "imgHrefL", "imgHrefR", "imgHrefR");
	} else if (type == "dyn_img") {
		no = parseInt(rmObj.id.substring(rmObj.id.lastIndexOf("_") + 1));
		ary1 = new Array("imgNameOri", "imgNameThumb");				
		ary2 = new Array("imgHref", "imgHref");
	} else if (type == "dyn_file") {
		no = parseInt(rmObj.id.substring(rmObj.id.lastIndexOf("_") + 1));
		ary1 = new Array("h_fileName");				
		ary2 = new Array("fileHref");		
	} else if (type == "dyn_prod_img") {
		no = parseInt(rmObj.id.substring(rmObj.id.lastIndexOf("_") + 1));
		ary1 = new Array("imgNameOriL", "imgNameThumbL", "imgNameOriS", "imgNameThumbS", "imgNameOriB", "imgNameThumbB", "imgNameOriF", "imgNameThumbF");
		ary2 = new Array("imgHref", "imgHref", "imgHref", "imgHref", "imgHref", "imgHref", "imgHref", "imgHref");
	} else if (type == "logo") {
		ary1 = new Array("logoNameOri", "logoNameThumb");				
		ary2 = new Array("logoHref", "logoHref");
	} else if (type == "img") {
		ary1 = new Array("imgNameOri", "imgNameThumb");				
		ary2 = new Array("imgHref", "imgHref");
	} else if (type == "imgLS") {
		ary1 = new Array("imgNameOri", "imgNameThumb", "imgNameOriS", "imgNameThumbS");				
		ary2 = new Array("imgHref", "imgHref", "imgHrefS", "imgHrefS");
	} else if (type == "slide") {
		ary1 = new Array("imgNameOri", "imgNameThumb", "imgNameOriB", "imgNameThumbB");
		ary2 = new Array("imgHref", "imgHref", "imgHref", "imgHref");
	} else if (type == "img_s") {
		ary1 = new Array("imgNameOriS", "imgNameThumbS");				
		ary2 = new Array("imgHrefS", "imgHrefS");
	} else if (type == "img_l") {
		ary1 = new Array("imgNameOriL", "imgNameThumbL");				
		ary2 = new Array("imgHrefL", "imgHrefL");
	} else if (type == "file") {
		ary1 = new Array("h_fileName");
		ary2 = new Array("fileHref");
	} else if (type == "file2") {
		ary1 = new Array("h_fileName", "h_fileName2");
		ary2 = new Array("fileHref", "fileHref2");
	} else if (type == "file3") {
		ary1 = new Array("h_fileName", "h_fileName2", "h_fileName3");
		ary2 = new Array("fileHref", "fileHref2", "fileHref3");
	} else if (type == "file5") {
		ary1 = new Array("h_fileName", "h_fileName2", "h_fileName3", "h_fileName4", "h_fileName5");
		ary2 = new Array("fileHref", "fileHref2", "fileHref3", "fileHref4", "fileHref5");
	}	

	for (x = 0; x < ary1.length ; x++) {
		if (prefixId + ary2[x] + (no != null ? "_" + no : "") == rmObj.id) {			
			o = eval("formObj." + ary1[x] + (no != null ? "_" + no : ""));
			if (o != null) {
				o.removeNode(true);
				rmObj.innerHTML = "";
			}
		}
	}
}

function toggleAdvBlock(ary) {
	var x;
	var ele;
	
	for (x = 0; x < ary.length; x++) {
		ele = document.getElementById(ary[x]);
		
		if (ele.style.display != 'none') {
			ele.style.display = "none";
		} else {
			ele.style.display = "";
		}
	
	}
	
}