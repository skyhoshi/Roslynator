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

        private readonly Dictionary<string, XElement> _cache;
        private readonly XDocument _document;
        private readonly XElement _members;

        private XmlDocumentation(XDocument document)
        {
            _document = document;
            _members = document.Root.Element("members");
            _cache = new Dictionary<string, XElement>();
        }

        public static XmlDocumentation Load(string filePath)
        {
            XDocument document = XDocument.Load(filePath, LoadOptions.PreserveWhitespace);

            return new XmlDocumentation(document);
        }

        public string GetElementValue(string id, string name)
        {
            return GetElement(id, name).Value;
        }

        internal XElement GetElement(string id, string name)
        {
            return GetElement(id)?.Element(name);
        }

        internal XmlReader CreateReader(string id, string name)
        {
            return GetElement(id)?.Element(name).CreateReader();
        }

        internal XElement GetElement(string id)
        {
            if (!_cache.TryGetValue(id, out XElement element))
            {
                element = _members.Elements().FirstOrDefault(f => f.Attribute("name")?.Value == id);

                if (element != null)
                {
                    element = Unindent(element);

                    _cache[id] = element;
                }
            }

            return element;
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

                        using (var stringReader = new StringReader(xml))
                        using (XmlReader xmlReader = XmlReader.Create(stringReader, _xmlReaderSettings))
                        {
                            if (xmlReader.Read()
                                && xmlReader.NodeType == XmlNodeType.Element)
                            {
                                element = (XElement)XNode.ReadFrom(xmlReader);
                            }
                        }
                    }
                }
            }

            return element;
        }
    }
}
