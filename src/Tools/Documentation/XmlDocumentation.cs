// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Roslynator.Documentation
{
    public sealed class XmlDocumentation
    {
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
            XDocument document = XDocument.Load(filePath);

            return new XmlDocumentation(document);
        }

        public string GetSummary(string id)
        {
            return GetOrAddElement(id)?.Element("summary").Value;
        }

        public string GetRemarks(string id)
        {
            return GetOrAddElement(id)?.Element("remarks").Value;
        }

        public string GetExamples(string id)
        {
            return GetOrAddElement(id)?.Element("examples").Value;
        }

        public string GetElementValue(string id, string name)
        {
            return GetOrAddElement(id)?.Element(name)?.Value;
        }

        public XElement GetOrAddElement(string id)
        {
            if (!_cache.TryGetValue(id, out XElement element))
            {
                element = _members.Elements().FirstOrDefault(f => f.Attribute("name")?.Value == id);

                if (element != null)
                {
                    _cache[id] = element;
                }
            }

            return element;
        }
    }
}
