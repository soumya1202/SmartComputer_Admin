using EntityLayer.DataTable;
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
        public string CategoryName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
    }
    public class SubCategoryFetchModel : SubCategoryBaseModel
    {
    }
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SectionId { get; set; }
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
        public int CategoryId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
        public bool IsActive { get; set; }
    }
}
