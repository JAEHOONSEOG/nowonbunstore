/**
 * 
 */
$(function(){
	$("#cancel").bind("click",function(){
		$("input[type=text]").each(function(){
			$(this).val("");
		});
		$("input[type=password]").each(function(){
			$(this).val("");
		});
	});
});