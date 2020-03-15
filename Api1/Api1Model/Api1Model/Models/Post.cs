using Common.CommonModel.Models;

namespace Api1.Api1Model.Models
{
    public class Post : ApiBase
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}