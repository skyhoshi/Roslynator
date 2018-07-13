// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Roslynator.Documentation
{
    public sealed class XmlDocumentation
    {
        private static readonly XmlReaderSettings _xmlReaderSettings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment };
        private static readonly Regex _indentationRegex = new Regex("(?<=\n)            ");

        private readonly Dictionary<string, SymbolXmlDocumentation> _symbolXmlDocumentations;
        private readonly XDocument _document;
        private readonly XElement _membersElement;

        private XmlDocumentation(XDocument document)
        {
            _document = document;
            _membersElement = document.Root.Element("members");
            _symbolXmlDocumentations = new Dictionary<string, SymbolXmlDocumentation>();
        }

        public static XmlDocumentation Load(string filePath)
        {
            XDocument document = XDocument.Load(filePath, LoadOptions.PreserveWhitespace);

            return new XmlDocumentation(document);
        }

        public SymbolXmlDocumentation GetDocumentation(string commentId)
        {
            if (!_symbolXmlDocumentations.TryGetValue(commentId, out SymbolXmlDocumentation documentation))
            {
                XElement element = _membersElement
                    .Elements()
                    .FirstOrDefault(f => f.Attribute("name")?.Value == commentId);

                if (element != null)
                {
                    element = Unindent(element);

                    documentation = new SymbolXmlDocumentation(commentId, element);
                    _symbolXmlDocumentations[commentId] = documentation;
                }
            }

            return documentation;
        }

        private static XElement Unindent(XElement element)
        {
            XElement firstElement = element.Elements().FirstOrDefault();

            if (firstElement != null)
            {
                XNode previousNode = firstElement.PreviousNode;

                if (previousNode is XText text)
                {
                    string s = text.Value;

                    bool areSpaces = true;
                    int count = 0;

                    for (int i = s.Length - 1; i >= 0; i--)
                    {
                        char ch = s[i];

                        if (ch == '\n'
                            || ch == '\r')
                        {
                            break;
                        }

                        if (ch == ' ')
                        {
                            count++;
                        }
                        else if (ch == '\t')
                        {
                            areSpaces = false;
                            count++;
                        }
                        else
                        {
                            Debug.Fail(ch.ToString());
                            count = 0;
                            break;
                        }
                    }

                    Debug.Assert(count == 12, count.ToString());
                    Debug.Assert(areSpaces);

                    if (count > 0)
                    {
                        s = s.Substring(s.Length - count, count);

                        string xml = element.ToString();

                        xml = (count == 12 && areSpaces)
                            ? _indentationRegex.Replace(xml, "")
                            : Regex.Replace(xml, "(?<=\n)" + s, "");

                        using (var sr = new StringReader(xml))
                        using (XmlReader xr = XmlReader.Create(sr, _xmlReaderSettings))
                        {
                            if (xr.Read()
                                && xr.NodeType == XmlNodeType.Element)
                            {
                                element = (XElement)XNode.ReadFrom(xr);
                            }
                        }
                    }
                }
            }

            return element;
        }
    }
}
