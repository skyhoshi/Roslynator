// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal readonly struct DocumentationCommentInfo
    {
        private static readonly XmlReaderSettings _xmlReaderSettings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment };

        public DocumentationCommentInfo(ISymbol symbol, XElement element)
        {
            Symbol = symbol;
            Element = element;
        }

        public ISymbol Symbol { get; }

        public XElement Element { get; }

        public string Summary
        {
            get { return Element?.Elements("summary").FirstOrDefault()?.Value; }
        }

        public static DocumentationCommentInfo Create(ISymbol symbol, CancellationToken cancellationToken = default(CancellationToken))
        {
            string xml = symbol.GetDocumentationCommentXml(cancellationToken: cancellationToken);

            if (string.IsNullOrEmpty(xml))
                return default;

            using (var sr = new StringReader(xml))
            using (XmlReader reader = XmlReader.Create(sr, _xmlReaderSettings))
            {
                if (reader.Read()
                    && reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "member":
                        case "doc":
                            {
                                try
                                {
                                    var node = (XElement)XNode.ReadFrom(reader);

                                    return new DocumentationCommentInfo(symbol, node);
                                }
                                catch (XmlException ex)
                                {
                                    Debug.Fail(ex.ToString());
                                    break;
                                }
                            }
                        default:
                            {
                                Debug.Fail(reader.Name);
                                break;
                            }
                    }
                }
            }

            return new DocumentationCommentInfo(symbol, null);
        }
    }
}
