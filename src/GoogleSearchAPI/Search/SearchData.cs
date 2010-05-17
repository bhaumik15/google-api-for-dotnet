﻿//-----------------------------------------------------------------------
// <copyright file="SearchData.cs" company="iron9light">
// Copyright (c) 2009 iron9light
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
// <author>iron9light@gmail.com</author>
//-----------------------------------------------------------------------

namespace Google.API.Search
{
    using System.Runtime.Serialization;

    internal interface ISearchData<TResult>
    {
        TResult[] Results { get; }

        int? CurrentIndex { get; }
    }

    [DataContract]
    internal class SearchData<TResult> : ISearchData<TResult>
    {
        [DataMember(Name = "results")]
        public TResult[] Results { get; private set; }

        [DataMember(Name = "cursor")]
        public CursorObject Cursor { get; private set; }

        public int? CurrentIndex
        {
            get
            {
                if (this.Cursor.Pages == null)
                {
                    return null;
                }

                return (int?)this.Cursor.CurrentPageIndex;
            }
        }

        [DataContract]
        public class CursorObject
        {
            [DataMember(Name = "pages")]
            public Page[] Pages { get; private set; }

            [DataMember(Name = "estimatedResultCount")]
            public long EstimatedResultCount { get; private set; }

            [DataMember(Name = "currentPageIndex")]
            public long CurrentPageIndex { get; private set; }

            [DataMember(Name = "moreResultsUrl")]
            public string MoreResultsUrl { get; private set; }

            [DataContract]
            public class Page
            {
                [DataMember(Name = "start")]
                public long Start { get; private set; }

                [DataMember(Name = "label")]
                public long Label { get; private set; }

                public override string ToString()
                {
                    return string.Format("start : {0}, label : {1}", this.Start, this.Label);
                }
            }
        }
    }
}