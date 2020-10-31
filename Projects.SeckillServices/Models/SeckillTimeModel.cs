using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Models
{
    /// <summary>
    /// 秒杀时间模型
    /// </summary>
    public class SeckillTimeModel
    {
        [Key]
        public int Id { set; get; } // 秒杀时间编号
        public string TimeTitleUrl { set; get; } // 秒杀时间主题url
        public string SeckillDate { set; get; } // 秒杀日期(2020/8/10)
        public string SeckillStarttime { set; get; } // 秒杀开始时间点(20:00)
        public string SeckillEndtime { set; get; } // 秒杀结束时间点(22:00)
        public int TimeStatus { set; get; } // 秒杀时间状态（0：启动，1：禁用）
    }
}
