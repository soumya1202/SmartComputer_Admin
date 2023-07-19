$(document).ready(function () {
   //$('.tblCategory').dataTable();
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

function AddCategory() {
    RemoveAllFieldErrorBorderColor();
    var isValid = 1;
    var formData = new FormData();
    var catName = $("#txt_category").val().trim();
    var catImage = $("#cat_Img").val().trim();
    var catId = $('#txtHiddenCatId').val().trim();
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
            // alert(result.IsSuccess);
            $('#addCategory').removeClass('active');
            ResetField();
            //alert(JSON.parse(result));
            if (result.ReturnCode==200) {
                ServerSideCategoryDataTable();
                //DatableSearchColumnWise("tblCategory");
                $("#txtCategoryCrudResultMsg").text(result.Message);
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
    $('#txtHiddenCatId').val("");
}

function ServerSideCategoryDataTable() {
   
    $('.tblCategory').DataTable({
        "processing": true,
        "destroy": true,
        scrollX: false,
        "ajax": {
            "url": "/Section/CategoryDataTable",
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
    RemoveAllFieldErrorBorderColor();
    var catId = $(element).closest("tr").find('.hiddenCatId').text().trim();
    var catname = $(element).closest("tr").find('.catName').text().trim();
    var desc = $(element).closest("tr").find('.catDesc').text().trim();
    $('#txtHiddenCatId').val(catId);
    $("#txt_category").val(catname);
   /* $("#txt_desc").val(desc);*/
    OpenAddPopUp();
}
function DeleteCategory(id) {
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $("#txtHiddenCatId").val(id);
    $('#CategoryDeleteModel').modal('show');
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
function AddSectionClick() {
    ResetField();
    $('#addCategory').addClass('active');
    $('html, body').animate({
        scrollTop: $('#addCategory').offset().top - 30
    }, 500);
}
function DeleteCategoryProcess(element) {
    var id = $("#txtHiddenCatId").val();
    if (id == null || id.length == 0) {
        alert("Inernal server error");
        return false;
    }
    $('#CategoryDeleteModel').modal('hide');
    var formData = new FormData()
    formData.append("IN_Id", id);
    formData.append("IN_IsDelete", true);
    CategoryCrud(formData);
    ServerSideCategoryDataTable();
}

