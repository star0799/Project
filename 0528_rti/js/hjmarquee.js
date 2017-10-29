/*
	hjslider v1.0
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
$(function(){
	$('.marqueeBox').each(function(){
		var $mBox = $(this);
		var $mList = $mBox.find('ul');
		var $mItems = $mList.append($mList.html()).children();
		var mHeight = -$mBox.height();
		var scrollstaySpeed = 500;
		var staySpeed = 3000;
		var timer;
		var startPos = $mItems.length / 2 * mHeight;
		$mList.css('top', startPos);
		$mItems.hover(function(){
			clearTimeout(timer);
		}, function(){
			timer = setTimeout(scrollMarquee, staySpeed);
		});
		function scrollMarquee(){
			var mIndex = $mList.position().top / mHeight;
			mIndex = (mIndex - 1 + $mItems.length) % $mItems.length;
			$mList.animate({
				top: mIndex * mHeight
			}, scrollstaySpeed, function(){
				if(mIndex == 0){
					$mList.css('top', startPos);
				}
			});
			timer = setTimeout(scrollMarquee, staySpeed);
		}
		timer = setTimeout(scrollMarquee, staySpeed);
		$('.marqueeBox a').focus(function(){
			this.blur();
		});
	});
});