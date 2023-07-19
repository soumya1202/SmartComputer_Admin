"use strict";function responsiveMenu(){var e=$(".dashboardPage .header").outerHeight();$(".dashboardMenu").css("top",e+"px")}function footerBottom(){$("#pageFooter").outerHeight()}function formSize(){var e=$(window).width();e<850?$(".customDDmenu").width(e-30):$(".customDDmenu").removeAttr("style"),e<450?$(".singleForm").width(e-30):$(".singleForm").removeAttr("style")}var resizeTimer;$(document).ready(function(){responsiveMenu(),footerBottom(),formSize(),$(".sb-container").scrollBox(),$(".btn-equipment").click(function(){$("#addEquipment").addClass("active")}),$("#addEquipment .closeEquipment").click(function(){$("#addEquipment").removeClass("active")}),$(".btn-editEquipment").click(function(){$("#editEquipment").addClass("active"),$("html, body").animate({scrollTop:$("#editEquipment").offset().top-30},500)}),$("#editEquipment .closeEquipment").click(function(){$("#editEquipment").removeClass("active")}),$(".btn-deleteEquipment").click(function(){$("#deleteEquipment").addClass("active"),$("html, body").animate({scrollTop:$("#deleteEquipment").offset().top-30},500)}),$("#deleteEquipment .closeEquipment").click(function(){$("#deleteEquipment").removeClass("active")}),$(".btn-generateQRcode").click(function(){$("#generateQRcode").addClass("active"),$("html, body").animate({scrollTop:$("#generateQRcode").offset().top-30},500)}),$("#generateQRcode .closeEquipment").click(function(){$("#generateQRcode").removeClass("active")}),$(".generateButton").click(function(){$(".generateForm").hide(),$(".generatedQr").show()}),$(".btn-editQRcode").click(function(){$("#editQRcode").addClass("active"),$("html, body").animate({scrollTop:$("#editQRcode").offset().top-30},500)}),$("#editQRcode .closeEquipment").click(function(){$("#editQRcode").removeClass("active")}),$(".btn-equipmentDecommissioned").click(function(){$("#equipmentDecommissioned").addClass("active"),$("html, body").animate({scrollTop:$("#equipmentDecommissioned").offset().top-30},500)}),$("#equipmentDecommissioned .closeEquipment").click(function(){$("#equipmentDecommissioned").removeClass("active")}),$(".statusSelect").change(function(e){$("#manageStatus").addClass("active"),$("html, body").animate({scrollTop:$("#manageStatus").offset().top-30},500)}),$("#manageStatus .closeEquipment").click(function(){$("#manageStatus").removeClass("active")}),$(".btn-addCategory").click(function(){$("#addCategory").addClass("active"),$("html, body").animate({scrollTop:$("#addCategory").offset().top-30},500)}),$("#addCategory .closeEquipment").click(function(){$("#addCategory").removeClass("active")}),$(".btn-editCategory").click(function(){$("#editCategory").addClass("active"),$("html, body").animate({scrollTop:$("#editCategory").offset().top-30},500)}),$("#editCategory .closeEquipment").click(function(){$("#editCategory").removeClass("active")}),$(".btn-addChecklist").click(function(){$("#addChecklist").addClass("active"),$("html, body").animate({scrollTop:$("#addChecklist").offset().top-30},500)}),$("#addChecklist .closeEquipment").click(function(){$("#addChecklist").removeClass("active")}),$(".btn-editChecklist").click(function(){$("#editChecklist").addClass("active"),$("html, body").animate({scrollTop:$("#editChecklist").offset().top-30},500)}),$("#editChecklist .closeEquipment").click(function(){$("#editChecklist").removeClass("active")}),$("#nav-icon").click(function(){$(this).toggleClass("open"),$(".dashboardMenu").toggleClass("active"),$("body").toggleClass("scroolLock")})}),$(window).trigger("resize.scrollBox"),$(window).on("resize",function(e){clearTimeout(resizeTimer),resizeTimer=setTimeout(function(){responsiveMenu(),footerBottom(),formSize()},250)});