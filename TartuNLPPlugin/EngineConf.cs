namespace TartuNLP
{
    public class EngineConf
    {
        public bool xml_support { get; set; }
        public DomainConf[] domains { get; set; }
    }
    
    public class DomainConf
    {
        public string name { get; set; }
        public string code { get; set; }
        public string[] languages { get; set; }
    }
}
