namespace Vega.Controllers.Resources
{
    public class VehichleQueryResource
    {
        public int? MakeId {get;set;}

        public string SortBy { get; set; }

        public bool IsSortAscending {get;set;}

         public int Page { get; set; }

        public byte PageSize { get; set; }
    }
}