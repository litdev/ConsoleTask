# 概述

    基于 [Furion](https://furion.baiqian.ltd/docs) 框架，适合异步跑一些任务（爬虫等）

## 解析Html

nuget 引用`HtmlAgilityPack` 包，大概使用示例：

```dotnet
var html = ""; //远程请求获取的HTML源码
var doc = new HtmlDocument();
doc.LoadHtml(html);

var ul = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/table/tr");

var result = new List<InquirySpider>();

foreach (var li in ul)
{
    var tagA = li.SelectSingleNode("a");
    var sourceUrl = "http://domain.com/id=" + tagA.Attributes["href"].Value;
    var sourceId = sourceUrl.RegexMatchValue("?id=(\\d*)"); //正则匹配路径的数据id
    var title = tagA.SelectSingleNode("span[1]").InnerText;
    var publishDate = tagA.SelectSingleNode("span").InnerText.ObjToDate();

    //var category = targetP.InnerText.Trim().RegexMatchValue("| 采购类别：(\\w*)");
        

    result.Add(new InquirySpider()
    {
        Title = title,
        SourceId = sourceId,
        SourceUrl = sourceUrl,
        PublishDate = publishDate,
        SourceName = sourceName,
        SourceType = sourceType,
    });
}


```


