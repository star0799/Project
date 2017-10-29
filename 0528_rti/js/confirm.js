function removeFile(sender, type, prefixId) {
	var x, no = null;
	var ary1, ary2, o;

	if (!confirm("確定移除此檔案?")) {
		return false;
	}

	var rmObj = $(sender).parent().get(0);

	// hidden fields
	if (type == "dyn_prod_img") {
		no = parseInt(rmObj.id.substring(rmObj.id.lastIndexOf("_") + 1));
		ary1 = new Array("imgNameThumb", "imgNameThumb2", "imgNameThumb3", "imgNameThumb4", "imgNameThumbL", "imgNameThumbS", "imgNameThumbB", "imgNameThumbF");
		ary2 = new Array("imgHref", "imgHref2", "imgHref3", "imgHref4", "imgHref", "imgHref", "imgHref", "imgHref");
	} else if (type == "img") {
		ary1 = new Array("imgNameThumb");
		ary2 = new Array("imgHref");
	} else if (type == "imgLS") {
		ary1 = new Array("imgNameThumb", "imgNameThumbS");
		ary2 = new Array("imgHref", "imgHref");
	} else if (type == "img2") {
		ary1 = new Array("imgNameThumb", "imgNameThumb2", "imgNameThumb3", "imgNameThumb4", "imgNameThumb5");
		ary2 = new Array("imgHref", "imgHref2", "imgHref3", "imgHref4", "imgHref5");
	} else if (type == "file") {
		ary1 = new Array("h_fileName");
		ary2 = new Array("fileHref");
	}

	if (prefixId == null) {
		prefixId = "master_ContentPlaceHolder1_";
	}
	for (x = 0; x < ary1.length ; x++) {
		if (prefixId + ary2[x] + (no != null ? "_" + no : "") == rmObj.id) {
			o = document.getElementById(ary1[x] + (no != null ? "_" + no : ""));
			if (o != null) {
				//o.parentNode.removeChild(o);
				//rmObj.innerHTML = "";
				$(o).remove();
				$(sender).parent().html('');
			}
		}
	}
}