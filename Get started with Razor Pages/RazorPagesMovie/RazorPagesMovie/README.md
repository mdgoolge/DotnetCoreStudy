# Get started with Razor Pages

### 异步编程是 ASP.NET Core 和 EF Core 的默认模式

### @page 指令
@page Razor 指令将文件转换为一个 MVC 操作，这意味着它可以处理请求。 @page 必须是页面上的第一个 Razor 指令。

### 注释 
行 @*Markup removed for brevity.*@ 为 Razor 注释。 与 HTML 注释不同 (<!-- -->)，Razor 注释不会发送到客户端。

### 标记帮助程序
通配符语法（“*”），指定程序集 (Microsoft.AspNetCore.Mvc.TagHelpers) 中的所有标记帮助程序对于 Views 目录或子目录中的所有视图文件均可用

@addTagHelper 后第一个参数指定要加载的标记帮助程序（我们使用“*”指定加载所有标记帮助程序），第二个参数“Microsoft.AspNetCore.Mvc.TagHelpers”指定包含标记帮助程序的程序集。 Microsoft.AspNetCore.Mvc.TagHelpers 是内置 ASP.NET Core 标记帮助程序的程序集

如果项目包含具有默认命名空间 (AuthoringTagHelpers.TagHelpers.EmailTagHelper) 的 EmailTagHelper，则可提供标记帮助程序的完全限定名称 (FQN)：
CSHTML

@using AuthoringTagHelpers
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper AuthoringTagHelpers.TagHelpers.EmailTagHelper, AuthoringTagHelpers

### [LINQ查询执行](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/ef/language-reference/query-execution)
在对 LINQ 查询进行定义或通过调用方法（如 Where、Contains 或 OrderBy）进行修改后，此查询不会被执行。 相反，会延迟执行查询。 这意味着表达式的计算会延迟，直到循环访问其实现的值或者调用 ToListAsync 方法为止

Contains 方法在数据库中运行，而不是在 C# 代码中运行。 查询是否区分大小写取决于数据库和排序规则。 在 SQL Server 上，Contains 映射到 SQL LIKE，这是不区分大小写的。 在 SQLite 中，由于使用了默认排序规则，因此需要区分大小写。

### 模型绑定
 模型绑定搜索不区分大小写