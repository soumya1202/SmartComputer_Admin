$(document).ready(function () {
    // $('.tblCategory').dataTable();
    $('.closecategory').click(function () {
        $('#addVarOpt').removeClass('active');
    });

    ServerSideVarOptDataTable();
    //DatableSearchColumnWise("tblCategory");

});

function OpenCrudpopUp() {
    $('#addVarOpt').removeClass('active');
    // show Modal
    $('#VarOptAddDone').modal('show');
}
function OpenCrudpopUpDanger() {
    $('#txtVarOptDangerMsg').val();
    $('#addVarOpt').removeClass('active');
    // show Modal
    $('#VarOptError').modal('show');
}

function AddVarOpt() {
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var VarOptName = $("#txt_VarOpt").val().trim();
    /*var catImage = $("#cat_Img").prop("files")[0];*/
    //var BrandImage = $("#brand_Img").prop("files")[0];
    //alert(catImage.getAsDataURL());
    var VarOptId = $('#txtHiddenVarOptId').val().trim();
    //var BrandPrevImage = $("#txthiddenPrvImage").val().trim();
    if (VarOptId.length > 0) {
        formData.append("IN_Id", VarOptId);
    }

    if (!HasValue(VarOptName)) {
        SetBorderColorOfErrorField($("#txt_VarOpt"));
        isValid = 0;
    }
    //if (!HasValue(catDesc)) {
    //    SetBorderColorOfErrorField($("#txt_desc"));
    //    isValid = 0;
    //}
    if (isValid == 1) {
        formData.append("IN_Name", VarOptName);
        //formData.append("IN_Image", BrandImage);
        //formData.append("IN_PrvImage", BrandPrevImage);
        //  formData.append("IN_IsActive", true);
        VarOptCrud(formData);
        ResetField();
    }

}
function VarOptCrud(formData) {
    $.ajax({
        type: "POST",
        url: '/VarientOption/VarientOptCrud',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {

            $('#addVarOpt').removeClass('active');
            ResetField();
            // alert(JSON.parse(result));
            if (result.returnCode == '200') {
                ServerSideVarOptDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtVarOptCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtVarOptDangerMsg").text(result.returnMessage);
                OpenCrudpopUpDanger();
            }
        },
        failure: function (response) {
            alert('Failed');
        }
    });
};
function ResetField() {
    $("#txt_VarOpt").val("");
    //$("#brand_Img").val("");
    $('#txtHiddenVarOptId').val("");
    //$('#txthiddenPrvImage').val("");
    //$('#divUploadedImg').hide();
    $("#btnActive").hide();
    $("#btnDeactive").hide();
    $("#txt_Reason").val("");
    $("#btnActive").attr('disabled', 'disabled');
    $("#btnDeactive").attr('disabled', 'disabled');
}

