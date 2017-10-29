var TencntART=new Object();
TencntART.Browser=
{
	ie:/msie/.test(window.navigator.userAgent.toLowerCase()),
	moz:/gecko/.test(window.navigator.userAgent.toLowerCase()),
	opera:/opera/.test(window.navigator.userAgent.toLowerCase()),
	safari:/safari/.test(window.navigator.userAgent.toLowerCase())
};
TencntART.JsLoader=
{
	load:function(sUrl,fCallback)
	{
		var _script=document.createElement('script');
		_script.setAttribute('charset','utf-8');
		_script.setAttribute('type','text/javascript');
		_script.setAttribute('src',sUrl);
		document.getElementsByTagName('head')[0].appendChild(_script);
		if(TencntART.Browser.ie)
		{
			_script.onreadystatechange=function()
			{
				if(this.readyState=='loaded'||this.readyStaate=='complete')
				{
					fCallback();
				}
			};
		}else if(TencntART.Browser.moz)
		{
			_script.onload=function()
			{
				fCallback();
			};
		}else
		{
			fCallback();
		}
	}
};
var TencentArticl=new Object();
TencentArticl=
{
	$:function(v){return document.getElementById(v)},
	getEles:function(id,ele)
	{	
		 return this.$(id).getElementsByTagName(ele);
	},
	tabId:"sildPicBar",
	tabDot:"dot",
	tabBox:"cnt-wrap",
	tabSilder:"cnt",
	tabSilderSon:"li",
	Count:function()
	{
		return this.getEles(this.tabSilder,this.tabSilderSon).length
	 },
	 Now:0,
	 isSild:true,
	 SildTab:function(now)
	 {
		 this.Now=Number(now);
		 if(this.Now>Math.ceil(this.Count()/4)-1)
		 {
			 this.Now=0;
		 }else if(this.Now<0)
		 {
			 this.Now=Math.ceil(this.Count()/4)-1;
		 }
		 
		if(parseInt(this.$(this.tabSilder).style.left)>-156*parseInt(this.Now*4))
		{
			this.moveR();
		}else
		{
			this.moveL();
		}
		for(var i=0;i<Math.ceil(this.Count()/4);i++)
		{
			if(i==this.Now)
			{
				this.getEles(this.tabId,"li")[this.Now].className="select";
			}else
			{
				
				
				this.getEles(this.tabId,"li")[i].className="";
			}
		}
	},
	moveR:function(setp)
	{
		var _curLeft=parseInt(this.$(this.tabSilder).style.left);
		var _distance=50;
		if(_curLeft>-156*parseInt(this.Now*4))
		{
			this.$(this.tabSilder).style.left=(_curLeft-_distance)+26+"px";
			window.setTimeout("TencentArticl.moveR()",1);
		}
	},
	moveL:function(setp)
	{
		var _curLeft=parseInt(this.$(this.tabSilder).style.left);
		var _distance=50;
		if(_curLeft<-156*parseInt(this.Now*4))
		{
			this.$(this.tabSilder).style.left=(_curLeft+_distance)-26+"px";
			window.setTimeout("TencentArticl.moveL()",1);
		}
	},
	pagePe:function(way)
	{
		if(way=="next")
		{
			this.Now+=1;
			this.SildTab(this.Now);
		}else
		{
			this.Now-=1;this.SildTab(this.Now);
		}
	},
	smallCk:function()
	{
		for(var i=0;i<Math.ceil(this.Count()/4);i++)
		{
			if(i==0)
			{
				this.$(this.tabDot).innerHTML+="<li class='select' onclick='TencentArticl.SildTab("+i+")'></li>";
			}else
			{
				this.$(this.tabDot).innerHTML+="<li onclick='TencentArticl.SildTab("+i+")'></li>";
			}
		}
	},
	onload:function()
	{
		TencentArticl.ints();
		setInterval("TencentArticl.pagePe('next')",8000);
	},
	ints:function()
	{
		if(this.isSild)
		{
			this.$(this.tabBox).style.position="relative";
			this.$(this.tabSilder).style.position="absolute";
			this.$(this.tabSilder).style.left=0+"px";
			this.getEles(this.tabId,"span")[1].onclick=function(){TencentArticl.pagePe("next");}
			this.getEles(this.tabId,"span")[0].onclick=function(){TencentArticl.pagePe("pre");}
			this.smallCk();
		}
	}
}


