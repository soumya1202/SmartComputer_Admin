using EntityLayer.Brand;
using EntityLayer.DataTable;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Master
{
    public class SubCategoryBaseModel
    {
        public int? Id { get; set; }
    }
    public class SubCategoryInputModel : SubCategoryBaseModel
    {
        public string SubCategoryName { get; set; }
        //public int SectionId { get; set; }
        //public string SectionName { get; set; }
        public string ImagePath { get; set; }
        public IFormFileCollection Image { get; set; }
        public string CreatedBy { get; set; }
        public string PrevImage { get; set; }
    }

    public class SubCategoryInputDBModel
    {
        public SubCategoryInputDBModel(SubCategoryInputModel inputModel)
        {
            IN_ID = inputModel.Id;
            IN_SUBCATEGORYNAME = inputModel.SubCategoryName;
            IN_IMAGEPATH = inputModel.ImagePath;
            IN_USEREMAIL = inputModel.CreatedBy;
        }
        public SubCategoryInputDBModel(SubCategoryActiveDeactiveInputModel inputModel)
        {
            IN_ID = inputModel.SubCategoryId;
            IN_DeletedBy = inputModel.DeletedBy;
            IN_DeletionReason = inputModel.DeletionReason;
            IN_ISACTIVE = inputModel.IsActive;
        }
        public string IN_ACTION { get; set; }
        public int? IN_ID { get; set; }
        public string IN_SUBCATEGORYNAME { get; set; }
        public string IN_IMAGEPATH { get; set; }
        //public int IN_SECTIONID { get; set; }
        public string IN_USEREMAIL { get; set; }
        public int IN_MODIFIEDBY { get; set; }
        public bool IN_ISACTIVE { get; set; }
        public string IN_DeletedBy { get; set; }
        public string IN_DeletionReason { get; set; }
    }
    public class SubCategoryFetchModel : SubCategoryBaseModel
    {
    }
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        //public string SectionId { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }
    public class SubCategoryDatatableInputModel : DataTableBaseModel
    {
        public string IN_NAME { get; set; }
        public string IN_DESC { get; set; }
    }
    public class SubCategoryDataTableViewmodel : SubCategoryViewModel
    {
        public Int64 TOTALCOUNT { get; set; }
    }
    public class SubCategoryActiveDeactiveInputModel
    {
        public int SubCategoryId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
        public bool IsActive { get; set; }
    }

    public class FetchSubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        //public int SectionId { get; set; }
        //public string SectionName { get; set; }
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
