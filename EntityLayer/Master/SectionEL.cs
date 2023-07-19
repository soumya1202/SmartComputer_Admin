using EntityLayer.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Master
{
    public class SectionBaseModel
    {
        public int? Id { get; set; }
    }
    public class SectionInputModel : SectionBaseModel
    {
        public string CategoryName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
    }
    public class SectionFetchModel : SectionBaseModel
    {
    }
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SectionId { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }
    public class SectionDatatableInputModel : DataTableBaseModel
    {
        public string IN_NAME { get; set; }
        public string IN_DESC { get; set; }
    }
    public class SectionDataTableViewmodel : SectionViewModel
    {
        public Int64 TOTALCOUNT { get; set; }
    }
    public class SectionActiveDeactiveInputModel
    {
        public int CategoryId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
        public bool IsActive { get; set; }
    }
}
