using TodoApi.Models;

public class CreateOperationRequestDTO {

    public string pacientid { get; set;}
    public string doctorid { get; set;}

    public string operationTypeId {get; set;}

    public string deadline {get; set;}

    public Priority priority {get; set;}
}