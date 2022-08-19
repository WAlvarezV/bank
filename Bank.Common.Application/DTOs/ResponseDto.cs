using Bank.Common.Application.Enum;

namespace Bank.Common.Application.DTOs
{
    public class ResponseDto
    {
        public Code Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDto()
        {

        }
        public ResponseDto(Code code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }
}
