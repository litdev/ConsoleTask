namespace ConsoleTask.Model;

/// <summary>
/// 数据实体
/// </summary>
public class InquirySpider : BasicEntity
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
    public int SId { get; set; }


    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn()]
    public string Title { get; set; }

    /// <summary>
    /// 类别
    /// </summary>
    [SugarColumn()]
    public string Category { get; set; }

    /// <summary>
    /// 数据启用状态
    /// </summary>
    [SugarColumn()]
    public bool Status { get; set; } = true;

    /// <summary>
    /// 是否置顶
    /// </summary>
    [SugarColumn()]
    public bool IsTop { get; set; } = false;

}