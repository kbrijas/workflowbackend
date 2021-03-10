namespace QA.Workflow.Business.Transfers.Admin
{
    public class MenuModel
    {
        public int SeqNo { get; set; }
        public string Menu { get; set; }
        public string UisRef { get; set; }
        public int? ParentSeqNo { get; set; }
        public int? MenuOrder { get; set; }
    }
}
