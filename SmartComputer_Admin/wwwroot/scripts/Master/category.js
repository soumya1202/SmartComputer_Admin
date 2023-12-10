$(document).ready(function () {
    // $('.tblCategory').dataTable();
    $('.closecategory').click(function () {
        $('#addCategory').removeClass('active');
    });
    ServerSideCategoryDataTable();
    //DatableSearchColumnWise("tblCategory");

});

function OpenCrudpopUp() {
    $('#addCategory').removeClass('active');
    // show Modal
    $('#CategoryAddDone').modal('show');
}
function OpenCrudpopUpDanger() {
    $('#txtCategoryDangerMsg').val();
    $('#addCategory').removeClass('active');
    // show Modal
    $('#CategoryError').modal('show');
}

function AddCategory() {
    alert('test1');
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var catName = $("#txt_category").val().trim();
    alert(catName);
    /*var catImage = $("#cat_Img").prop("files")[0];*/
    var catImage = $("#cat_Img").prop("files")[0];
    alert(catImage);
    //alert(catImage.getAsDataURL());
    var catId = $('#txtHiddenCatId').val().trim();
    alert(catId);
    var catPrevImage = $("#txthiddenPrvImage").val().trim();
    alert(catPrevImage);
    if (catId.length > 0) {
        formData.append("Id", catId);
    }

    if (!HasValue(catName)) {
        SetBorderColorOfErrorField($("#txt_category"));
        isValid = 0;
    }
    //if (!HasValue(catDesc)) {
    //    SetBorderColorOfErrorField($("#txt_desc"));
    //    isValid = 0;
    //}
    if (isValid == 1) {
        formData.append("CategoryName", catName);
        formData.append("Image", catImage);
        formData.append("PrvImage", catPrevImage);
        //  formData.append("IN_IsActive", true);
        CategoryCrud(formData);
        ResetField();
    }

}
function CategoryCrud(formData) {
    $.ajax({
        type: "POST",
        url: '/Category/CategoryCrud',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {

            $('#addCategory').removeClass('active');
            ResetField();
            // alert(JSON.parse(result));
            if (result.returnCode == '200') {
                ServerSideCategoryDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtCategoryCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtCategoryDangerMsg").text(result.returnMessage);
                OpenCrudpopUpDanger();
            }
        },
        failure: function (response) {
            alert('Failed');
        }
    });
};
function ResetField() {
    $("#txt_category").val("");
    $("#cat_Img").val("");
    $('#txtHiddenCatId').val("");
    $('#txthiddenPrvImage').val("");
    $('#divUploadedImg').hide();
    $("#btnActive").hide();
    $("#btnDeactive").hide();
    $("#txt_Reason").val("");
    $("#btnActive").attr('disabled', 'disabled');
    $("#btnDeactive").attr('disabled', 'disabled');
}

function ServerSideCategoryDataTable() {

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
            "url": "/Category/CategoryDataTable",
            dataSrc: ''
        },
        "columns": [{
            "data": "id",
            "sClass": "dsplynone hiddenCatId",

        },
        {
            "data": "categoryName",
            "sClass": "catName"
        },
        {
            "data": "imagePath",
            "sClass": "dsplynone prvImagePath"
        },
        {
            'data': 'imagePath',
            "sClass": " ",
            'render': function (data) {
                return '<img src="/Upload/Category/' + data +'" class="imgEnlarge" style="width: 30px; height: 30px;"  id="cat_PrevImage"/>';

            }
        },
        {
            'data': 'isActive',
            "sClass": " ",
            'render': function (data, type, row) {
                if (data) {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditCategory(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveCat(' + data + ', ' + row["id"] + ' )">DeActive</button></div ></div>';
                }
                else {
                    return '<div class="btn-group actionButton"><button type= "button" class="dropdown-toggle" data-toggle="dropdown" data- display="dynamic" aria- haspopup="true" aria-expanded="false">Button</button><div class="dropdown-menu dropdown-menu-lg-right"><button class="dropdown-item btn-editCategory" type="button" onclick="EditCategory(this)">Edit</button><button class="dropdown-item btn-deleteCategory" type="button" onclick="ActiveDActiveCat(' + data + ', ' + row["id"] + ' )">Active</button></div ></div>';
                }
                

            }
        }
        ]
    });

}

function EditCategory(element) {
    $('#divUploadedImg').show();
    RemoveAllFieldErrorBorderColor();
    var catId = $(element).closest("tr").find('.hiddenCatId').text().trim();
    var catname = $(element).closest("tr").find('.catName').text().trim();
    /*var desc = $(element).closest("tr").find('.catDesc').text().trim();*/
    var prvImg = $(element).closest("tr").find('.prvImagePath').text().trim();
    $('#txtHiddenCatId').val(catId);
    $("#txt_category").val(catname);
    $('#txthiddenPrvImage').val(prvImg);
    var imgPath = "/Upload/Category/" + prvImg;
    $("#cat_PrevImage").attr("src", imgPath);
    $("#txt_category").val(catname);
    /* $("#txt_desc").val(desc);*/
    OpenAddPopUp();
}
function ActiveDActiveCat(isActive, id) {
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
    $("#txtHiddenCatId").val(id);
    $("#txtHiddenActiveInactive").val(isActive);
    $('#CategoryActiveDeactiveModel').modal('show');
}
function DeleteCategory(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenCatId").val(id);
    $('#CategoryActiveDeactiveModel').modal('show');
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
    $('#addCategory').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addCategory').offset().top - 30
    }, 500);
}
function AddCategoryClick() {
    ResetField();
    $('#addCategory').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addCategory').offset().top - 30
    }, 500);
}
function CatActiveProcess(element) {
    var id = $("#txtHiddenCatId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#CategoryActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsActive", true);
    formData.append("IN_Reason", $("#txt_Reason").val().trim());
    CategoryActiveDeactive(formData);
    ServerSideCategoryDataTable();
}
function CatDActiveProcess(element) {
    var id = $("#txtHiddenCatId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#CategoryActiveDeactiveModel').modal('hide');
    var formData = new FormData()
    formData.append("CategoryId", id);
    formData.append("IsActive", false);
    formData.append("DeletionReason",  $("#txt_Reason").val().trim());
    CategoryActiveDeactive(formData);
    ServerSideCategoryDataTable();
}
function CategoryActiveDeactive(formData) {
    $.ajax({
        type: "POST",
        url: '/Category/CategoryActiveDeactive',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {
            ResetField(); 
            if (result.returnCode == '200') {
                ServerSideCategoryDataTable();
                //  DatableSearchColumnWise("tblCategory");
                $("#txtCategoryCrudResultMsg").text(result.returnMessage);
                OpenCrudpopUp();
            }
            else {
                $("#txtCategoryDangerMsg").text(result.returnMessage);
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

