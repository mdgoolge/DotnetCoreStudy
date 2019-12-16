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