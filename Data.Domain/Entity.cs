using System;
using System.ComponentModel;

namespace Project.Domain {
    /// <summary>
    ///     数据实体基础类
    /// </summary>
    public abstract class Entity {
        /// <summary>
        ///     实体主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        ///     行标识，每次修改都会变更
        /// </summary>
        public byte[] RowFlag { get; set; }

        /// <summary>
        ///     最后变更时间
        /// </summary>
        public DateTime LastChange { get; set; }

        public bool IsChanged { get; set; }

        public virtual string Memo { get; set; }
    }

    /// <summary>
    ///     实体状态
    /// </summary>
    public enum EntityBaseStatus {
        [Description("正常")]
        Init = 0,

        [Description("已删除")]
        Deleted = 1000
    }
}