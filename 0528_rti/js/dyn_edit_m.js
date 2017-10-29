/*
	Dyn Edit v1.0
	Copyright 2011, Haor
	This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
 */
 function DynEdit(setNo, prefixId) {
	if (setNo == null) {
		setNo = '';
	}
	var instance = this;
	var formObj = document.getElementById("aspnetForm");
	if (prefixId == null) {
		prefixId = "master_ContentPlaceHolder1_";
	}
	
	var tblList = document.getElementById(prefixId + "tblList" + setNo);
	var tblBack = document.getElementById(prefixId + "tblBack" + setNo);
	var aryHidden = [];
	var itemCount = document.getElementById("itemCount" + setNo);
	
	jQuery.fn.outerHTML = function(s) {
		return (s) ? this.before(s).remove() : jQuery("<div>").append(this.eq(0).clone()).html();
	};
	
	instance.swapNode = function(node1,node2) { 
		var _parent=node1.parentNode; 
		var _t1=node1.nextSibling;
		var _t2=node2.nextSibling;
		if(_t1) 
			_parent.insertBefore(node2,_t1); 
		else 
			_parent.appendChild(node2); 
			
		if(_t2) 
			_parent.insertBefore(node1,_t2);
		else 
			_parent.appendChild(node1);
	};
	
	instance.checkMainItem = function(o, td) {
		if (o.checked) {
			var t = document.getElementById(td);
			for (var x = 0; x < t.children.length; x++) {
				t.children[x].checked = o.checked;
			}
		}
	};
	
	instance.checkSubItems = function(o, table) {
	
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
	};	
	
	instance.setCtlNameID = function(o, n)
	{
		var x, y, i, oo;
		for (x=0;x<o.cells.length;x++){
			for (y=0;y<o.cells[x].children.length;y++){
				oo = o.cells[x].children[y];
				instance.setChildCtlNameID(oo, n);
				for (i=0;i<oo.children.length;i++){
					instance.setChildCtlNameID(oo.children[i], n);
				}
				
			}
		}
	};
	
	instance.setChildCtlNameID = function(o, n){
		var specifyName = false, i;
		if (o.tagName.toUpperCase() == "TABLE") {
			for (i=0;i<o.rows.length;i++){
				instance.setCtlNameID(o.rows[i], n);
			}
		} else if (o.tagName.toUpperCase() == "INPUT") {								
			//specifyName = (o.type.toLowerCase() != "radio");		// e.g. id = genderF, genderM, name = gender
		} else if (o.tagName.toUpperCase() == "DIV" || o.tagName.toUpperCase() == "SPAN") {
			for (i=0;i<o.children.length;i++){
				instance.setChildCtlNameID(o.children[i], n);
			}
		}
		
		var id = o.id//(o.getAttribute("jsId") == null ? o.id : o.getAttribute("jsId"));
		var name = o.name//(o.getAttribute("jsName") == null ? o.name : o.getAttribute("jsName"));
		
		if (o.name != null && o.name != "") {
			
			if (id.lastIndexOf("_") == -1) {
				o.id = (id != null && id != "") ? id : name + "_" + n;
			} else {
				if (!isNaN(id.substring(id.lastIndexOf("_") + 1))) {
					o.id = id.substring(0, id.lastIndexOf("_") + 1) + n;
				} else {
					o.id = id + "_" + n;
				}
			}		
	
			if (!(o.tagName.toUpperCase() == "INPUT" && o.type.toLowerCase() == "radio")) {
				o.name = o.id;		// set name directly equals to id (prefixId)
			} else {				// input type is radio
				
				if (o.getAttribute("t_group") == null) {
					if (o.name.lastIndexOf("_") == -1) {
						o.name = name.replace(/\$/g, "_");// + "_" + n;
					} else {
						o.name = name.substring(0, name.lastIndexOf("_") + 1).replace(/\$/g, "_") + n;
					}
				}
				
				if (o.getAttribute("t_value") != null) o.value = o.getAttribute("t_value");
				if (o.getAttribute("t_checked") != null) o.checked = (o.getAttribute("t_checked") == 1);
				//alert(o.name+";      "+o.id + "         "+o.checked);
			}
			
		} else if (o.id != null && o.id != "") {
			
			if (id.lastIndexOf("_") == -1) {
				o.id = id + "_" + n;
			} else {
				if (!isNaN(id.substring(id.lastIndexOf("_") + 1))) {
					o.id = id.substring(0, id.lastIndexOf("_") + 1) + n;
				} else {
					o.id = id + "_" + n;
				}
			}

			// specify names
			if (!(o.tagName.toUpperCase() == "INPUT" && o.type.toLowerCase() == "radio")) {
				o.name = o.id;		// set name directly equals to id
			} else {
	
				if (name.lastIndexOf("_") == -1) {
					o.name = name.replace(/\$/g, "_");// + "_" + n;
				} else {
					o.name = name.substring(0, name.lastIndexOf("_") + 1).replace(/\$/g, "_") + n;
				}	
				
				if (o.getAttribute("t_value") != null) o.value = o.getAttribute("t_value");
				if (o.getAttribute("t_checked") != null) o.checked = (o.getAttribute("t_checked") == 1);
			}
		}
	};
	
	instance.selectAll = function(o) {
		var e = document.getElementsByTagName("input");
		
		for (var x = 0; x < e.length; x++) {
			if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkSelect" + setNo + "_(.+)$"))) {
				if (!e[x].disabled) {
					e[x].checked = o.checked;
				}
			}			
		}
	};
	instance.selectOne = function(o) {
		var e = document.getElementsByTagName("input");
		var checked = true;
		
		for (var x = 0; x < e.length; x++) {
			if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkSelect" + setNo + "_(.+)$")) && !e[x].checked) {
				checked = false;
				break;
			}			
		}
		for (var x = 0; x < e.length; x++) {
			if (e[x].type == "checkbox" && e[x].id.match(new RegExp(prefixId + "chkAll" + setNo + "$"))) {
				e[x].checked = checked;
				break;
			}
		}
	};
	
	instance.fnRemoveItem = function(blockNo) {			
		var x, y, o, no, objName, newName;
	
		$(tblList.rows[blockNo]).remove();
		
		itemCount.value = parseInt(itemCount.value) - 1;	
	
		// remove hidden fields
		for (x=0 ; x < instance.aryHidden.length ; x++) {
			o = document.getElementById(instance.aryHidden[x] + "_" + blockNo);
			if (o != null) $(o).remove();
		}
	
		// renew hidden field names
		for(x = 0 ; x < formObj.elements.length ; x++){
			o = formObj.elements[x];
			if (o.tagName.toUpperCase() == "INPUT") {
				if (o.type.toLowerCase() == "hidden" && o.name.lastIndexOf("_") != -1) {
					// hidden fields				
					objName = o.name.substring(0, o.name.lastIndexOf("_"));
					for (y = 0 ; y < instance.aryHidden.length ; y++) {
						if (objName == instance.aryHidden[y]) {
							no = o.name.substring(o.name.lastIndexOf("_") + 1);	
							if (no > blockNo) {
								
								// renew name
								newName = o.name.substring(0, o.name.lastIndexOf("_") + 1) + (parseInt(no) - 1);
								$(o).outerHTML( "<input type=\"hidden\" id=\"" + newName + "\" name=\"" + newName + "\" value=\"" + o.value + "\">" );
								
							}						
						}
					}
				}
			}
		}
		
		instance.renewTableElementsId();
	};
	
	
	instance.htmlEncode = function(value){ 
	  return $('<div/>').text(value).html(); 
	};
	
	instance.htmlDecode = function(value){ 
	  return $('<div/>').html(value).text(); 
	};
	
	instance.addItem = function(){
		itemCount.value = parseInt(itemCount.value) + 1;
		$(tblList).append( $(tblBack.rows[0]).clone() )
		instance.setCtlNameID(tblList.rows[tblList.rows.length - 1], itemCount.value);
	};
	
	instance.removeItem = function(){
		var x, y, o, noCheck;			
		var aryRM = new Array("itemCount" + setNo);

		noCheck = true;
		for(x=1 ; x <= parseInt(itemCount.value) ; x++){
			o = document.getElementById(prefixId + "chkSelect" + setNo + "_" + x);
			if (o.checked) 	{
				noCheck = false;
				break;
			}
		}

		if (noCheck) {
			alert("請選擇要移除的項目!");
			o = document.getElementById(prefixId + "chkSelect" + setNo + "_1");
			if (o != null) o.focus();
			return false;
		}									
		if (!confirm("確定要移除選擇的項目嗎?")){
			return false;
		}

		// ------- remove items -------	
		for(x = 1 ; x <= parseInt(itemCount.value) ; x++){
			o = document.getElementById(prefixId + "chkSelect" + setNo + "_" + x);
			if (o.checked) {
				instance.fnRemoveItem(x, aryRM);
				x -= 1;
			}
		}
		
		if (document.getElementById(prefixId + "chkAll" + setNo) != undefined) {
			document.getElementById(prefixId + "chkAll" + setNo).checked = false;
		}
	};
	instance.removeAllItems = function(){
		var x, y, o;			
		var aryRM = new Array("itemCount" + setNo);
		
		// ------- remove items -------	
		for(x = 1 ; x <= parseInt(itemCount.value) ; x++){
			instance.fnRemoveItem(x, aryRM);
			x -= 1;
		}
		
		if (document.getElementById(prefixId + "chkAll" + setNo) != undefined) {
			document.getElementById(prefixId + "chkAll" + setNo).checked = false;
		}
	};
	instance.renewTableElementsId = function(){
		for(var x = 1 ; x <= parseInt(itemCount.value) ; x++){
			instance.setCtlNameID(tblList.rows[x], x);
		}
	};
	
	instance.sortItem = function(trigger, cmd) {
		var x, sNo, o, tNo, tmpValue, obj1, obj2, h;
		sNo = trigger.id.substring(trigger.id.lastIndexOf("_") + 1);
		
		// fix radio value disappear problem
		// http://209.85.175.104/search?q=cache:IN2ASTBsFhYJ:www.cnblogs.com/chinahnzl/articles/547669.html+radio+button+value+swapnode&hl=zh-TW&ct=clnk&cd=3&gl=tw
		var el = document.getElementsByTagName("input")
		var arr = [];
		
		for (i = 0; i < el.length; i++) {
			if (el[i].type == "radio")
				arr.push(el[i], el[i].checked);
		}
	
		if (cmd == "Down") {
			tNo = parseInt(sNo) + 1;
			if (tNo > parseInt(itemCount.value)) tNo = 1;		
		} else if (cmd == "Up") {
			tNo = parseInt(sNo) - 1;
			if (tNo < 1) tNo = parseInt(itemCount.value);
		} else if (cmd == "Bottom") {
			for (x = sNo; x < parseInt(itemCount.value); x++) {
				trigger = document.getElementById("btnSortDown" + setNo + "_" + x);
				instance.sortItem(trigger, 'Down');
			}
			return;
		} else if (cmd == "Top") {
			for (x = sNo; x > 1; x--) {
				trigger = document.getElementById("btnSortUp" + setNo + "_" + x);
				instance.sortItem(trigger, 'Up');
			}
			return;
		}
		

		// swap item
		instance.swapNode(tblList.rows[sNo], tblList.rows[tNo]);

		// swap hidden field values
		for(x = 0 ; x < instance.aryHidden.length ; x++){
			o = document.getElementById(instance.aryHidden[x] + "_" + sNo);
			if (o == null){
				h = document.createElement("input");
				h.type = "hidden";
				h.name = instance.aryHidden[x] + "_" + sNo;
				h.id = h.name;
				formObj.appendChild(h);
			}
			
			o = document.getElementById(instance.aryHidden[x] + "_" + tNo);
			if (o == null){
				h = document.createElement("input");
				h.type = "hidden";
				h.name = instance.aryHidden[x] + "_" + tNo;
				h.id = h.name;
				formObj.appendChild(h);
			}
			
			obj1 = document.getElementById(instance.aryHidden[x] + "_" + sNo);
			obj2 = document.getElementById(instance.aryHidden[x] + "_" + tNo);
			tmpValue = obj1.value;
			obj1.value = obj2.value
			obj2.value = tmpValue;
		}
		
		
		instance.renewTableElementsId(tblList);
		
		while (arr.length > 0)
			arr.shift().checked = arr.shift();
		
	};
}