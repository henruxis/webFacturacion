using System.Data;

namespace ERPWebAPI.Class
{
    public class Result
    {
        public bool Codigo { get; set; }
        public string Description { get; set; }
        public DataSet Dt { get; set; }
    }
}
