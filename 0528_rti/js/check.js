var digits = "0123456789";
var telecom = "";//"- ()";
var telecomdot = ".";
var telecomneg = "-";
var telecomdotsign = ".-";

function StripComma(str)
{
	var x,y = 0,ret ="";	

	for(x=0;x<=str.length-1;x++) {
		y++;
		ret += (str.substr(x,1) != "," ? str.substr(x,1) : "");
	}

	return ret;
}

function comma(num)
{
	var x,y = 0,ret ="";
	var str = new String(num);

	for(x=str.length-1;x>=0;x--) {
		y++;
		ret = str.substr(x,1) + (y%3==1 && y>=3 && str.substr(x,1) != "-" ? "," : "") + ret;
	}
	return ret
}

function replace(str,find,rep)
{
	var newstr = str.replace(find,rep);
	if (str != newstr)
		return replace(newstr,find,rep);
	else
		return str;	
}

function IsDigit(In_str)		// not allowed dot digits
{
	var i, temp;

	for (i=0;i<In_str.length;i++)
	{
		temp=In_str.substring(i,i+1);

		if (digits.indexOf(temp)==-1 && telecom.indexOf(temp)==-1)
			return false;
	}

	return !isNaN(In_str);
}

function IsSignedDotDigit(In_str)		// allowed positive & negative numbers & dots
{
	var i, temp;

	for (i=0;i<In_str.length;i++)
	{
		temp=In_str.substring(i,i+1);

		if (digits.indexOf(temp)==-1 && telecomdotsign.indexOf(temp)==-1)
			return false;
	}

	return !isNaN(In_str);
}


function IsSignedDigit(In_str)		// allowed positive & negative numbers
{
	var i, temp;

	for (i=0;i<In_str.length;i++)
	{
		temp=In_str.substring(i,i+1);

		if (digits.indexOf(temp)==-1 && telecomneg.indexOf(temp)==-1)
			return false;
	}

	return !isNaN(In_str);
}

function IsDotDigit(In_str)		// allowed dot digits & no dot digits
{
	var i, temp;

	for (i=0;i<In_str.length;i++)
	{
		temp=In_str.substring(i,i+1);

		if (digits.indexOf(temp)==-1 && telecomdot.indexOf(temp)==-1)
			return false;
	}

	return !isNaN(In_str);
}

function CheckEmail (emailStr) {
	
emailStr = StripSpaces(emailStr);

if (emailStr.length == 0) return true;

var emailPat=/^(.+)@(.+)$/
var specialChars="\\(\\)<>@,;:\\\\\\\"\\.\\[\\]"
var validChars="\[^\\s" + specialChars + "\]"
var quotedUser="(\"[^\"]*\")"
var ipDomainPat=/^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/
var atom=validChars + '+'
var word="(" + atom + "|" + quotedUser + ")"
var userPat=new RegExp("^" + word + "(\\." + word + ")*$")
var domainPat=new RegExp("^" + atom + "(\\." + atom +")*$")

var matchArray=emailStr.match(emailPat)
if (matchArray==null) {
	return false;
}
var user=matchArray[1];
var domain=matchArray[2];

if (user.match(userPat)==null) {    
    return false;
}

var IPArray=domain.match(ipDomainPat)
if (IPArray!=null) {  
	  for (var i=1;i<=4;i++) {
	    if (IPArray[i]>255) {	       
		return false;
	    }
    }
    return true
}

var domainArray=domain.match(domainPat)
if (domainArray==null) {
    return false;
}

var atomPat=new RegExp(atom,"g")
var domArr=domain.match(atomPat)
var len=domArr.length
if (domArr[domArr.length-1].length<2 || 
    domArr[domArr.length-1].length>3) {   
   return false;
}

if (len<2) {
   return false;
}

return true;
}

function isEmptyString(obj, fieldname)
{
		if(obj.value == "")
		{			
			alert("請輸入" + fieldname + "!");			
			obj.select();
			obj.focus();
			return true;
		}
				
		return false;
}

function openWinRecommand(url)
{
	myWin = open(url, "new", "top=150,left=150,width=560,height=430,toolbar=0,menubar=0,location=0,directories=0,status=0");
	myWin.focus();
}

function StripSpaces(newstr)
{
	while (newstr.substring(0,1) == " ")
		newstr = newstr.substring(1);
	
	while (newstr.substring(newstr.length-1,newstr.length) == " ")
		newstr = newstr.substring(0,newstr.length-1);

	return newstr;
}

