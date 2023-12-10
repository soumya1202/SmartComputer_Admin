//$(document).ready(function () {
//    //$('.tblCategory').dataTable();
//    $('.closecategory').click(function () {
//        $('#addSubCategory').removeClass('active');
//    });
//    alert("test");
//    ServerSideSubCategoryDataTable();
//    //DatableSearchColumnWise("tblCategory");

//});

$(document).ready(function () {
    // $('.tblCategory').dataTable();
    $('.closesubcategory').click(function () {
        $('#addSubCategory').removeClass('active');
    });     
    ServerSideSubCategoryDataTable();
    ServerSideCategoryDataTable();
    //DatableSearchColumnWise("tblCategory");

});

function OpenCrudpopUp() {
    $('#addSubCategory').removeClass('active');
    // show Modal
    $('#SubCategoryAddDone').modal('show');
}

function AddSubCategory() {
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var catName = $("#txt_category").val().trim();
    var catImage = $("#cat_Img").val().trim();
    var catId = $('#txtHiddenSubCatId').val().trim();
    if (catId.length > 0) {
        formData.append("IN_Id", catId);
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
        formData.append("IN_Name", catName);
        formData.append("IN_Image", catImage);
        //  formData.append("IN_IsActive", true);
        SubCategoryCrud(formData);
        ResetField();
    }

}
function SubCategoryCrud(formData) {
    alert('test');
    $.ajax({
        type: "POST",
        url: '/SubCategory/SubCategoryCrud',
        data: formData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (result) {
            // alert(result.IsSuccess);
            $('#addSubCategory').removeClass('active');
            ResetField();
            //alert(JSON.parse(result));
            if (result.ReturnCode == 200) {
                ServerSideSubCategoryDataTable();
                //DatableSearchColumnWise("tblCategory");
                $("#txtSubCategoryCrudResultMsg").text(result.Message);
                OpenCrudpopUp();
            }
            else {
                alert(result.ReturnMessage);
            }
        },
        failure: function (response) {
        }
    });
};
function ResetField() {
    $("#txt_category").val("");
    $("#cat_Img").val("");
    $('#txtHiddenSubCatId').val("");
}

function ServerSideSubCategoryDataTable() {
    alert('2');
    $('.tblSubCategory').DataTable({
        "processing": true,
        "destroy": true,
        scrollX: false,
        "ajax": {
            "url": "/SubCategory/SubCategoryDataTable",
            dataSrc: '',
            //success: function (result) {
            //    alert(JSON.stringify(result));
            //}
        },
        "columns": [{
            "data": "id",
            "sClass": "dsplynone hiddenSubCatId",

        },
        {
            "data": "subCategoryName", 
            "sClass": " "
        },

        {
                "data": "imagePath",
            "sClass": "dsplynone prvImagePath"
            },
            {
                'data': 'imagePath',
                "sClass": " ",
                'render': function (data) {
                    return '<img src="/Upload/Category/' + data + '" class="imgEnlarge" style="width: 30px; height: 30px;"  id="cat_PrevImage"/>';

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

function EditSubCategory(element) {
    RemoveAllFieldErrorBorderColor();
    var subcatId = $(element).closest("tr").find('.hiddenSubCatId').text().trim();
    var subcatname = $(element).closest("tr").find('.subcatName').text().trim();
    //var desc = $(element).closest("tr").find('.catDesc').text().trim();
    $('#txtHiddenSubCatId').val(subcatId);
    $("#txt_category").val(subcatname);
    /* $("#txt_desc").val(desc);*/
    OpenAddPopUp();
}
function DeleteCategory(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenSubCatId").val(id);
    $('#SubCategoryDeleteModel').modal('show');
    //if (confirm('Are you sure to delete this!')) {
    //    var formData = new FormData()
    //    formData.append("IN_Id", id);
    //    formData.append("IN_IsDelete", true);
    //    CategoryCrud(formData);
    //    ServerSideCategoryDataTable();
    //}
}
function OpenAddPopUp() {addCategory
    //$('#addCategory').modal('show');
    //$('#addCategory').addClass('active');
    //$('html, body').animate({
    //    scrollTop: $('#addCategory').offset().top - 30
    //}, 500);
    $('#addSubCategory').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addSubCategory').offset().top - 30
    }, 500);
}
function AddSectionClick() {
    alert('1');
    ResetField();
    $('#addSubCategory').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addSubCategory').offset().top - 30
    }, 500);
}
function DeleteSubCategoryProcess(element) {
    var id = $("#txtHiddenSubCatId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#SubCategoryDeleteModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsDelete", true);
    SubCategoryCrud(formData);
    ServerSideSubCategoryDataTable();
}

function ServerSideCategoryDataTable() {
    alert('1');
    $.ajax({
        type: "POST",
        url: "/Category/CategoryDataTable",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Please Select a Category</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '">' + data[i].categoryName + '</option>';
            }
            $(".txt_category").html(s);
        }
    });  
   

}

