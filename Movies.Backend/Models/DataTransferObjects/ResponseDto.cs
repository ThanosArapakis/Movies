namespace Movies.Backend.Models.DataTransferObjects
{
    //The Response object which cotains the MovieDto in the Result field, if the function called was successful and the Error Message if it was not
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
