$(document).ready(function () {
    // $('.tblCategory').dataTable();
    $('.closecategory').click(function () {
        $('#addVarAtr').removeClass('active');
    });

    ServerSideVarAtrDataTable();
    //DatableSearchColumnWise("tblCategory");

});

function OpenCrudpopUp() {
    $('#addVarAtr').removeClass('active');
    // show Modal
    $('#VarAtrAddDone').modal('show');
}
function OpenCrudpopUpDanger() {
    $('#txtVarAtrDangerMsg').val();
    $('#addVarAtr').removeClass('active');
    // show Modal
    $('#VarAtrError').modal('show');
}

function AddVarAtr() {
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var VarAtrName = $("#txt_VarAtr").val().trim();
    /*var catImage = $("#cat_Img").prop("files")[0];*/
    //var BrandImage = $("#brand_Img").prop("files")[0];
    //alert(catImage.getAsDataURL());
    var VarAtrId = $('#txtHiddenVarAtrId').val().trim();
    //var BrandPrevImage = $("#txthiddenPrvImage").val().trim();
    if (VarAtrId.length > 0) {
        formData.append("IN_Id", VarAtrId);
    }

    if (!HasValue(VarAtrName)) {
        SetBorderColorOfErrorField($("#txt_VarAtr"));
        isValid = 0;
    }
    //if (!HasValue(catDesc)) {
    //    SetBorderColorOfErrorField($("#txt_desc"));
    //    isValid = 0;
    //}
    if (isValid == 1) {
        formData.append("IN_Name", VarAtrName);
        //formData.append("IN_Image", BrandImage);
        //formData.append("IN_PrvImage", BrandPrevImage);
        //  formData.append("IN_IsActive", true);
        VarAtrCrud(formData);
        ResetField();
    }

}
function VarAtrCrud(formData) {
    $.ajax({
        type: "POST",
        url: '/VarientAttribute/VarientAtrCrud',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {

            $('#addVarAtr').removeClass('active');
            ResetField();
            // alert(JSON.parse(result));
            if (result.returnCode == '200') {
                ServerSideVarAtrDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtVarAtrCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtVarAtrDangerMsg").text(result.returnMessage);
                OpenCrudpopUpDanger();
            }
        },
        failure: function (response) {
            alert('Failed');
        }
    });
};
function ResetField() {
    $("#txt_VarAtr").val("");
    //$("#brand_Img").val("");
    $('#txtHiddenVarAtrId').val("");
    //$('#txthiddenPrvImage').val("");
    //$('#divUploadedImg').hide();
    $("#btnActive").hide();
    $("#btnDeactive").hide();
    $("#txt_Reason").val("");
    $("#btnActive").attr('disabled', 'disabled');
    $("#btnDeactive").attr('disabled', 'disabled');
}

function ServerSideVarAtrDataTable() {

    //$('.tblCategory').dataTable({
    //    dom: 'rtip',
    //    scrollX: true,
    //    scrollCollapse: true,
    //    "paging": true,
    //    "ordering": true,
    //    "filter": true,
    //    "destroy": true,
    //    "orderMulti": false,
    //    "processing": true,
    //    "serverSide": true,
    //    "orderCellsTop": true,

    //    "ajax":
    //    {
    //        "url": "/Category/CategoryDataTable",

    //        "type": "POST",
    //        "dataType": "JSON",

    //        complete: function (data) {
    //            //alert(JSON.parse( data));
    //        }
    //    },
    //    columns: [
    //        {
    //            'data': 'id',
    //            "sClass": "dsplynone hiddenCatId",
    //        },
    //        {
    //            'data': 'categoryName',
    //            "sClass": "catName"
    //        }
    //    ],
    //    "oLanguage": {
    //        "sEmptyTable": "No record(s) to display",
    //        "loadingRecords": "Please wait - loading..."
    //    },
    //});
    $('.tblCategory').DataTable({
        "processing": true,
        "destroy": true,
        scrollX: false,
        "ajax": {
            "url": "/VarientAttribute/VarientAtrDataTable",
            dataSrc: ''
            //success: function (result) {
            //    alert(JSON.stringify(result));
            //}
        },
        "columns": [{
            "data": "id",
            "sClass": "dsplynone hiddenVarAtrId",

        },
        {
            "data": "attributeName",
            "sClass": "varatrName"
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
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditVarAtr(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveVarAtr(' + data + ', ' + row["id"] + ' )">DeActive</button></div ></div>';
                }
                else {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditVarAtr(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveVarAtr(' + data + ', ' + row["id"] + ' )">Active</button></div ></div>';
                }
                

            }
        }
        ]
    });

}

function EditVarAtr(element) {
    //$('#divUploadedImg').show();
    RemoveAllFieldErrorBorderColor();
    var varatrId = $(element).closest("tr").find('.hiddenVarAtrId').text().trim();
    var varatrname = $(element).closest("tr").find('.varatrName').text().trim();
    /*var desc = $(element).closest("tr").find('.catDesc').text().trim();*/
    //var prvImg = $(element).closest("tr").find('.prvImagePath').text().trim();
    $('#txtHiddenBrandId').val(varatrId);
    $("#txt_brands").val(varatrname);
    //$('#txthiddenPrvImage').val(prvImg);
    //var imgPath = "/Upload/Brand/" + prvImg;
    //$("#brand_PrevImage").attr("src", imgPath);
    $("#txt_brands").val(varatrname);
    /* $("#txt_desc").val(desc);*/
    OpenAddPopUp();
}
function ActiveDActiveBrand(isActive, id) {
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
    $("#txtHiddenVarAtrId").val(id);
    $("#txtHiddenActiveInactive").val(isActive);
    $('#VarAtrActiveDeactiveModel').modal('show');
}
function DeleteBrand(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenVarAtrId").val(id);
    $('#VarAtrActiveDeactiveModel').modal('show');
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
    $('#addVarAtr').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addVarAtr').offset().top - 30
    }, 500);
}
function AddVarAtrClick() {
    ResetField();
    $('#addVarAtr').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addVarAtr').offset().top - 30
    }, 500);
}
function VarAtrActiveProcess(element) {
    var id = $("#txtHiddenVarAtrId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#VarAtrActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", true);
    formData.append("IN_Reason", $("#txt_Reason").val().trim());
    BrandActiveDeactive(formData);
    ServerSideVarAtrDataTable();
}
function VarAtrDActiveProcess(element) {
    var id = $("#txtHiddenVarAtrId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#VarAtrActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", false);
    formData.append("IN_Reason",  $("#txt_Reason").val().trim());
    VarAtrActiveDeactive(formData);
    ServerSideVarAtrDataTable();
}
function VarAtrActiveDeactive(formData) {
    $.ajax({
        type: "POST",
        url: '/VarientAttribute/VarientAtrActiveDeactive',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {
            ResetField(); 
            if (result.returnCode == '200') {
                ServerSideVarAtrDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtVarAtrCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtVarAtrDangerMsg").text(result.returnMessage);
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

