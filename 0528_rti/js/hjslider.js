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
	$('.slideBox').each(function(){
		var $slideBox = $(this);
		var $slideList = $slideBox.find('ul');
		var $slideItems = $slideList.find('li');
		var pageIndex = 0;
		var numsVisible = 7;
		var pageWidth = numsVisible * $slideItems.outerWidth(true);
		var pageCount = Math.ceil($slideItems.length / numsVisible);
		var speed = 200;
		$slideBox.find('.prev, .next').click(function(){
			pageIndex = ($(this).attr('className').indexOf('prev') > -1 ? pageIndex - 1 + pageCount : pageIndex + 1) % pageCount;
			scrollToPage();
		});
		$slideItems.find('a').mouseenter(function() {
			var objImg = $(this).children('img');
			var hoverImage = objImg.attr('hover');
			if (hoverImage != null) {
				objImg.attr('oSrc', objImg.attr('src')).attr('src', hoverImage);
			}
		});
		$slideItems.find('a').mouseleave(function() {
			var objImg = $(this).children('img');
			var restoreImage = objImg.attr('oSrc');
			if (restoreImage != null) {
				objImg.attr('src', restoreImage);
			}
		});
		function scrollToPage(){
			$slideList.stop().animate({
				left: pageWidth * pageIndex * -1
			}, speed);
			$slideBox.find('li').eq(no).addClass('active').siblings().removeClass('active');
		}
		$('.slideBox a').focus(function(){
			this.blur();
		});
	});
	$('.slideBox-two').each(function(){
		var $slideBox = $(this);
		var $slideList = $slideBox.find('ul');
		var $slideItems = $slideList.find('li');
		var pageIndex = 0;
		var numsVisible = 2;
		var pageWidth = numsVisible * $slideItems.outerWidth(true);
		var pageCount = Math.ceil($slideItems.length / numsVisible);
		var speed = 200;
		$slideBox.find('.prev, .next').click(function(){
			pageIndex = ($(this).attr('className').indexOf('prev') > -1 ? pageIndex - 1 + pageCount : pageIndex + 1) % pageCount;
			scrollToPage();
		});
		function scrollToPage(){
			$slideList.stop().animate({
				left: pageWidth * pageIndex * -1
			}, speed);
			$slideBox.find('li').eq(no).addClass('active').siblings().removeClass('active');
		}
		$('.slideBox-two a').focus(function(){
			this.blur();
		});
	});
});