using System.Reflection.Metadata;
using FreelanceAppAPI.Context;
using FreelanceAppAPI.Entities;
using FreelanceAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private ApplicationDbContext _context;
        public DocumentController(ApplicationDbContext context)
        {
            _context= context;
        }

        #region Get Methods
        [HttpGet("DocumentList")]
        public async Task<ActionResult<DocumentHeader>> GetDocHeader(){

            return Ok(await _context.DocumentHeaders.ToListAsync());
        } 

        [HttpGet("DocumentList/{docId}")]
        public async Task<ActionResult<DocumentModel>> GetDocumentById (Guid docId) 
        {
            var document = new DocumentModel();
            var docDetailsList = new List<DocumentDetail>();
            var docHeader = await _context.DocumentHeaders.FirstOrDefaultAsync(x => x.DocId == docId);

            if(docHeader != null) {
            document.Header = docHeader;
            docDetailsList = await _context.DocumentDetails.Where(x => x.DocId == docId).ToListAsync();
            document.Detail = docDetailsList;
            return Ok(document);
            }else {
                return BadRequest();
            }
        }

        #endregion

        #region Post Methods
        [HttpPost("CreateDocHeader")]
        public async Task<ActionResult<DocumentHeader>> CreateDocumentHeader([FromBody] DocumentHeader  docHeaderData) 
        {

            if (docHeaderData != null) {
                var docHeader = new DocumentHeader {
                DocId = new Guid(),
                UserId = docHeaderData.UserId,
                CutomerId = docHeaderData.CutomerId,
                ProjectName = docHeaderData.ProjectName,
                PeriodStart = docHeaderData.PeriodStart,
                PeriodEnd = docHeaderData.PeriodEnd,
                WeekHours = docHeaderData.WeekHours,
                TotalHoursWorked = docHeaderData.TotalHoursWorked,
                RegularHours = docHeaderData.RegularHours,
                OvertimeHours = docHeaderData.OvertimeHours,
                DocState = 1
            };

            _context.DocumentHeaders.Add(docHeader);
            
            await _context.SaveChangesAsync();
            
            return Ok(docHeaderData);
            
            }else {

                return BadRequest();
            }
        }

        [HttpPost("CreateDocDetail/{docId}")]
        public async Task<ActionResult<DocumentDetail>>  CreateDocDetail([FromBody] DocumentDetail docDetailData, Guid docId)
        {
            var docHeader = await _context.DocumentHeaders.FindAsync(docId);

            if (docHeader != null ){
                var docDetail = new DocumentDetail 
                {
                    RowId = new Guid(),
                    DocId = docId,
                    WorkedDate = docDetailData.WorkedDate,
                    TimeIn = docDetailData.TimeIn,
                    TimeOut = docDetailData.TimeOut,
                    OffTimeStart = docDetailData.OffTimeStart,
                    OffTimeEnd = docDetailData.OffTimeEnd,
                    TotalHours = docDetailData.TotalHours,
                    TaskComment = docDetailData.TaskComment
                };
                _context.DocumentDetails.Add(docDetail);
                await _context.SaveChangesAsync();
                
                return Ok(docDetail);
            }else {
                return BadRequest();
            }
            
            
        }

        #endregion

        #region Delete Methods
        [HttpDelete("{docId}")]
        public async Task<ActionResult> DeleteDocument(Guid docId)
        {
            var docHeader = await _context.DocumentHeaders.FindAsync(docId);
            if (docHeader !=null) {

                var docDetail =  _context.DocumentDetails.Where(x => x.DocId == docId);

                _context.Remove(docHeader);

                if (docDetail.Count() != 0){

                    _context.Remove(docDetail);
                }

                await _context.SaveChangesAsync();

                return Ok($"El documento  {docId} fue eliminado");
            }else {
                return BadRequest();
            }
        }

        #endregion

        #region  Put Methods
        

        #endregion
    
    }
}