function IsChDate(inStr)
{
	if (inStr != "")
	{
	var yr_value, mth_value, dy_value;

	if (inStr.length > 8 || inStr.length < 6)
	{
		//alert("Invalid Date Format!");
		return false;
	}
	
	pos = inStr.indexOf("/")
	yr_value = inStr.substring(0, pos);
	if (yr_value == "")
	{
		return false;
	}

	temp1 = inStr.substring(pos, pos + 1);
	temp2 = inStr.indexOf("/", pos + 1);

	mth_value = inStr.substring(pos + 1, temp2);
	temp2 = temp2 + 1;
	dy_value = inStr.substring(temp2, inStr.length);
	
	if (temp1 != "/" || temp2 == -1 || temp2 == pos + 2 || temp2 > pos + 4)
	{
		//alert("Invalid Date Format!");
		return false;
	}

	if (isNaN(yr_value))
	{
		//alert("Year must be Numeric!");
		return false;
	}
	if (isNaN(mth_value))
	{
		//alert("Month must be Numeric!");
		return false;
	}
	if (isNaN(dy_value))
	{
		//alert("Day must be Numeric!");
		return false;
	}

	if (yr_value < 90 || yr_value > 200)
	{
		//alert("Invalid Year!");
  		return false;
	}	
	if (mth_value < 1 || mth_value > 12)
	{
		//alert("Invalid Month!");
  		return false;
	}
	if (dy_value < 1 || dy_value > 31)
	{
		//alert("Invalid Day!");
  		return false;
	}
	
	if ((mth_value == 4 || mth_value == 6 || mth_value == 9 || mth_value == 11) && (dy_value > 30))
	{
		//alert("Invalid Date!");
  		return false;
	}
	if (((mth_value == 2) && leapYear(yr_value)) && (dy_value > 29))
	{
		//alert("Invalid Date!");
  		return false;
	}
	if (((mth_value == 2) && !(leapYear(yr_value))) && (dy_value>28))
	{
		//alert("Invalid Date!");
  		return false;
	}
	
	return true;
	}
}

function IsNotDate(inStr)
{
	if (inStr != "")
	{
	var yr_value, mth_value, dy_value;

	if (inStr.length > 10 || inStr.length < 8)
	{
		//alert("Invalid Date Format!");
		return true;
	}
	
	yr_value = inStr.substring(0, 4);
	temp1 = inStr.substring(4, 5);
	temp2 = inStr.indexOf("/", 5);
	mth_value = inStr.substring(5, temp2);
	temp2 = temp2 + 1;
	dy_value = inStr.substring(temp2, inStr.length);

	if (temp1 != "/" || temp2 == -1 || temp2 == 6 || temp2 > 8)
	{
		//alert("Invalid Date Format!");
		return true;
	}

	if (isNaN(yr_value))
	{
		//alert("Year must be Numeric!");
		return true;
	}
	if (isNaN(mth_value))
	{
		//alert("Month must be Numeric!");
		return true;
	}
	if (isNaN(dy_value))
	{
		//alert("Day must be Numeric!");
		return true;
	}

	if (yr_value < 1900)
	{
		//alert("Invalid Year!");
  		return true;
	}	
	if (mth_value < 1 || mth_value > 12)
	{
		//alert("Invalid Month!");
  		return true;
	}
	if (dy_value < 1 || dy_value > 31)
	{
		//alert("Invalid Day!");
  		return true;
	}
	
	if ((mth_value == 4 || mth_value == 6 || mth_value == 9 || mth_value == 11) && (dy_value > 30))
	{
		//alert("Invalid Date!");
  		return true;
	}
	if (((mth_value == 2) && leapYear(yr_value)) && (dy_value > 29))
	{
		//alert("Invalid Date!");
  		return true;
	}
	if (((mth_value == 2) && !(leapYear(yr_value))) && (dy_value>28))
	{
		//alert("Invalid Date!");
  		return true;
	}
	
	return false;
	}
}

function leapYear(yr)
{
 	if (((yr % 4 == 0) && yr % 100 != 0) || yr % 400 == 0)
  		return true;
 	else
  		return false;
}

function enlargeControl(obj)
{
	while (obj.scrollHeight > obj.clientHeight) {
		obj.rows++;
	}	
	while (obj.scrollHeight < obj.clientHeight) {
		if (obj.rows <= 1) break;
		obj.rows--;		
	}	
	obj.rows++;
}


