using System;

namespace Common.CommonModel.Models
{
    public class ApiBase
    {
        public DateTime TsStamp { get; set; }

        public string UsStamp { get; set; }

        public string AsStamp { get; set; }

        public void SetBaseData(DateTime dt, string user, string app)
        {
            this.TsStamp = dt;
            this.UsStamp = user;
            this.AsStamp = app;
        }
    }
}