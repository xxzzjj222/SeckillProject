using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Common.Exceptions;
using Projects.SeckillServices.Models;
using Projects.SeckillServices.Pos;
using Projects.SeckillServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Controllers
{
    [Route("Seckills")]
    [ApiController]
    public class SeckillsController : ControllerBase
    {
        private readonly ISeckillService SeckillService;

        public SeckillsController(ISeckillService SeckillService)
        {
            this.SeckillService = SeckillService;
        }

        // GET: api/Seckills
        [HttpGet]
        public ActionResult<IEnumerable<Seckill>> GetSeckills()
        {
            // 1、业务异常
            // throw new BizException("业务异常信息22");
            return SeckillService.GetSeckills().ToList();
        }

        /// <summary>
        /// 根据时间查询
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpGet("GetList")]
        public ActionResult<IEnumerable<Seckill>> GetList([FromQuery] Seckill seckill)
        {
            List<Seckill> seckills = SeckillService.GetSeckills(seckill).ToList();

            // 1、一个页面需要多个模型 例如：秒杀列表：Seckill product ===> 新的对象Dto
            // 2、保证业务模型数据安全
            // 2、业务模型进行传输
            // po(参数) ---- > model(业务模型数据) ---->dto（外界访问数据）
            // AutoMapper
            return seckills;
        }

        // GET: api/Seckills/5
        [HttpGet("{id}")]
        public ActionResult<Seckill> GetSeckill(int id)
        {
            var Seckill = SeckillService.GetSeckillById(id);

            if (Seckill == null)
            {
                return NotFound();
            }

            return Seckill;
        }

        // PUT: api/Seckills/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutSeckill(int id, Seckill Seckill)
        {
            if (id != Seckill.Id)
            {
                return BadRequest();
            }

            try
            {
                SeckillService.Update(Seckill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeckillExists(id))
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


        /// <summary>
        /// 修改秒杀商品库存
        /// </summary>
        /// <param name="seckillPo"></param>
        /// <returns></returns>
        [HttpPut("set-stock")]
        public IActionResult PutProductStock(SeckillPo seckillPo)
        {
            // 1、查询秒杀库存
            Seckill seckill = SeckillService.GetSeckillByProductId(seckillPo.ProductId);

            // 2、判断秒杀库存是否完成
            if (seckill.SeckillStock <= 0)
            {
                throw new BizException("秒杀库存完了");
            }

            // 3、扣减秒杀库存
            seckill.SeckillStock = seckill.SeckillStock - seckillPo.ProductCount;

            // 4、更新秒杀库存
            SeckillService.Update(seckill);

            // 5、seckill 转换成为Dto对象seckill 
            // SeckillDto seckillDto = AutoMapperHelper.AutoMapTo<SeckillDto>(seckill);
            return Ok("更新库存成功");
        }

        /// <summary>
        /// 异步更新秒杀库存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productVo"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult SetProductStock(SeckillPo seckillPo)
        {
            // 1、查询秒杀库存
            Seckill seckill = SeckillService.GetSeckillByProductId(seckillPo.ProductId);

            // 2、判断秒杀库存是否完成
            if (seckill.SeckillStock <= 0)
            {
                throw new BizException("秒杀库存完了");
            }

            // 3、扣减秒杀库存
            seckill.SeckillStock = seckill.SeckillStock - seckillPo.ProductCount;

            // 4、更新秒杀库存
            SeckillService.Update(seckill);
            return Ok("更新库存成功");
        }

        // POST: api/Seckills
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Seckill> PostSeckill(Seckill Seckill)
        {
            SeckillService.Create(Seckill);
            return CreatedAtAction("GetSeckill", new { id = Seckill.Id }, Seckill);
        }

        // DELETE: api/Seckills/5
        [HttpDelete("{id}")]
        public ActionResult<Seckill> DeleteSeckill(int id)
        {
            var Seckill = SeckillService.GetSeckillById(id);
            if (Seckill == null)
            {
                return NotFound();
            }

            SeckillService.Delete(Seckill);
            return Seckill;
        }

        private bool SeckillExists(int id)
        {
            return SeckillService.SeckillExists(id);
        }
    }
}
