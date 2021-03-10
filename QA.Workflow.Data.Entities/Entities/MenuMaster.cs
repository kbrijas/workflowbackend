using System;

namespace QA.Framework.DataEntities.Entities
{
    public partial class MenuMaster : BaseEntity
    {
        public int SeqNo { get; set; }
        public string Menu { get; set; }
        public string UisRef { get; set; }
        public int? ParentSeqNo { get; set; }
        public int? MenuOrder { get; set; }
    }

    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
