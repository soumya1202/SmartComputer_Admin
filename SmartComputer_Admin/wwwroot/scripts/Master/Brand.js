$(document).ready(function () {
    // $('.tblCategory').dataTable();
    $('.closecategory').click(function () {
        $('#addBrand').removeClass('active');
    });

    ServerSideBrandDataTable();
    //DatableSearchColumnWise("tblCategory");

});

function OpenCrudpopUp() {
    $('#addBrand').removeClass('active');
    // show Modal
    $('#BrandAddDone').modal('show');
}
function OpenCrudpopUpDanger() {
    $('#txtBrandDangerMsg').val();
    $('#addBrand').removeClass('active');
    // show Modal
    $('#BrandError').modal('show');
}

function AddBrand() {
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var BrandName = $("#txt_Brands").val().trim();
    /*var catImage = $("#cat_Img").prop("files")[0];*/
    var BrandImage = $("#brand_Img").prop("files")[0];
    //alert(catImage.getAsDataURL());
    var BrandId = $('#txtHiddenBrandId').val().trim();
    var BrandPrevImage = $("#txthiddenPrvImage").val().trim();
    if (BrandId.length > 0) {
        formData.append("IN_Id", BrandId);
    }

    if (!HasValue(BrandName)) {
        SetBorderColorOfErrorField($("#txt_Brands"));
        isValid = 0;
    }
    //if (!HasValue(catDesc)) {
    //    SetBorderColorOfErrorField($("#txt_desc"));
    //    isValid = 0;
    //}
    if (isValid == 1) {
        formData.append("IN_Name", BrandName);
        formData.append("IN_Image", BrandImage);
        formData.append("IN_PrvImage", BrandPrevImage);
        //  formData.append("IN_IsActive", true);
        BrandCrud(formData);
        ResetField();
    }

}
function BrandCrud(formData) {
    $.ajax({
        type: "POST",
        url: '/Brand/BrandCrud',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {

            $('#addBrand').removeClass('active');
            ResetField();
            // alert(JSON.parse(result));
            if (result.returnCode == '200') {
                ServerSideBrandDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtBrandCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtBrandDangerMsg").text(result.returnMessage);
                OpenCrudpopUpDanger();
            }
        },
        failure: function (response) {
            alert('Failed');
        }
    });
};
function ResetField() {
    $("#txt_Brands").val("");
    $("#brand_Img").val("");
    $('#txtHiddenBrandId').val("");
    $('#txthiddenPrvImage').val("");
    $('#divUploadedImg').hide();
    $("#btnActive").hide();
    $("#btnDeactive").hide();
    $("#txt_Reason").val("");
    $("#btnActive").attr('disabled', 'disabled');
    $("#btnDeactive").attr('disabled', 'disabled');
}

function ServerSideBrandDataTable() {

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
            "url": "/Brand/BrandDataTable",
            dataSrc: ''
            //success: function (result) {
            //    alert(JSON.stringify(result));
            //}
        },
        "columns": [{
            "data": "id",
            "sClass": "dsplynone hiddenBrandId",

        },
        {
            "data": "brandName",
            "sClass": "brandName"
        },
        {
            "data": "imagePath",
            "sClass": "dsplynone prvImagePath"
        },
        {
            'data': 'imagePath',
            "sClass": " ",
            'render': function (data) {
                return '<img src="/Upload/Brand/' + data +'" class="imgEnlarge" style="width: 30px; height: 30px;"  id="brand_PrevImage"/>';

            }
        },
        {
            'data': 'isActive',
            "sClass": " ",
            'render': function (data, type, row) {
                if (data) {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditBrand(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveBrand(' + data + ', ' + row["id"] + ' )">DeActive</button></div ></div>';
                }
                else {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditBrand(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveBrand(' + data + ', ' + row["id"] + ' )">Active</button></div ></div>';
                }
                

            }
        }
        ]
    });

}

function EditBrand(element) {
    $('#divUploadedImg').show();
    RemoveAllFieldErrorBorderColor();
    var brandId = $(element).closest("tr").find('.hiddenBrandId').text().trim();
    var brandname = $(element).closest("tr").find('.brandName').text().trim();
    /*var desc = $(element).closest("tr").find('.catDesc').text().trim();*/
    var prvImg = $(element).closest("tr").find('.prvImagePath').text().trim();
    $('#txtHiddenBrandId').val(brandId);
    $("#txt_brands").val(brandname);
    $('#txthiddenPrvImage').val(prvImg);
    var imgPath = "/Upload/Brand/" + prvImg;
    $("#brand_PrevImage").attr("src", imgPath);
    $("#txt_brands").val(brandname);
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
    $("#txtHiddenBrandId").val(id);
    $("#txtHiddenActiveInactive").val(isActive);
    $('#BrandActiveDeactiveModel').modal('show');
}
function DeleteBrand(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenBrandId").val(id);
    $('#BrandActiveDeactiveModel').modal('show');
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
    $('#addBrand').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addBrand').offset().top - 30
    }, 500);
}
function AddBrandClick() {
    ResetField();
    $('#addBrand').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addBrand').offset().top - 30
    }, 500);
}
function BrandActiveProcess(element) {
    var id = $("#txtHiddenBrandId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#BrandActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", true);
    formData.append("IN_Reason", $("#txt_Reason").val().trim());
    BrandActiveDeactive(formData);
    ServerSideBrandDataTable();
}
function BrandDActiveProcess(element) {
    var id = $("#txtHiddenBrandId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#BrandActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", false);
    formData.append("IN_Reason",  $("#txt_Reason").val().trim());
    BrandActiveDeactive(formData);
    ServerSideBrandDataTable();
}
function BrandActiveDeactive(formData) {
    $.ajax({
        type: "POST",
        url: '/Brand/BrandActiveDeactive',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {
            ResetField(); 
            if (result.returnCode == '200') {
                ServerSideBrandDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtBrandCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtBrandDangerMsg").text(result.returnMessage);
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

