$(function(){
	var timer, speed = 4000, 
		$BannerPic = $("#banner img")
		_index = 0,
		$gLogo = $(".global .logo"),
		$gNav = $(".global .globalNav");
		
	$gLogo.fadeIn(1000);
	$gNav.delay(800).fadeIn(500);
	$gNav.animate({top:320},600);
		
	function showPic(_num){
		
		$BannerPic.eq(_num).fadeIn(1300).siblings().fadeOut(1300);
	}
	
	function autoShow(){
		
		
		if(_index >=$BannerPic.length){
			_index = 0;
		}		
		showPic(_index);		
		timer = setTimeout(autoShow, speed);		
		_index = _index+1;
	}

	autoShow();
});