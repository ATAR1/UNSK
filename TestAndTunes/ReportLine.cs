namespace TestAndTunes
{
    public class ReportLine
    {
        public string Deviation { get; internal set; }
        public string Duration { get; internal set; }
        public string Header { get; set; }
        public string Normative { get; internal set; }
        public int Quantity { get; internal set; }
        public bool TooLong { get; internal set; }
    }
}