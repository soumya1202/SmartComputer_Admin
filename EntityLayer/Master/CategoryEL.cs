using EntityLayer.DataTable;
using System;
using System.Collections.Generic; 
using System.Text;

namespace EntityLayer.Category
{

    public class CategoryBaseModel
    {
        public int? Id { get; set; }
    }
    public class CategoryInputModel : CategoryBaseModel
    {
        public string CategoryName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }

    }

    public class CategoryInputDBModel
    {
        public CategoryInputDBModel(CategoryInputModel inputModel) {
            IN_ID = inputModel.Id;
            IN_CATEGORYNAME= inputModel.CategoryName;
            IN_IMAGEPATH= inputModel.ImagePath;
            IN_USEREMAIL = inputModel.CreatedBy;
        }
        public string IN_ACTION { get; set; }
        public int? IN_ID { get; set; }
        public string IN_CATEGORYNAME { get; set; }
        public string IN_IMAGEPATH { get; set; }
        public int IN_SECTIONID { get; set; }
        public string IN_USEREMAIL { get; set; }
        public int IN_MODIFIEDBY { get; set; }
        public bool IN_ISACTIVE { get; set; }
        public string IN_DeletedBy {get;set;}
        public string IN_DeletionReason { get; set; }
    }
    public class CategoryFetchModel : CategoryBaseModel
    {
    }
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SectionId { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }
    public class CategoryDatatableInputModel : DataTableBaseModel
    {
        public string IN_NAME { get; set; }
        public string IN_DESC { get; set; }
    }
    public class CategoryDataTableViewmodel : CategoryViewModel
    {
        public Int64 TOTALCOUNT { get; set; }
    }
    public class CategoryActiveDeactiveInputModel
    {
        public int CategoryId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
        public bool IsActive { get; set; } 
    }
    public class FetchCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string LastModificationReason { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }
        public string DeletionReason { get; set; }
    }
}
