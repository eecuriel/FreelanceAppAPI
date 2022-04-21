
using FreelanceAppAPI.Entities;

namespace FreelanceAppAPI.Models
{

public class DocumentModel 
{
    public DocumentHeader Header { get; set; }
    public List<DocumentDetail>  Detail { get; set; }

}

}