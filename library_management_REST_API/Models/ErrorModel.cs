public class ErrorModel
{
    public int Status { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string Stack { get; set; }

    public ErrorModel()
    {
        Status = 500;
        Message = "Error";
        Source = "";
        Stack = "";
    }
    public ErrorModel(int Stts, string msg)
    {
        Status = Stts;
        Message = msg;
    }
    public ErrorModel(int Stts, string msg, string src, string stack)
    {
        Status = Stts;
        Message = msg;
        Source = src;
        Stack = stack;
    }
}