using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNListDTO<T>
{
    public int currentPage { get; set; }
    public int totalPages { get; set; }
    public int totalRecords { get; set; }
    public int recordsPerPage { get; set; }
    public T[] results { get; set; }
}