function ServerSideVarOptDataTable() {
    $('.tblCategory').DataTable({
        "processing": true,
        "destroy": true,
        scrollX: false,
        "ajax": {
            "url": "/VarientOption/VarientOptDataTable",
            dataSrc: ''
            //success: function (result) {
            //    alert(JSON.stringify(result));
            //}
        },
        "columns": [{
            "data": "id",
            "sClass": "dsplynone hiddenVarOptId",

        },
        {
            "data": "varientName",
            "sClass": "varoptName"
        },
        //{
        //    "data": "imagePath",
        //    "sClass": "dsplynone prvImagePath"
        //},
        //{
        //    'data': 'imagePath',
        //    "sClass": " ",
        //    'render': function (data) {
        //        return '<img src="/Upload/Brand/' + data +'" class="imgEnlarge" style="width: 30px; height: 30px;"  id="brand_PrevImage"/>';

        //    }
        //},
        {
            'data': 'isActive',
            "sClass": " ",
            'render': function (data, type, row) {
                if (data) {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditVarOpt(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveVarOpt(' + data + ', ' + row["id"] + ' )">DeActive</button></div ></div>';
                }
                else {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditVarOpt(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveVarOpt(' + data + ', ' + row["id"] + ' )">Active</button></div ></div>';
                }
                

            }
        }
        ]
    });

}

function EditBrand(element) {
    //$('#divUploadedImg').show();
    RemoveAllFieldErrorBorderColor();
    var varoptId = $(element).closest("tr").find('.hiddenVarOptId').text().trim();
    var varoptname = $(element).closest("tr").find('.varoptName').text().trim();
    /*var desc = $(element).closest("tr").find('.catDesc').text().trim();*/
    //var prvImg = $(element).closest("tr").find('.prvImagePath').text().trim();
    $('#txtHiddenVarOptId').val(varoptId);
    $("#txt_VarOpt").val(varoptname);
    //$('#txthiddenPrvImage').val(prvImg);
    //var imgPath = "/Upload/Brand/" + prvImg;
    //$("#brand_PrevImage").attr("src", imgPath);
    $("#txt_VarOpt").val(varoptname);
    /* $("#txt_desc").val(desc);*/
    OpenAddPopUp();
}
function ActiveDActiveVarOpt(isActive, id) {
    $("#txt_Reason").val("");
    $("#btnActive").attr('disabled', 'disabled');
    $("#btnDeactive").attr('disabled', 'disabled');
    if (isActive) {
        $("#btnActive").hide();
        $("#btnDeactive").show();
        $("#spnActiveDeactive").text("Are you sure you want to Deactive this category");
    }
    else {
        $("#btnActive").show();
        $("#btnDeactive").hide();
        $("#spnActiveDeactive").text("Are you sure you want to Active this category");
    }
    $("#txtHiddenBrandId").val(id);
    $("#txtHiddenActiveInactive").val(isActive);
    $('#BrandActiveDeactiveModel').modal('show');
}
function DeleteVarOpt(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenVarOptId").val(id);
    $('#VarOptActiveDeactiveModel').modal('show');
    //if (confirm('Are you sure to delete this!')) {
    //    var formData = new FormData()
    //    formData.append("IN_Id", id);
    //    formData.append("IN_IsDelete", true);
    //    CategoryCrud(formData);
    //    ServerSideCategoryDataTable();
    //}
}
function OpenAddPopUp() {
    //$('#addCategory').modal('show');
    //$('#addCategory').addClass('active');
    //$('html, body').animate({
    //    scrollTop: $('#addCategory').offset().top - 30
    //}, 500);
    $('#addVarOpt').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addVarOpt').offset().top - 30
    }, 500);
}
function AddVarOptClick() {
    ResetField();
    $('#addVarOpt').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addVarOpt').offset().top - 30
    }, 500);
}
function VarOptActiveProcess(element) {
    var id = $("#txtHiddenVarOptId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#VarOptActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", true);
    formData.append("IN_Reason", $("#txt_Reason").val().trim());
    VarOptActiveDeactive(formData);
    ServerSideVarOptDataTable();
}
function VarOptDActiveProcess(element) {
    var id = $("#txtHiddenVarOptId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#VarOptActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", false);
    formData.append("IN_Reason",  $("#txt_Reason").val().trim());
    VarOptActiveDeactive(formData);
    ServerSideVarOptDataTable();
}
function VarOptActiveDeactive(formData) {
    $.ajax({
        type: "POST",
        url: '/VarientOption/VarientOptActiveDeactive',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {
            ResetField(); 
            if (result.returnCode == '200') {
                ServerSideVarOptDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtVarOptCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtVarOptDangerMsg").text(result.returnMessage);
                OpenCrudpopUpDanger();
            }
        },
        failure: function (response) {
            alert('Failed');
        }
    });
};

function buttonYesActive() {
    var reason = $("#txt_Reason").val().trim();
    if (reason.length > 0) {
        $("#btnActive").removeAttr('disabled');
        $("#btnDeactive").removeAttr('disabled'); 
    }
    else {
        $("#btnActive").attr('disabled', 'disabled');
        $("#btnDeactive").attr('disabled', 'disabled');
    }
}