var TencentArtic2=new Object();
TencentArtic2=
{
	$:function(v){return document.getElementById(v)},
	getEles:function(id,ele)
	{	
		 return this.$(id).getElementsByTagName(ele);
	},
	tabId:"sildPicBar2",
	tabDot:"dot2",
	tabBox:"cnt-wrap2",
	tabSilder:"cnt2",
	tabSilderSon:"li",
	Count:function()
	{
		return this.getEles(this.tabSilder,this.tabSilderSon).length
	 },
	 numsVisible: 6,
	 Now:0,
	 isSild:true,
	 SildTab:function(now)
	 {
		 this.Now=Number(now);
		 if(this.Now>Math.ceil(this.Count()/this.numsVisible)-1)
		 {
			 this.Now=0;
		 }else if(this.Now<0)
		 {
			 this.Now=Math.ceil(this.Count()/this.numsVisible)-1;
		 }
		 
		if(parseInt(this.$(this.tabSilder).style.left)>-156*parseInt(this.Now*this.numsVisible))
		{
			this.moveR();
		}else
		{
			this.moveL();
		}
		for(var i=0;i<Math.ceil(this.Count()/this.numsVisible);i++)
		{
			if(i==this.Now)
			{
				this.getEles(this.tabId,"li")[this.Now].className="select";
			}else
			{
				
				
				this.getEles(this.tabId,"li")[i].className="";
			}
		}
	},
	moveR:function(setp)
	{
		var _curLeft=parseInt(this.$(this.tabSilder).style.left);
		var _distance=50;
		if(_curLeft>-156*parseInt(this.Now*this.numsVisible))
		{
			this.$(this.tabSilder).style.left=(_curLeft-_distance)+26+"px";
			window.setTimeout("TencentArtic2.moveR()",1);
		}
	},
	moveL:function(setp)
	{
		var _curLeft=parseInt(this.$(this.tabSilder).style.left);
		var _distance=50;
		if(_curLeft<-156*parseInt(this.Now*this.numsVisible))
		{
			this.$(this.tabSilder).style.left=(_curLeft+_distance)-26+"px";
			window.setTimeout("TencentArtic2.moveL()",1);
		}
	},
	pagePe:function(way)
	{
		if(way=="next")
		{
			this.Now+=1;
			this.SildTab(this.Now);
		}else
		{
			this.Now-=1;this.SildTab(this.Now);
		}
	},
	smallCk:function()
	{
		for(var i=0;i<Math.ceil(this.Count()/this.numsVisible);i++)
		{
			if(i==0)
			{
				this.$(this.tabDot).innerHTML+="<li class='select' onclick='TencentArtic2.SildTab("+i+")'></li>";
			}else
			{
				this.$(this.tabDot).innerHTML+="<li onclick='TencentArtic2.SildTab("+i+")'></li>";
			}
		}
	},
	onload:function()
	{
		TencentArtic2.ints();
		return;
	},
	ints:function()
	{
		if(this.isSild)
		{
			this.$(this.tabBox).style.position="relative";
			this.$(this.tabSilder).style.position="absolute";
			this.$(this.tabSilder).style.left=0+"px";
			this.getEles(this.tabId,"span")[1].onclick=function(){TencentArtic2.pagePe("next");}
			this.getEles(this.tabId,"span")[0].onclick=function(){TencentArtic2.pagePe("pre");}
			this.smallCk();
		}
	}
}