namespace WebApiDB.DB
{
    public class ReqestData
    {
        public int id { get; set; }
        public DateTime ReceiveTime { get; set; }                      // время получения запроса
        public DateTime CompleteTime { get; set; }                    // время завершения обработки запроса
        public string HttpMethod { get; set; } = string.Empty;       // метод запроса
        public int StatusCode { get; set; }                         // код ответа
        public string URI { get; set; } = string.Empty;            // URI = URL + URN

        public ReqestData() { }

        public override string ToString()
        {
            return ($"{id} : {HttpMethod} {URI} => { StatusCode}");
        }
    }
}
