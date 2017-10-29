//=================================
//function 정리 Update 20070108
//=================================


//========================================================================
// 함수 이름 (플래시파일 이름, 경로, 넓이, 높이)
//========================================================================

function flashOpenTran(FN_Src,FN_Width,FN_Height, FN_Tran){

    if (FN_Tran == null || FN_Tran == "undifined"){FN_Tran = "Transparent";}

    document.write('<OBJECT classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" WIDTH="' + FN_Width + '" HEIGHT="' + FN_Height + '" id="" ALIGN="" VIEWASTEXT>');
    document.write('<PARAM NAME=movie VALUE="' + FN_Src + '"> ');
    document.write('<PARAM NAME=quality VALUE=high>');
    document.write('<PARAM NAME=wmode VALUE="'+ FN_Tran +'">');
    document.write('<PARAM NAME=bgcolor VALUE=#FFFFFF>');
    document.write('<EMBED src="' + FN_Src + '" quality=high bgcolor=#FFFFFF  WIDTH="' + FN_Width + '" HEIGHT="' + FN_Height + '" wmode="'+ FN_Tran +'" NAME="" ALIGN="" TYPE="application/x-shockwave-flash" PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer"></EMBED>');
    document.write('</OBJECT>');
}

