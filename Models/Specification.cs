namespace PCConfigurator.Data
{
    public class Specification
    {
        public int SpecificationID { get; set; }
        public int ComponentID { get; set; }
        public string SpecText { get; set; }
        public virtual ComponentEntity Component { get; set; }
    }
}
