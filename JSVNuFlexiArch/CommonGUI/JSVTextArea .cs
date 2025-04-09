﻿using Microsoft.AspNetCore.Html;
using NuFlexiArch;
using System.Runtime.Versioning;
using static JSVaporizer.JSVapor;

namespace JSVNuFlexiArch;

[SupportedOSPlatform("browser")]
public class JSVTextArea : ATextArea, IJSVComponent
{
    public string Id { get; set; }
    public string LabelId { get; set; }

    public JSVTextArea(string uniqueName)
    {
        Renderer = new JSVTextAreaRenderer();
        Metadata.Add("UnqPrefix", uniqueName);
        Metadata.Add("CompTypeAQN", GetAssemblyQualifiedName());

        Id = uniqueName.AppendElementSuffix("InputId");
        LabelId = uniqueName.AppendElementSuffix("LabelId");
    }

    private string? _labelVal;
    private string? _textVal;
    private int _rows;
    private int _cols;
    private int _maxLength;

    protected override void SetLabel(string? val)
    {
        _labelVal = val;
        Document.AssertGetElementById(LabelId).SetProperty("innerHTML", _labelVal ?? "");
    }

    protected override string? GetLabel()
    {
        return _labelVal;
    }

    protected override void SetTextValue(string? val)
    {
        _textVal = val;
        Document.AssertGetElementById(Id).SetFormElemValue(_textVal);
    }

    protected override string? GetTextValue()
    {
        return _textVal;
    }

    protected override void SetRows(int rows)
    {
        _rows = rows;
       Document.AssertGetElementById(Id).SetProperty("rows", _rows);
    }

    protected override int GetRows()
    {
        return _rows;
    }

    protected override void SetCols(int cols)
    {
        _cols = cols;
        Document.AssertGetElementById(Id).SetProperty("cols", _cols);
    }

    protected override int GetCols()
    {
        return _cols;
    }

    protected override void SetMaxLength(int maxLength)
    {
        _maxLength = maxLength;
        Document.AssertGetElementById(Id).SetProperty("maxLength", _maxLength);
    }

    protected override int GetMaxLength()
    {
        return _maxLength;
    }
}

[SupportedOSPlatform("browser")]
public class JSVTextAreaRenderer : JSVComponentRenderer
{
    protected override void RenderBody(AComponent tmpComp, HtmlContentBuilder htmlCB)
    {
        JSVTextArea comp = (JSVTextArea)tmpComp;

        string htmlStr = Environment.NewLine + $"<label id=\"{comp.LabelId}\" for=\"{comp.Id}\"></label>";
        htmlStr += Environment.NewLine + $"<textarea id=\"{comp.Id}\"></textarea>";

        htmlCB.AppendHtml(htmlStr);
    }
}
