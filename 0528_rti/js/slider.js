E.onDOMReady(function(){
	//初始化当前产品的内容，默认为全部隐藏
	var currentLink = D.query('.svc .content li.current a');
	if(currentLink.length >0){
		var currentCont = currentLink[0].getAttribute('data-type');
		D.removeClass(D.get(currentCont),'fn-hide');
	}
	
	//根据icon显示相应的信息
	function showinfo(li){
		var target = D.getChildren(li)[0].getAttribute('data-type');
		D.addClass(D.query('.svc-content .item:not(.fn-hide)'),'fn-hide');
		D.removeClass(D.get(target),'fn-hide');
		
		D.removeClass(D.query('.svc .content li.current'),'current');
		D.addClass(li,'current');
	}

	//点击icon显示相应内容
	E.on(D.query('.svc .content li'),'mouseover',function(e){
		E.preventDefault(e);
		showinfo(this);
	});
	
	//点击左右的滑动效果
	var index = 1;
	var end = D.query('.svc .container')[0].offsetWidth;
	var content = D.query('.svc .content')[0];
	var list = D.query('li',content);
	content.style.width = ( list[0].offsetWidth + 7 ) * list.length + 'px';
	var left = new YAHOO.util.Anim(content,{left:{to:0}},1,YAHOO.util.Easing.easeOut);
	var right = new YAHOO.util.Anim(content,{left:{to:-end}},1,YAHOO.util.Easing.easeOut);
	
	E.on(D.query('.nav-left a'),'click',function(e){
		E.preventDefault(e);	
		if(index == 2){
			index = 1;
			left.animate();
			D.query('.svc em').forEach(function(o){
				o.innerHTML = index;
			});			
			
			showinfo(list[0]);
		}
	});
	
	E.on(D.query('.nav-right a'),'click',function(e){
		E.preventDefault(e);	
		if(index == 1){
			index = 2;
			right.animate();
			D.query('.svc em').forEach(function(o){
				o.innerHTML = index;
			});			
			

			if(list.length >= 10){ //如果第二排的第一个存在，则选中第一个
				showinfo(list[9]);
			}
		}
	});
});