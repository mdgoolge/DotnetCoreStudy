# Get started with Razor Pages

### �첽����� ASP.NET Core �� EF Core ��Ĭ��ģʽ

### @page ָ��
@page Razor ָ��ļ�ת��Ϊһ�� MVC ����������ζ�������Դ������� @page ������ҳ���ϵĵ�һ�� Razor ָ�

### ע�� 
�� @*Markup removed for brevity.*@ Ϊ Razor ע�͡� �� HTML ע�Ͳ�ͬ (<!-- -->)��Razor ע�Ͳ��ᷢ�͵��ͻ��ˡ�

### ��ǰ�������
ͨ����﷨����*������ָ������ (Microsoft.AspNetCore.Mvc.TagHelpers) �е����б�ǰ���������� Views Ŀ¼����Ŀ¼�е�������ͼ�ļ�������

@addTagHelper ���һ������ָ��Ҫ���صı�ǰ�����������ʹ�á�*��ָ���������б�ǰ������򣩣��ڶ���������Microsoft.AspNetCore.Mvc.TagHelpers��ָ��������ǰ�������ĳ��򼯡� Microsoft.AspNetCore.Mvc.TagHelpers ������ ASP.NET Core ��ǰ�������ĳ���

�����Ŀ��������Ĭ�������ռ� (AuthoringTagHelpers.TagHelpers.EmailTagHelper) �� EmailTagHelper������ṩ��ǰ����������ȫ�޶����� (FQN)��
CSHTML

@using AuthoringTagHelpers
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper AuthoringTagHelpers.TagHelpers.EmailTagHelper, AuthoringTagHelpers

### [LINQ��ѯִ��](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/ef/language-reference/query-execution)
�ڶ� LINQ ��ѯ���ж����ͨ�����÷������� Where��Contains �� OrderBy�������޸ĺ󣬴˲�ѯ���ᱻִ�С� �෴�����ӳ�ִ�в�ѯ�� ����ζ�ű��ʽ�ļ�����ӳ٣�ֱ��ѭ��������ʵ�ֵ�ֵ���ߵ��� ToListAsync ����Ϊֹ

Contains ���������ݿ������У��������� C# ���������С� ��ѯ�Ƿ����ִ�Сдȡ�������ݿ��������� �� SQL Server �ϣ�Contains ӳ�䵽 SQL LIKE�����ǲ����ִ�Сд�ġ� �� SQLite �У�����ʹ����Ĭ��������������Ҫ���ִ�Сд��

### ģ�Ͱ�
 ģ�Ͱ����������ִ�Сд