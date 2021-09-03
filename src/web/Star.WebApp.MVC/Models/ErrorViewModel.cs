using System.Collections.Generic;

namespace Star.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseResultMessages Errors { get; set; }
    }

    public class ResponseResultMessages
    { 
        public List<string> Messages { get; set; }
    }
}
