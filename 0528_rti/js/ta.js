
function addEvent( obj, type, fn )
{
	if (obj.addEventListener) {
		obj.addEventListener( type, fn, false );
	} else if (obj.attachEvent)	{
		obj["evt" + type+fn] = fn;
		obj[type + fn] = function() { obj["evt" + type + fn]( window.event ); }
		obj.attachEvent( "on" + type, obj[type + fn] );
	}
}

function initTextareas(){

	if (document.getElementById && document.createElement && document.appendChild) {
	
		var taCollection = document.getElementsByTagName("textarea");
		var ta, o, hiddenid;

		for (var x = 0; x < taCollection.length; x++) {
			ta = taCollection[x];			

			if (ta.disableStat == undefined || ta.disableStat == 'false') {
			
				if (ta.id.lastIndexOf("_") == -1) {
					hiddenid = ta.id;
				} else {
					if (!isNaN(ta.id.substring(ta.id.lastIndexOf("_") + 1))) {
						hiddenid = ta.id.substring(0, ta.id.lastIndexOf("_"));
					} else {		// not yet auto-numbering
						hiddenid = ta.id;
					}
				}
				
				o = document.getElementById("div_" + ta.id);
				if (o == null) {
					var newDiv = document.createElement("div");
					newDiv.setAttribute("id", "div_" + ta.id);
					// alert(x + ": " +ta.id)
					newDiv.innerHTML = "" + ta.value.length + " / " + (parseInt(document.getElementById("ta_" + hiddenid).value) - parseInt(ta.value.length));
					newDiv.innerHTML = "<img src=images/word.gif>" + newDiv.innerHTML;
	
					var taParent = ta.parentNode;
					taParent.insertBefore(newDiv, ta);
				}
				
				ta.onchange = checkCharLen;
				ta.onkeyup = checkCharLen;
			}
		}		
	}
}

function checkCharLen() {
	var hiddenid;
	
	if (this.id.lastIndexOf("_") == -1) {
		hiddenid = this.id;
	} else {
		if (!isNaN(this.id.substring(this.id.lastIndexOf("_") + 1))) {
			hiddenid = this.id.substring(0, this.id.lastIndexOf("_"));
		} else {		// not yet auto-numbering
			hiddenid = this.id;
		}
	}

	var intChars =  parseInt(this.value.length);
	var intMax = parseInt(document.getElementById("ta_" + hiddenid).value);

	var objPrompt = document.getElementById("div_" + this.id);
	
	if (intChars > intMax) {
		this.value = this.value.substring(0, intMax);
		objPrompt.innerHTML = "<span style='color: red'>" + intMax + "  / 0</span";
	} else {
		objPrompt.innerHTML = "" + intChars + " / " + (intMax - intChars);
	}
	
	objPrompt.innerHTML = "<img src=images/word.gif>" + objPrompt.innerHTML;
}

addEvent(window, 'load', initTextareas);