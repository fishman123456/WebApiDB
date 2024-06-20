namespace WebApiDB.DB
{
    public class ReqestData
    {
        public int id { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime CompleteTime { get; set; }
        public string HttpMethod { get; set; }
        public int StatusCode { get; set; }
        public string URI { get; set; }

        public ReqestData() { }

        public override string ToString()
        {
            return ($"{id} : {HttpMethod} => { StatusCode}");
        }
    }
}
