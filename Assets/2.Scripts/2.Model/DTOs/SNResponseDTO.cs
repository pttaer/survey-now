using System.Collections.Generic;

public class SNResponseDTO<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public int RecordsPerPage { get; set; }
    public List<T> Results { get; set; }
}