function IsValidSSN(ssn){
	//var obj = document.getElementById(objName);
	//if(ssn==''){
		//alert("請填寫身份證字號!!")
		//obj.focus()
		//return false;
	//}
	var LegalID = "0123456789"
	var fResult=true;
	var value = 0;
	var sId=ssn;
	if(sId.length<10)
		fResult=false;
	else{
      if((sId.charAt(0)=='A') || (sId.charAt(0)=='a')) value=10
      else if((sId.charAt(0)=='B') || (sId.charAt(0)=='b')) value=11
      else if((sId.charAt(0)=='C') || (sId.charAt(0)=='c')) value=12
      else if((sId.charAt(0)=='D') || (sId.charAt(0)=='d')) value=13
      else if((sId.charAt(0)=='E') || (sId.charAt(0)=='e')) value=14
      else if((sId.charAt(0)=='F') || (sId.charAt(0)=='f')) value=15
      else if((sId.charAt(0)=='G') || (sId.charAt(0)=='g')) value=16
      else if((sId.charAt(0)=='H') || (sId.charAt(0)=='h')) value=17
      else if((sId.charAt(0)=='J') || (sId.charAt(0)=='j')) value=18
      else if((sId.charAt(0)=='K') || (sId.charAt(0)=='k')) value=19
      else if((sId.charAt(0)=='L') || (sId.charAt(0)=='l')) value=20
      else if((sId.charAt(0)=='M') || (sId.charAt(0)=='m')) value=21
      else if((sId.charAt(0)=='N') || (sId.charAt(0)=='n')) value=22
      else if((sId.charAt(0)=='P') || (sId.charAt(0)=='p')) value=23
      else if((sId.charAt(0)=='Q') || (sId.charAt(0)=='q')) value=24
      else if((sId.charAt(0)=='R') || (sId.charAt(0)=='r')) value=25
      else if((sId.charAt(0)=='S') || (sId.charAt(0)=='s')) value=26
	  else if((sId.charAt(0)=='T') || (sId.charAt(0)=='t')) value=27
      else if((sId.charAt(0)=='U') || (sId.charAt(0)=='u')) value=28
      else if((sId.charAt(0)=='V') || (sId.charAt(0)=='v')) value=29
      else if((sId.charAt(0)=='X') || (sId.charAt(0)=='x')) value=30
      else if((sId.charAt(0)=='Y') || (sId.charAt(0)=='y')) value=31
      else if((sId.charAt(0)=='W') || (sId.charAt(0)=='w')) value=32
      else if((sId.charAt(0)=='Z') || (sId.charAt(0)=='z')) value=33
      else if((sId.charAt(0)=='I') || (sId.charAt(0)=='i')) value=34
      else if((sId.charAt(0)=='O') || (sId.charAt(0)=='o')) value=35
      else fResult = false ;    
    }
    if(fResult==true){
	    value = Math.floor(value/10) + (value%10)*9 +
	    parseInt(sId.charAt(1))*8+
	    parseInt(sId.charAt(2))*7+
	    parseInt(sId.charAt(3))*6+
	    parseInt(sId.charAt(4))*5+
	    parseInt(sId.charAt(5))*4+
	    parseInt(sId.charAt(6))*3+
	    parseInt(sId.charAt(7))*2+
	    parseInt(sId.charAt(8))+
	    parseInt(sId.charAt(9)) ;
	    value = value % 10 ;
	    if(value!=0) fResult = false ;
		var i;
		var c;
		for (i = 1; i < sId.length; i++){
			c = sId.charAt(i);
			if (LegalID.indexOf(c) == -1) fResult = false;
		}
	}
	if(fResult == false){
		//alert("此身份證字號不正確!")
		//obj.focus()
		return false; 
	}
	
	return true;
}

function IsValidMobile(mobile)
{
	if (mobile.length == 0)
		return true;
	else
		return (IsDigit(mobile) && mobile.length == 10);
}

function IsLetterAndNumber(str) {
	if (str.match(/[^a-zA-Z0-9]/)) {
		return false;
	} else {
		return true;
	}
}


function onlyNum() {

    // Mum 0 ~ 9, Backspace, Del
	if (!(event.keyCode==46)&&!(event.keyCode==8)&&!(event.keyCode==37)&&!(event.keyCode==39)) {
		if (!((event.keyCode>=48&&event.keyCode<=57)||(event.keyCode>=96&&event.keyCode<=105))) {
    		event.returnValue = false;
    	}
    }
}