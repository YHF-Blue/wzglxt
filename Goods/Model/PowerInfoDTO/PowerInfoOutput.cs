using System;
using System.Collections.Generic;
using System.Text;

namespace Model.PowerInfoDTO
{
  public   class PowerInfoOutput
    {
        /// <summary>
        /// 节点唯一索引值，用于对指定节点进行各类操作——对应数据库的PowerId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 节点标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 节点字段名
        /// </summary>
        public string FieId { get; set; }

        /// <summary>
        /// 点击节点弹出新窗口对应的url。需开启isJump参数
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 节点是否初始展开，默认false
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 节点是否初始为选中状态（如果开启复选框的话），默认False
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 节点是否为禁用状态，默认false
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 子节点。支持设定选项同父节点
        /// </summary>
        public List<PowerInfoOutput> Children { get; set; }

    }
}
