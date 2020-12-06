using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.SeckillServices.Models;
using Projects.SeckillServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Controllers
{
    /// <summary>
    /// 秒杀记录服务控制器
    /// </summary>
    [Route("Seckills/{SeckillId}/SeckillRecords")]
    [ApiController]
    public class SeckillRecordsController : ControllerBase
    {
        private readonly ISeckillRecordService SeckillRecordService;

        public SeckillRecordsController(ISeckillRecordService SeckillRecordService)
        {
            this.SeckillRecordService = SeckillRecordService;
        }

        // GET: api/SeckillRecords
        [HttpGet]
        public ActionResult<IEnumerable<SeckillRecord>> GetSeckillRecords(int SeckillId)
        {
            return SeckillRecordService.GetSeckillRecords().ToList();
        }

        // GET: api/SeckillRecords/5
        [HttpGet("{id}")]
        public ActionResult<SeckillRecord> GetSeckillRecord(int id)
        {
            var SeckillRecord = SeckillRecordService.GetSeckillRecordById(id);

            if (SeckillRecord == null)
            {
                return NotFound();
            }

            return SeckillRecord;
        }

        // PUT: api/SeckillRecords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutSeckillRecord(int id, SeckillRecord SeckillRecord)
        {
            if (id != SeckillRecord.Id)
            {
                return BadRequest();
            }

            try
            {
                SeckillRecordService.Update(SeckillRecord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeckillRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SeckillRecords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<SeckillRecord> PostSeckillRecord(SeckillRecord SeckillRecord)
        {
            SeckillRecordService.Create(SeckillRecord);
            return CreatedAtAction("GetSeckillRecord", new { id = SeckillRecord.Id }, SeckillRecord);
        }

        // DELETE: api/SeckillRecords/5
        [HttpDelete("{id}")]
        public ActionResult<SeckillRecord> DeleteSeckillRecord(int id)
        {
            var SeckillRecord = SeckillRecordService.GetSeckillRecordById(id);
            if (SeckillRecord == null)
            {
                return NotFound();
            }

            SeckillRecordService.Delete(SeckillRecord);
            return SeckillRecord;
        }

        private bool SeckillRecordExists(int id)
        {
            return SeckillRecordService.SeckillRecordExists(id);
        }
    }
}
