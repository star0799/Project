$(function(){

	var $ShowBtn = $('.collection  .datalist  a.showBtn'),
		$ShowTxt = $('.collection  .datalist .txt2');

	$ShowBtn.click(function(){
		$(this).fadeOut(0);
		$ShowTxt.fadeIn(300);		
		return false
	})
});