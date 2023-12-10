
using EntityLayer.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.VarientAtr
{
    public class VarientAtrBaseModel
    {
        public int? Id { get; set; }
    }
    public class VarientAtrInputModel : VarientAtrBaseModel
    {
        public string AttributeName { get; set; }
        //public string ImagePath { get; set; }
        public string CreatedBy { get; set; }

    }
    public class VarientAtrInputDBModel
    {
        public VarientAtrInputDBModel(VarientAtrInputModel inputModel)
        {
            IN_ID = inputModel.Id;
            IN_ATTRIBUTENAME = inputModel.AttributeName;
            //IN_IMAGEPATH = inputModel.ImagePath;
            IN_USEREMAIL = inputModel.CreatedBy;
        }
        public VarientAtrInputDBModel(VarientAtrActiveDeactiveInputModel inputModel)
        {
            IN_ID = inputModel.AttributeId;
            IN_DeletedBy = inputModel.DeletedBy;
            IN_DeletionReason = inputModel.DeletionReason;
            IN_ISACTIVE = inputModel.IsActive;
        }
        public string IN_ACTION { get; set; }
        public int? IN_ID { get; set; }
        public string IN_ATTRIBUTENAME { get; set; }
        //public string IN_IMAGEPATH { get; set; }
        //public int IN_SECTIONID { get; set; }
        public string IN_USEREMAIL { get; set; }
        public int IN_MODIFIEDBY { get; set; }
        public bool IN_ISACTIVE { get; set; }
        public string IN_DeletedBy { get; set; }
        public string IN_DeletionReason { get; set; }
    }
    public class VarientAtrFetchModel : VarientAtrBaseModel
    {
    }
    public class VarientAtrViewModel
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        //public string SectionId { get; set; }
        public bool IsActive { get; set; }
        //public string ImagePath { get; set; }
    }
    public class VarientAtrDatatableInputModel : DataTableBaseModel
    {
        public string IN_NAME { get; set; }
        //public string IN_DESC { get; set; }
    }
    public class VarientAtrDataTableViewmodel : VarientAtrViewModel
    {
        public Int64 TOTALCOUNT { get; set; }
    } 
    public class VarientAtrActiveDeactiveInputModel
    {
        public int AttributeId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
        public bool IsActive { get; set; }
    }
    public class FetchVarientAtr
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        //public int SectionId { get; set; }
        //public string SectionName { get; set; }
        //public string ImagePath { get; set; }

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
