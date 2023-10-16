namespace ConsoleTask.Model;

/// <summary>
/// 数据库实体基类
/// </summary>
public class BasicEntity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn()]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn()]
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 是否已删除
    /// </summary>
    [SugarColumn()]
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn()]
    public int Version { get; set; }
}
